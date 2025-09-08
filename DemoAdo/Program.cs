using DemoAdo.Models;
using Microsoft.Data.SqlClient;

Console.WriteLine("Hello, World!");

string connectionString = "Data Source=BSTORM-PHIL\\DATAVIZ;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Initial Catalog=DemoADO;";

// Création du "pont" entre C# et SQL Server
SqlConnection connection = new(connectionString);

//TestConnection(connection);

//using (SqlConnection connection = new(connectionString))
//{
//    Console.WriteLine("Connection réussie!");
//}

// GetBookCount(connection);
//GetBookTitle(connection, 10);
//GetBookTitle3(connection, "'; UPDATE Book set Title = 'test';--");
Console.WriteLine(InsertBook(
                    connection,
                    "Alice au pays des merveilles"));

List<Book> booksFromDB = GetBooks(connection);

for(int i = 0; i < booksFromDB.Count(); i++)
{
    Console.WriteLine($"{booksFromDB[i].id} {booksFromDB[i].title}");
}

//DeleteBook(connection, 8);
//DeleteBook(connection, 9);

//booksFromDB = GetBooks(connection);

//for (int i = 0; i < booksFromDB.Count(); i++)
//{
//    Console.WriteLine($"{booksFromDB[i].id} {booksFromDB[i].title}");
//}



void TestConnection(SqlConnection connection)
{
    // Ouverture du "pont"
    connection.Open();

    // 🎉 ça fonctionne!
    Console.WriteLine("Connection réussie!");

    // Ferme le "pont"
    connection.Close();
}

void GetBookCount(SqlConnection connection)
{
    using (SqlCommand command = connection.CreateCommand())
    {
        command.CommandText = "SELECT COUNT(*) FROM Book;";

        connection.Open();

        int nbLivre = (int)command.ExecuteScalar();

        connection.Close();

        Console.WriteLine($"Il y a {nbLivre} livres en DB");
    }
}

void GetBookTitle(SqlConnection connection, int livreId)
{
    using (SqlCommand command = connection.CreateCommand())
    {
        command.CommandText = $"SELECT Title FROM Book WHERE BookId = {livreId};";

        connection.Open();

        string titre = (string)command.ExecuteScalar();

        connection.Close();
        
        if (titre is null)
        {
            Console.WriteLine($"Aucun livre n'a été trouvé pour l'id {livreId}");
        }
        else
        {
            Console.WriteLine($"Le titre du livre avec l'id {livreId} est {titre}");
        }
    }
}

// demonstration injection SQL
void GetBookTitle2(SqlConnection connection, string livreTitre)
{
    using (SqlCommand command = connection.CreateCommand())
    {
        command.CommandText = $"SELECT Title FROM Book WHERE Title = '{livreTitre}';";
        //"SELECT Title FROM Book WHERE Title = ''; UPDATE Book set Title = 'test';--';"

        connection.Open();

        string titre = (string)command.ExecuteScalar();

        connection.Close();

        if (titre is null)
        {
            Console.WriteLine($"Aucun livre n'a été trouvé avec ce titre {livreTitre}");
        }
        else
        {
            Console.WriteLine($"On a trouvé un livre avec ce titre {livreTitre}");
        }
    }
}

void GetBookTitle3(SqlConnection connection, string livreTitre)
{
    using (SqlCommand command = connection.CreateCommand())
    {
        command.CommandText = $"SELECT Title FROM Book WHERE Title = '@titre';";

        command.Parameters.AddWithValue("titre", livreTitre);

        connection.Open();

        string titre = (string)command.ExecuteScalar();

        connection.Close();

        if (titre is null)
        {
            Console.WriteLine($"Aucun livre n'a été trouvé avec ce titre {livreTitre}");
        }
        else
        {
            Console.WriteLine($"On a trouvé un livre avec ce titre {livreTitre}");
        }
    }
}

List<Book> GetBooks (SqlConnection connection)
{
    List<Book> books = new List<Book>();
    using (SqlCommand command = connection.CreateCommand())
    {
        command.CommandText = "SELECT * FROM Book";

        connection.Open();

        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                //Console.WriteLine($"{reader["BookId"]} {reader["Title"]} {reader["ReleaseYear"]}");
                books.Add(new Book(
                            (int)reader["BookId"], 
                            (string)reader["Title"], 
                            (DateTime?)(reader["ReleaseYear"] == DBNull.Value ? null : reader["ReleaseYear"])));
            }
        }

        // operation
        connection.Close();
    }
    return books;
}

void DeleteBook (SqlConnection connection, int id)
{
    using (SqlCommand command = connection.CreateCommand())
    {
        command.CommandText = "DELETE FROM Book WHERE BookId = @id";
        command.Parameters.AddWithValue("id", id);

        connection.Open();

        int nbLivre = command.ExecuteNonQuery();

        connection.Close();

        if (nbLivre > 0)
        {
            Console.WriteLine($"On a supprimé {nbLivre} Livres");
        }
        else
        {
            Console.WriteLine("Aucun livre n'a été supprimé");
        }
    }
}

int InsertBook(SqlConnection connection,string title, DateTime? releaseYear = null)
{
    int idCreated = -1;
    using (SqlCommand command = connection.CreateCommand())
    {
        command.CommandText = "INSERT INTO Book (Title, ReleaseYear) " +
            "OUTPUT inserted.BookId " +
            "VALUES (@title, @releaseYear)";

        command.Parameters.AddWithValue("title", title);
        command.Parameters.AddWithValue("releaseYear", releaseYear is null ? DBNull.Value : releaseYear);

        connection.Open();

        idCreated = (int)command.ExecuteScalar();

        connection.Close();
    }
    return idCreated;
}
