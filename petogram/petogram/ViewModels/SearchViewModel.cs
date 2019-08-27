using petogram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petogram.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<ApplicationUser> Profiles { get; set; }
        public ILookup<string,Following> Followings { get; set; }
    }
}