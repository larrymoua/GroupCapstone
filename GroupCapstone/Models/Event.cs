using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GroupCapstone.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Display(Name = "Event's Name")]
        public string EventName { get; set; }
        [Display(Name = "Event's Date/Time")]
        public DateTime EventDate{ get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public States State { get; set; }
        public int Zip { get; set; }
        public int? Rating { get; set; }  
        public int TicketsAvailable { get; set; }
        public Decimal TicketPrice { get; set; }

        public string ImagePath { get; set; }
  
        public Category Category { get; set; }
        public int HolderId { get; set; }
        [ForeignKey("HolderId")]
        public virtual EventHolder EventHolders { get; set; }
    }   

}