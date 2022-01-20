using BookCollection.Data;
using BookCollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace BookCollection.Controllers
{
    public class ListController : Controller
    {
        private BookDbContext context;
        public ListController(BookDbContext dbContext)
        {
            context = dbContext;
        }
        /*public IActionResult Index()
        {
            List<Book> books = context.Books.ToList();

            return View(books);
        }*/
        public IActionResult Index(string searchTerm)
        {
            List<Book> books = context.Books.ToList();
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View(books);
            }
            else
            {
                books = context.Books
                        .Where(j => j.BookTitle.Contains(searchTerm) || j.AuthorFirstName.Contains(searchTerm)
                        || j.AuthorLastName.Contains(searchTerm) || j.Genre.Contains(searchTerm))
                        .ToList();
            }

            return View(books);
        }

        public IActionResult List()
        {
            ViewBag.books = context.Books.ToList();
            return View();
        }


    }
}
