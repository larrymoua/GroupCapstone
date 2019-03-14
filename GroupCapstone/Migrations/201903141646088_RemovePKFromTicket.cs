namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePKFromTicket : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tickets");
            AddPrimaryKey("dbo.Tickets", new[] { "EventId", "GuestId" });
            DropColumn("dbo.Tickets", "TicketId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "TicketId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Tickets");
            AddPrimaryKey("dbo.Tickets", "TicketId");
        }
    }
}
