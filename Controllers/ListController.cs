using BookCollection.Data;
using BookCollection.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            List<Book> books = context.Books.ToList();

            return View(books);
        }

        public IActionResult List()
        {
            ViewBag.books = context.Books.ToList();
            return View();
        }
    }
}
