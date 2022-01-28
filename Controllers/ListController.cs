﻿using BookCollection.Data;
using BookCollection.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            //if (User.Identity.IsAuthenticated)
            //{
            //    var currentUser = await GetCurrentUserAsync();

            //    var omg = (from b in context.Books
            //               join bu in context.BookUsers on b.Id equals bu.BookId
            //               join au in context.ApplicationUsers on bu.ApplicationUserId equals au.Id
            //               where au.Id == currentUser.Id
            //               select new Book
            //               {
            //                   Id = b.Id,
            //                   BookTitle = b.BookTitle,
            //                   AuthorFirstName = b.AuthorFirstName,
            //                   AuthorLastName = b.AuthorLastName,
            //                   Genre = b.Genre,
            //                   NumberOfPages = b.NumberOfPages,
            //               }).ToList();

            //    var titles = (from bu in context.BookUsers
            //        .Include(x => x.BookId)
            //        .Where(x => x.ApplicationUserId == currentUser.Id)
            //        .FirstOrDefault()
            //        .ToString();
            //    ViewBag.bookTitles = new List<string>();
            //    foreach (var book in titles)
            //    {

            //        ViewBag.bookTitles.Add(book);
            //    }
            //}

            return View(books);
        }

        public IActionResult List()
        {
            ViewBag.books = context.Books.ToList();
            return View();
        }


    }
}
