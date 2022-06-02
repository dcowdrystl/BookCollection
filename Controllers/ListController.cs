using BookCollection.Data;
using BookCollection.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace BookCollection.Controllers
{
    public class ListController : Controller
    {
        private BookDbContext context;
        private UserManager<ApplicationUser> _userManager;
        public ListController(BookDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }
        /*public IActionResult Index()
        {
            List<Book> books = context.Books.ToList();

            return View(books);
        }*/
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> IndexAsync(string searchTerm)
        {
            List<Book> books = context.Books
            .ToList();
            ViewBag.bookTitles = new List<string>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
            
                books = context.Books
                        .Where(j => j.BookTitle.Contains(searchTerm.Trim()) || j.AuthorFirstName.Contains(searchTerm.Trim())
                        || j.AuthorLastName.Contains(searchTerm.Trim()) || j.Genre.Contains(searchTerm.Trim()))
                        .ToList();
            }

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await GetCurrentUserAsync();

                var titles = (from b in context.Books
                              join bu in context.BookUsers on b.Id equals bu.BookId
                              join au in context.ApplicationUsers on bu.ApplicationUserId equals au.Id
                              where au.Id == currentUser.Id
                              select new Book
                              {
                                  Id = b.Id,
                                  BookTitle = b.BookTitle,
                                  AuthorFirstName = b.AuthorFirstName,
                                  AuthorLastName = b.AuthorLastName,
                                  Genre = b.Genre,
                                  NumberOfPages = b.NumberOfPages,
                                  ApplicationUserId = ""
                              }).ToList();
                
                foreach (var book in titles)
                {

                    ViewBag.bookTitles.Add(book.BookTitle);
                }
            }

            return View(books);
        }

        public IActionResult List()
        {
            ViewBag.books = context.Books
            .ToList();
            return View();
        }

      /* [HttpGet]
       [Route("List/Genre")]*/

      /* public async Task<IActionResult> ShowGenreAsync(string genre)
       {
          // if (!string.IsNullOrEmpty(genre)) { }
          Book start = new Book(); 




          List<Book> books2 = context.Books
             .ToList();


            *//* books2 = context.Books
                     .Where(j => j.Genre == genre)
                    //.Include(j => j.BookUsers)
                    //.Include(j => j.User)
                    //.Include(j => j.Genre)
                     .ToList();*//*


          List<Book> booksToShow = new List<Book>();
          foreach (var book in books2)
          {
             if (book.Genre == genre)
             {
                booksToShow.Add(book);
             }
             booksToShow.ToList();
          }

          return View("Genre", books2);
         // return RedirectToAction("Index", books2);


       }*/

      public async Task<IActionResult> ShowGenre(string searchTerm, string genre)
      {
         Book book1 = new Book();
         List<Book> books = context.Books
         .Where(b => b.Genre == genre)
         .ToList();

        /* List<Book> booksToShow = new List<Book>();
         foreach (Book book in books)
         {
            if (book.Genre == genre)
            {
               booksToShow.Add(book);
            }
            booksToShow.ToList();
         }*/
         
         ViewBag.bookTitles = new List<string>();
         if (!string.IsNullOrEmpty(searchTerm))
         {

            books = context.Books
                    .Where(j => j.BookTitle.Contains(searchTerm.Trim()) || j.AuthorFirstName.Contains(searchTerm.Trim())
                    || j.AuthorLastName.Contains(searchTerm.Trim()) || j.Genre.Contains(searchTerm.Trim()))
                    .ToList();
         }

         if (User.Identity.IsAuthenticated)
         {
            var currentUser = await GetCurrentUserAsync();

            var titles = (from b in context.Books
                          join bu in context.BookUsers on b.Id equals bu.BookId
                          join au in context.ApplicationUsers on bu.ApplicationUserId equals au.Id
                          where au.Id == currentUser.Id
                          select new Book
                          {
                             Id = b.Id,
                             BookTitle = b.BookTitle,
                             AuthorFirstName = b.AuthorFirstName,
                             AuthorLastName = b.AuthorLastName,
                             Genre = b.Genre,
                             NumberOfPages = b.NumberOfPages,
                             ApplicationUserId = ""
                          }).ToList();

            foreach (var book in titles)
            {

               ViewBag.bookTitles.Add(book.BookTitle);
            }
         }
         
         return View("Index", books);
      }



   }
}
