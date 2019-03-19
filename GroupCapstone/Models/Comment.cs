using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupCapstone.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public virtual Event Events { get; set; }

    }
}