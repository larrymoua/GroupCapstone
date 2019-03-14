using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupCapstone.Models
{
    public class Comments
    {
        [Key]

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Add Comment")]
        public string Comment { get; set; }

        public DateTime Date { get; set; }
    }
}