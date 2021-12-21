using System;

namespace BookCollection.Models
{
    public class Book
    {
        public string BookTitle { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int Id { get; }
        private static int nextId = 1;

        public Book()
        {
            Id = nextId;
            nextId++;
        }

        public Book(string booktitle, string authorfirstname, string authorlastname, string genre, int numberofpages)
        {
            BookTitle = booktitle;
            AuthorFirstName = authorfirstname;
            AuthorLastName = authorlastname;
            Genre = genre;
            NumberOfPages = numberofpages;
        }      

        public override string ToString()
        {
            return BookTitle;
        }

        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   Id == book.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
