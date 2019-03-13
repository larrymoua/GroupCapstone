using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupCapstone.Models
{
    public class Bookmarks
    {
        [Key]
        public int BookmarkId { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]

        public virtual Event Events { get; set; }
        public int GuestId { get; set; }
        [ForeignKey("GuestId")]

        public virtual Guest Guests { get; set; }
    }
}