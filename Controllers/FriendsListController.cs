using BookCollection.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookCollection.Controllers
{
    public class FriendsListController : Controller
    {
        private readonly BookDbContext _context;

        public FriendsListController(BookDbContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            var people = _context.Users.ToList();
            var friends = _context.Friends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .ToList();
            //Remove friends I already have from list
            ViewBag.friendNames = new List<string>();
            foreach (var relationship in friends)
            {
                if (User.Identity.Name == relationship.User.UserName)
                {
                    ViewBag.friendNames.Add(relationship.Friend.UserName);
                }
            }

            //Remove myself from friends options
            var self = people.Find(x => x.UserName == User.Identity.Name);
            people.Remove(self);
            return View(people);
        }
    }
}
