using Microsoft.AspNet.Identity;
using petogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace petogram.Controllers.Api
{
    public class PostsController : ApiController
    {
        private ApplicationDbContext db;

        public PostsController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Like([FromBody]int id)
        {
            var userId = User.Identity.GetUserId();
            var post = db.Posts.Single(m => m.Id == id);

            post.LikeCount++;

            var like = new Like
            {
                PostId = id,
                UserId = userId,
                IsLiked = true
            };
            db.Likes.Add(like);
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnLick(int id)
        {
            var post = db.Posts.Single(m => m.Id == id);
            var userId = User.Identity.GetUserId();

            var like = db.Likes.Single(m => m.PostId == post.Id && m.UserId == userId);

            if (like == null)
                return NotFound();

            post.LikeCount--;
            db.Likes.Remove(like);
            db.SaveChanges();

            return Ok();
        }

    }
}
