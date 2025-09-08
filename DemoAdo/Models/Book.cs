using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAdo.Models
{
    public class Book
    {
        public int id;
        public string title;
        public DateTime? releaseYear;

        public Book (int id, string title, DateTime? releaseYear = null)
        {
            this.id = id;
            this.title = title;
            this.releaseYear = releaseYear;
        }
    }
}
