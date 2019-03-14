namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDisplayName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "CategoryId", c => c.Int(nullable: false));
        }
    }
}
