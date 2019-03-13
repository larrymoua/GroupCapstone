namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newAttributes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Events", "Rating", c => c.Int());
            AddColumn("dbo.Events", "TicketsAvailable", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "TicketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Events", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.EventHolders", "AvgRating", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "State", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "CategoryId");
            AddForeignKey("dbo.Events", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Events", new[] { "CategoryId" });
            AlterColumn("dbo.Events", "State", c => c.String());
            DropColumn("dbo.EventHolders", "AvgRating");
            DropColumn("dbo.Events", "CategoryId");
            DropColumn("dbo.Events", "TicketPrice");
            DropColumn("dbo.Events", "TicketsAvailable");
            DropColumn("dbo.Events", "Rating");
            DropTable("dbo.Categories");
        }
    }
}
