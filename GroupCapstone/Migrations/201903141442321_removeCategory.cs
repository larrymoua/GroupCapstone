namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Events", new[] { "CategoryId" });
            AddColumn("dbo.Events", "categories", c => c.Int(nullable: false));
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            DropColumn("dbo.Events", "categories");
            CreateIndex("dbo.Events", "CategoryId");
            AddForeignKey("dbo.Events", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
