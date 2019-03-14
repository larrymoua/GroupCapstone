namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newattribute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Events", new[] { "CategoryId" });
            AddColumn("dbo.Events", "Cat", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "City", c => c.String(nullable: false));
          
       
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
            
            AddColumn("dbo.Events", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "City", c => c.String());
            AlterColumn("dbo.Events", "Street", c => c.String());
            DropColumn("dbo.Events", "Cat");
            CreateIndex("dbo.Events", "CategoryId");
            AddForeignKey("dbo.Events", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
