using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupCapstone.Models
{
    public class Ratings
    {
        [Key]
        public int Ratingid {get; set;}
        public int Rating { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public virtual Event RatedEvent { get; set; }

    }
}