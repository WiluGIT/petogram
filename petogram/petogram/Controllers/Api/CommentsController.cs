using Microsoft.AspNet.Identity;
using petogram.Models;
using petogram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace petogram.Controllers.Api
{
    [Authorize]
    public class CommentsController : ApiController
    {
        private ApplicationDbContext db;

        public CommentsController()
        {
            db = new ApplicationDbContext();

        }

        [HttpPost]
        public IHttpActionResult Comment([FromUri]int id, [FromUri]string content)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Single(m => m.Id == userId);
            var post = db.Posts.SingleOrDefault(m => m.Id == id);
            

            var comment = new Comment
            {
                PostId=id,
                UserId=userId,
                Content=content
            };
            post.CommentCount++;
            post.Comments.Add(comment);
            db.Comments.Add(comment);
            user.Comments.Add(comment);
            db.SaveChanges();

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteComment([FromBody] int id)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Single(m => m.Id == userId);
            var comment = db.Comments.SingleOrDefault(m => m.Id == id && m.UserId == userId);


            if (comment == null)
                return NotFound();

            user.Comments.Remove(comment);
            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok();

        }
    }
}
