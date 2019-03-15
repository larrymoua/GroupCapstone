using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GroupCapstone.Models
{   
        public enum States
        {
            [Description("AL")]
            Alabama,
            [Description("AK")]
            Alaska,
            [Description("AZ")]
            Arizona,
            [Description("AR")]
            Arkansas,
            [Description("CA")]
            California,
            [Description("CO")]
            Colorado,
            [Description("CT")]
            Connecticut,
            [Description("DE")]
            Delaware,
            [Description("FL")]
            Florida,
            [Description("GA")]
            Georgia,
            [Description("HI")]
            Hawaii,
            [Description("ID")]
            Idaho,
            [Description("IL")]
            Illinois,
            [Description("IN")]
            Indiana,
            [Description("IA")]
            Iowa,
            [Description("KS")]
            Kansas,
            [Description("KY")]
            Kentucky,
            [Description("LA")]
            Louisiana,
            [Description("ME")]
            Maine,
            [Description("MD")]
            Maryland,
            [Description("MA")]
            Massachusetts,
            [Description("MT")]
            Michigan,
            [Description("MN")]
            Minnesota,
            [Description("MS")]
            Mississippi,
            [Description("MO")]
            Missouri,
            [Description("MT")]
            Montana,
            [Description("NE")]
            Nebraska,
            [Description("NV")]
            Nevada,
            [Description("NH")]
            NewHampshire,
            [Description("NJ")]
            NewJersey,
            [Description("NM")]
            NewMexico,
            [Description("NY")]
            NewYork,
            [Description("NC")]
            NorthCarolina,
            [Description("ND")]
            NorthDakota,
            [Description("OH")]
            Ohio,
            [Description("OK")]
            Oklahoma,
            [Description("OR")]
            Oregon,
            [Description("PA")]
            Pennsylvania,
            [Description("RI")]
            RhodeIsland,
            [Description("SC")]
            SouthCarolina,
            [Description("SD")]
            SouthDakota,
            [Description("TN")]
            Tennessee,
            [Description("TX")]
            Texas,
            [Description("UT")]
            Utah,
            [Description("VT")]
            Vermont,
            [Description("VA")]
            Virginia,
            [Description("WA")]
            Washington,
            [Description("WV")]
            WestVirginia,
            [Description("WI")]
            Wisconsin,
            [Description("WY")]
            Wyoming
        }
    public class AccessClass
    {
        public States State { get; set; }
    }
    
}