﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupCapstone.Models
{
    public class Comment
    {
        public int UserID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

    }
}