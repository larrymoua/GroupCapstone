﻿using System;
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
        [Required]
        public DateTime EventDate{ get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public States State { get; set; }
        [Required]
        public int Zip { get; set; }
        public int? Rating { get; set; }
        [Required]
        public int TicketsAvailable { get; set; }
        [Required]
        public Decimal TicketPrice { get; set; }
       
        [Display(Name = "Category")]
        [Required]
        public Category Categories { get; set; }
        public int HolderId { get; set; }
        [ForeignKey("HolderId")]
        public virtual EventHolder EventHolders { get; set; }
    }   

}