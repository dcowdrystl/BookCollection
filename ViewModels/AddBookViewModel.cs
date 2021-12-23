using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollection.ViewModels
{
    public class AddBookViewModel
    {
        public string BookTitle { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
    }
}
