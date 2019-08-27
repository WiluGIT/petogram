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
    public class ProfileController : Controller
    {
        private ApplicationDbContext db;

        public ProfileController()
        {
            db = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(m => m.Id == userId)
                .Include(m => m.Posts)
                .Include(m=>m.Followees)
                .Include(m=>m.Followers)
                .FirstOrDefault();


            return View(user);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Single(m => m.Id == userId);

            var viewModel = new ProfileViewModel
            {
                Biography=user.Biography,
                Name=user.Name,
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(ProfileViewModel model)
        {
            var userId = User.Identity.GetUserId();

            var user = db.Users.Single(m => m.Id == userId);

            user.Name = model.Name;
            user.Biography = model.Biography;

            db.SaveChanges();

            return RedirectToAction("MyProfile");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ProfilePicture()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ProfilePicture(ProfileViewModel model)
        {
            string filename = Path.GetFileNameWithoutExtension(model.ImgFile.FileName);
            string extension = Path.GetExtension(model.ImgFile.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            model.ImgPath = "~/img/" + filename;
            filename = Path.Combine(Server.MapPath("~/img/"), filename);
            model.ImgFile.SaveAs(filename);

            var userId = User.Identity.GetUserId();

            var user = db.Users.Single(m => m.Id == userId);

            user.ProfilePicture = model.ImgPath;
            db.SaveChanges();

            return RedirectToAction("MyProfile");
        }


        public ActionResult SearchResult(string query)
        {
            var userId = User.Identity.GetUserId();
            var profiles= db.Users
                    .Where(m=>!m.Id.Equals(userId))
                    .Include(m => m.Posts)
                    .Include(m => m.Followers)
                    .Include(m => m.Followees)
                    .ToList();

            if (!String.IsNullOrEmpty(query))
            {
                    profiles = db.Users
                    .Where(m => m.Name.Contains(query) && !m.Id.Equals(userId))
                    .Include(m=>m.Posts)
                    .Include(m=>m.Followers)
                    .Include(m=>m.Followees)
                    .ToList();



            }
            var followings = db.Followings
                    .Where(m => m.FollowerId == userId)
                    .ToList()
                    .ToLookup(m => m.FolloweeId);

            var model = new SearchViewModel
            {
                Profiles = profiles,
                Followings = followings
            };


            return View(model);

        }


    }
}