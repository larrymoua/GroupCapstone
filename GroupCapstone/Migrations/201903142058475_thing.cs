namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        GuestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventId, t.GuestId })
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
                        State = c.Int(nullable: false),
                        Zip = c.Int(nullable: false),
                        Rating = c.Int(),
                        TicketsAvailable = c.Int(nullable: false),
                        TicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImagePath = c.String(),
                        Category = c.Int(nullable: false),
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
                        AvgRating = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HolderId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Zip = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GuestId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        GuestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventId, t.GuestId })
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.GuestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.Tickets", "EventId", "dbo.Events");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Bookmarks", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.Guests", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookmarks", "EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "HolderId", "dbo.EventHolders");
            DropForeignKey("dbo.EventHolders", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Tickets", new[] { "GuestId" });
            DropIndex("dbo.Tickets", new[] { "EventId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Guests", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.EventHolders", new[] { "ApplicationUserId" });
            DropIndex("dbo.Events", new[] { "HolderId" });
            DropIndex("dbo.Bookmarks", new[] { "GuestId" });
            DropIndex("dbo.Bookmarks", new[] { "EventId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Guests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.EventHolders");
            DropTable("dbo.Events");
            DropTable("dbo.Bookmarks");
        }
    }
}
