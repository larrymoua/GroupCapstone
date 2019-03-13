namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zipAdding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "Zip", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "Zip");
        }
    }
}
