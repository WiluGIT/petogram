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
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext db;

        public FollowingsController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow([FromBody] string followeId)
        {
            var userId = User.Identity.GetUserId();

            if(db.Followings.Any(f=>f.FolloweeId==userId && f.FolloweeId==followeId))
                return BadRequest("Following already exists");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followeId
            };

            db.Followings.Add(following);
            db.SaveChanges();


            return Ok();

        }
    }
}
