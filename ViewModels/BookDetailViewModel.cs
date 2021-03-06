
using BookCollection.Models;
﻿using System;
using System.Collections.Generic;

namespace BookCollection.ViewModels
{
    public class BookDetailViewModel
    {


        public string BookTitle { get; set; }
        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }

        public IList<UserProfile> UserBooks { get; set; }

        public BookDetailViewModel(Book theBook, IList<UserProfile> userProfiles)
        {
            BookTitle = theBook.BookTitle;
            AuthorFirstName = theBook.AuthorFirstName;
            AuthorLastName = theBook.AuthorLastName;
            Genre = theBook.Genre;
            NumberOfPages = theBook.NumberOfPages;

            UserBooks = userProfiles;
        }
    }
}