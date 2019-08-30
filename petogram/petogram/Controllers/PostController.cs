using Microsoft.AspNet.Identity;
using petogram.Models;
using petogram.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace petogram.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db;

        public PostController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel model)
        {
            string filename = Path.GetFileNameWithoutExtension(model.ImgFile.FileName);
            string extension = Path.GetExtension(model.ImgFile.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            model.ImgPath = "~/img/" + filename;
            filename = Path.Combine(Server.MapPath("~/img/"), filename);
            model.ImgFile.SaveAs(filename);

            var userId = User.Identity.GetUserId();

            var user = db.Users.Single(m => m.Id == userId);

            var post = new Post
            {
                Description=model.Description,
                Img=model.ImgPath,
                Location=model.Location,
                User=user
                
            };
            db.Posts.Add(post);
            db.SaveChanges();


            return RedirectToAction("Mine","Post");
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var posts = db.Posts.Where(m => m.UserId == userId)
                .Include(m => m.User)
                .ToList();



            var likeings = db.Likes.Where(m => m.UserId == userId)
                .ToList()
                .ToLookup(m => m.PostId);

            var model = new LickViewModel
            {
                Posts = posts,
                Likeings= likeings
            };


            return View(model);
        }
    }
}