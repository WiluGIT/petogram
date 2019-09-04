using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace petogram.Models
{
    public class Comment
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [ForeignKey("Post")]
        [Column(Order = 2)]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [ForeignKey("User")]
        [Column(Order = 3)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Content { get; set; }
    }
}