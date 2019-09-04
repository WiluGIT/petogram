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
    [Authorize]
    public class PostsController : ApiController
    {
        private ApplicationDbContext db;

        public PostsController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Like([FromBody]int PostId)
        {
            var userId = User.Identity.GetUserId();
            var post = db.Posts.Single(m => m.Id == PostId);

            post.LikeCount++;

            var like = new Like
            {
                PostId = PostId,
                UserId = userId,
                IsLiked = true
            };

            var user = db.Users.Single(m => m.Id == userId);

            user.Likes.Add(like);
            db.Likes.Add(like);
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnLick([FromUri]int id)
        {
            var post = db.Posts.SingleOrDefault(m => m.Id == id);
            var userId = User.Identity.GetUserId();

            var like = db.Likes.SingleOrDefault(m => m.PostId == post.Id && m.UserId == userId);
            

            if (like == null)
                return NotFound();

            like.IsLiked = false;
            var user = db.Users.SingleOrDefault(m => m.Id == userId);
            user.Likes.Remove(like);

            post.LikeCount--;
            db.Likes.Remove(like);
            db.SaveChanges();

            return Ok();
        }

    }
}
