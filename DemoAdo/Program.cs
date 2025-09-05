using Microsoft.Data.SqlClient;

Console.WriteLine("Hello, World!");

string connectionString = "Data Source=BSTORM-PHIL\\DATAVIZ;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;";

// Création du "pont" entre C# et SQL Server
SqlConnection connection = new(connectionString);

// Ouverture du "pont"
connection.Open();

// 🎉 ça fonctionne!
Console.WriteLine("Connection réussie!");

// TODO des requêtes SQL

// Ferme le "pont"
connection.Close();