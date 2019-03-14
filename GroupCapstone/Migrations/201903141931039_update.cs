namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Category", c => c.Int(nullable: false));
            DropColumn("dbo.Events", "Cat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Cat", c => c.Int(nullable: false));
            DropColumn("dbo.Events", "Category");
        }
    }
}
