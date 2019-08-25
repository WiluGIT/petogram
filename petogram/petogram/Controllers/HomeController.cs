using Microsoft.AspNet.Identity;
using petogram.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace petogram.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult MyProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(m => m.Id == userId)
                .Include(m => m.Posts)
                .FirstOrDefault();


            return View(user);
        }


    }
}