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
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category categories { get; set; }
        public int HolderId { get; set; }
        [ForeignKey("HolderId")]
        public virtual EventHolder EventHolders { get; set; }
    }
    public enum States
    {
        Alabama,
        Alaska,
        Arizona,
        Arkansas,
        California,
        Colorado,
        Connecticut,
        Delaware,
        Florida,
        Georgia,
        Hawaii,
        Idaho,
        Illinois,
        Indiana,
        Iowa,
        Kansas,
        Kentucky,
        Louisiana,
        Maine,
        Maryland,
        Massachusetts,
        Michigan,
        Minnesota,
        Mississippi,
        Missouri,
        Montana ,
        Nebraska,
        Nevada,
        NewHampshire,
        NewJersey,
        NewMexico,
        NewYork,
        NorthCarolina,
        NorthDakota,
        Ohio,
        Oklahoma,
        Oregon,
        Pennsylvania ,
        RhodeIsland,
        SouthCarolina,
        SouthDakota,
        Tennessee,
        Texas,
        Utah,
        Vermont,
        Virginia,
        Washington,
        WestVirginia,
        Wisconsin,
        Wyoming

    }
}