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
            if (ModelState.IsValid)
            {
                var currentUser = await GetCurrentUserAsync();

                // This ONE line of code below took about 9 hours to create...
                //List<Book> userList = await context.Books
                //    .Where(b => b.Id == b.BookUsers.First().BookId
                //    && currentUser.Id == b.BookUsers.First().ApplicationUserId).ToListAsync();

                List<Book> omg = (from b in context.Books
                                  join bu in context.BookUsers on b.Id equals bu.BookId
                                  join a in context.ApplicationUsers on bu.ApplicationUserId equals a.Id
                                  where a.Id == currentUser.Id
                                  select new Book
                                  {
                                      Id = b.Id,
                                      BookTitle = b.BookTitle,
                                      AuthorFirstName = b.AuthorFirstName,
                                      AuthorLastName = b.AuthorLastName,
                                      Genre = b.Genre,
                                      NumberOfPages = b.NumberOfPages,
                                  }).ToList<Book>();



                return View(omg);
            }

            return View();
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
                    //ApplicationUserId = currentUser.Id
                };

                Book extantBook = (from b in context.Books where b.BookTitle == newBook.BookTitle select b).FirstOrDefault();

                if (extantBook != null)
                {

                    if (context.BookUsers.ToList().Count(bu => bu.BookId == extantBook.Id
                    && bu.ApplicationUserId == currentUser.Id) == 0)

                    {


                        //var bookId = context.Books
                        //    .Where(b => b.BookTitle == newBook.BookTitle)
                        //    .Select(b => b.Id);

                        var rUserId = currentUser.Id;
                        //newBook.Id = (from b in context.Books
                        //              where b.BookTitle == newBook.BookTitle
                        //              select b.Id).First();

                        BookUser newBookUser = new BookUser
                        {
                            BookId = extantBook.Id,
                            ApplicationUserId = rUserId
                        };

                        context.BookUsers.Add(newBookUser);
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
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
                }

                return RedirectToAction(nameof(Index));
            }
            return View(addBookViewModel);
        }
        //[HttpGet]
        //public IActionResult Delete()
        //{
        //    /*ViewBag.books = BookData.GetAll();*/
        //    ViewBag.books = context.Books.ToList();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int bookId)
        {
            var currentUser = await GetCurrentUserAsync();

            //foreach (int bookId in bookIds)
            //{
            //    /*BookData.Remove(bookId);*/
            //    Book theBook = context.Books.Find(bookId);
            //    context.Books.Remove(theBook);
            //}

            BookUser extantUser = (from bu in context.BookUsers
                                   where bu.BookId == bookId && bu.ApplicationUserId == currentUser.Id
                                   select bu).FirstOrDefault();

            if (extantUser != null)
            {
                context.BookUsers.Remove(extantUser);
                context.SaveChanges();

                extantUser = (from bu in context.BookUsers
                              where bu.BookId == bookId
                              select bu).FirstOrDefault();

                if (extantUser == null)
                {
                    Book extantBook = context.Books.Find(bookId);
                    context.Books.Remove(extantBook);
                    context.SaveChanges();
                }

            }
            
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
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

        public IActionResult TheirBooks(string userName)
        {
            var findUserBooks = (from b in context.Books
                                 join bu in context.BookUsers on b.Id equals bu.BookId
                                 join a in context.ApplicationUsers on bu.ApplicationUserId equals a.Id
                                 where a.Id == userName
                                 select new Book { Id = b.Id, BookTitle = b.BookTitle }).ToList<Book>();
            /*.Where(bu => bu.ApplicationUserId == userName)
            *//*.Include(p => p.BookId)*//*
            .ToList();*/
            ViewBag.User = userName;/*.Remove(userName.IndexOf("@"));*/
            return View(findUserBooks);
        }

    }
}
