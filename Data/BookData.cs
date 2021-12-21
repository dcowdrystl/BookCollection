using BookCollection.Models;
using System.Collections.Generic;

namespace BookCollection.Data
{
    public class BookData
    {
        // Store books
        static private Dictionary<int, Book> Books = new Dictionary<int, Book>();

        // Add books
        public static void Add(Book newBook)
        {
            Books.Add(newBook.Id, newBook);
        }

        // Retrieve all books
        public static IEnumerable<Book> GetAll()
        {
            return Books.Values;
        }

        //Retrieve a single book
        public static Book GetById(int id)
        {
            return Books[id];
        }

        public static void Remove(int id)
        {
            Books.Remove(id);
        }
    }
}
