﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace petogram.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Location { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}