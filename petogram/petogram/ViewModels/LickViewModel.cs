using petogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petogram.ViewModels
{
    public class LickViewModel
    {
        public string CommentContent { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public ILookup<int, Like> Likeings { get; set; }
    }
}