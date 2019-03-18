namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedNullFromRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Ratingid = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Ratingid)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            AlterColumn("dbo.Events", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "EventId", "dbo.Events");
            DropIndex("dbo.Ratings", new[] { "EventId" });
            AlterColumn("dbo.Events", "Rating", c => c.Int());
            DropTable("dbo.Ratings");
        }
    }
}
