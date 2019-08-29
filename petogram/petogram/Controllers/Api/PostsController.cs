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
            var post = db.Posts.Single(m => m.Id == id);

            post.Like++;
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnLick(int id)
        {
            var post = db.Posts.Single(m => m.Id == id);

            if(post.Like>0)
            {
                post.Like--;
                db.SaveChanges();
            }


            return Ok();
        }

    }
}
