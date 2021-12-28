using BookCollection.Data;
using BookCollection.Models;
using BookCollection.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace BookCollection.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            List<Book> books = new List<Book>(BookData.GetAll());
            return View(books);
        }

        public IActionResult Add()
        {
            AddBookViewModel addBookViewModel = new AddBookViewModel();
            return View(addBookViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel addBookViewModel)
        {
            Book newBook = new Book
            {
                BookTitle = addBookViewModel.BookTitle,
                AuthorFirstName = addBookViewModel.AuthorFirstName,
                AuthorLastName = addBookViewModel.AuthorLastName,
                Genre = addBookViewModel.Genre,
                NumberOfPages = addBookViewModel.NumberOfPages,
            };

            BookData.Add(newBook);

            return Redirect("/Books");
        }

        public IActionResult Delete()
        {
            ViewBag.books = BookData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] bookIds)
        {
            foreach (int bookId in bookIds)
            {
                BookData.Remove(bookId);
            }

            return Redirect("/Books");
        }

        [HttpGet]
        [Route("Books/Edit/{bookId}")]
        public IActionResult Edit(int bookId)
        {
            Book editingBook = BookData.GetById(bookId);
            ViewBag.bookToEdit = editingBook;
            ViewBag.title = "Edit Book " + editingBook.BookTitle + "(id = " + editingBook.Id + ")";
            return View();
        }

        [HttpPost]
        [Route("Books/Edit")]
        public IActionResult SubmitEditBookForm(int bookId, string booktitle, string authorfirstname, string authorlastname, string genre, int numberofpages)
        {
            Book editingBook = BookData.GetById(bookId);
            editingBook.BookTitle = booktitle;
            editingBook.AuthorFirstName = authorfirstname;
            editingBook.AuthorLastName = authorlastname;
            editingBook.Genre = genre;
            editingBook.NumberOfPages = numberofpages;
            return Redirect("/Books");
        }
    }
}
