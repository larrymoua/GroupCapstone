using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupCapstone.Models
{
    public enum Category
    {
        Sports,
        Music,
        Networking,
        Other
    }
    public class AcessClass
    {
        public Category Category { get; set; }
    }
}