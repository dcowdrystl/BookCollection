using BookCollection.Models;
using BookCollection.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookCollection.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            return View(addUserViewModel);
        }

        [HttpPost]
        [Route("/user/add")]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", addUserViewModel);
            }

            User user = new User()
            {
                Username = addUserViewModel.UserName,
                Email = addUserViewModel.Email,
                Password = addUserViewModel.Password
            };
            return View("Index", user);
        }
    }
}