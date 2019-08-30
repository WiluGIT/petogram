using Microsoft.AspNet.Identity;
using petogram.Models;
using petogram.ViewModels;
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

            var posts = db.Posts
                .Include(m => m.User)
                .ToList();

            var likeings = db.Likes.Where(m => m.UserId == userId)
                .ToList()
                .ToLookup(m => m.PostId);

            var model = new LickViewModel
            {
                Posts = posts,
                Likeings = likeings
            };




            return View(model);
        }


    }
}