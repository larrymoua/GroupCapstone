namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "City", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "City", c => c.String());
            AlterColumn("dbo.Events", "Street", c => c.String());
        }
    }
}
