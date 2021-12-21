using Microsoft.AspNetCore.Mvc;

namespace BookCollection.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
