namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedIdentityModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        BookmarkId = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        GuestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookmarkId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.GuestId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.Int(nullable: false),
                        HolderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.EventHolders", t => t.HolderId, cascadeDelete: true)
                .Index(t => t.HolderId);
            
            CreateTable(
                "dbo.EventHolders",
                c => new
                    {
                        HolderId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CompanyName = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HolderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GuestId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        GuestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.GuestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.Tickets", "EventId", "dbo.Events");
            DropForeignKey("dbo.Bookmarks", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.Guests", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookmarks", "EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "HolderId", "dbo.EventHolders");
            DropForeignKey("dbo.EventHolders", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Tickets", new[] { "GuestId" });
            DropIndex("dbo.Tickets", new[] { "EventId" });
            DropIndex("dbo.Guests", new[] { "ApplicationUserId" });
            DropIndex("dbo.EventHolders", new[] { "ApplicationUserId" });
            DropIndex("dbo.Events", new[] { "HolderId" });
            DropIndex("dbo.Bookmarks", new[] { "GuestId" });
            DropIndex("dbo.Bookmarks", new[] { "EventId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Guests");
            DropTable("dbo.EventHolders");
            DropTable("dbo.Events");
            DropTable("dbo.Bookmarks");
        }
    }
}
