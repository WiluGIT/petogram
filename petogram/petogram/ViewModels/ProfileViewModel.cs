using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace petogram.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Biography { get; set; }

        [DisplayName("Upload Profile Picture")]
        public string ImgPath { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
    }
}