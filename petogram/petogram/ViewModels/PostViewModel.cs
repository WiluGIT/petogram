using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace petogram.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        [DisplayName("Upload File")]
        public string ImgPath { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
    }
}