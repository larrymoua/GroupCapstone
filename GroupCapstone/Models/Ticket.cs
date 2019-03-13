using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupCapstone.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
    
        public Event Events { get; set; }
        public int GuestId { get; set; }
        [ForeignKey("GuestId")]
        public virtual Guest Guests { get; set; }

    }
}