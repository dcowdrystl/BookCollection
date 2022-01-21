using BookCollection.Data;
using BookCollection.Models;
using BookCollection.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookCollection.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private BookDbContext context;
        private UserManager<ApplicationUser> _userManager;

        public BooksController(BookDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        //[AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (!ModelState.IsValid) { return View(); }
            /*List<Book> books = new List<Book>(BookData.GetAll());*/

            var currentUser = await GetCurrentUserAsync();
            var bookDbContext = context.Books
                .Where(b => b.ApplicationUserId == currentUser.Id);

            return View(await bookDbContext.ToListAsync());
        }

        public IActionResult Add()
        {
            //var currentUser = await GetCurrentUserAsync();

            AddBookViewModel addBookViewModel = new AddBookViewModel();
            return View(addBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel addBookViewModel)
        {
            var currentUser = await GetCurrentUserAsync();
            if (!ModelState.IsValid)
            {

                return View("Add", addBookViewModel);

            }
            else if (ModelState.IsValid)
            {

                
                Book newBook = new Book
                {
                    BookTitle = addBookViewModel.BookTitle,
                    AuthorFirstName = addBookViewModel.AuthorFirstName,
                    AuthorLastName = addBookViewModel.AuthorLastName,
                    Genre = addBookViewModel.Genre,
                    NumberOfPages = addBookViewModel.NumberOfPages,
                    ApplicationUserId = currentUser.Id
                };

                //context.SaveChanges();
                context.Books.Add(newBook);
                await context.SaveChangesAsync();

                var bookId = newBook.Id;
                var rUserId = currentUser.Id;

                BookUser newBookUser = new BookUser
                {
                    BookId = bookId,
                    ApplicationUserId = rUserId
                };

                /*NOT MINE BookData.Add(newBook);*/
                context.BookUsers.Add(newBookUser);
                await context.SaveChangesAsync();
                //context.Books.Add(newBook);
                //await context.SaveChangesAsync();

                //return Redirect("/Books");
                return RedirectToAction(nameof(Index));
            }
            return View(addBookViewModel);
        }

        public IActionResult Delete()
        {
            /*ViewBag.books = BookData.GetAll();*/
            ViewBag.books = context.Books.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] bookIds)
        {
            foreach (int bookId in bookIds)
            {
                /*BookData.Remove(bookId);*/
                Book theBook = context.Books.Find(bookId);
                context.Books.Remove(theBook);
            }

            context.SaveChanges();
            return Redirect("/Books");
        }

        [HttpGet]
        [Route("Books/Edit/{bookId}")]
        public IActionResult Edit(int bookId)
        {
            /*Book editingBook = BookData.GetById(bookId);*/
            Book editingBook = context.Books.Find(bookId);

            ViewBag.bookToEdit = editingBook;
            ViewBag.title = "Edit Book " + editingBook.BookTitle + "(id = " + editingBook.Id + ")";
            return View();
        }

        [HttpPost]
        [Route("Books/Edit")]
        public IActionResult SubmitEditBookForm(int bookId, string booktitle, string authorfirstname, string authorlastname, string genre, int numberofpages)
        {
            /*Book editingBook = BookData.GetById(bookId);*/
            Book editingBook = context.Books.Find(bookId);
            editingBook.BookTitle = booktitle;
            editingBook.AuthorFirstName = authorfirstname;
            editingBook.AuthorLastName = authorlastname;
            editingBook.Genre = genre;
            editingBook.NumberOfPages = numberofpages;

            context.SaveChanges();
            return Redirect("/Books");
        }
      
    }
}
