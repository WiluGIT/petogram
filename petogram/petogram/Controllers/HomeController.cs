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
            var userId = User.Identity.GetUserId();

            var user = db.Users.Where(m => m.Id == userId).Include(m => m.Followees).FirstOrDefault();

           
            

            return View(db.Posts.Include(m=>m.User).ToList());
        }


    }
}