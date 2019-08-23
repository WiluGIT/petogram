using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petogram.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }


        public ApplicationUser Owner { get; set; }
    }
}