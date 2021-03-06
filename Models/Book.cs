using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookCollection.Models
{
    public class Book
    {
        public string BookTitle { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int Id { get; set; }

        //[ForeignKey("ApplicationUserId")]
        public ApplicationUser User { get; set; }

        //[Key]
        public string ApplicationUserId { get; set; }

        public IList<BookUser> BookUsers { get; set; }

        public Book()
        {
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
