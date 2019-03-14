namespace GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePKFromBookmarks : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Bookmarks");
            AddPrimaryKey("dbo.Bookmarks", new[] { "EventId", "GuestId" });
            DropColumn("dbo.Bookmarks", "BookmarkId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookmarks", "BookmarkId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Bookmarks");
            AddPrimaryKey("dbo.Bookmarks", "BookmarkId");
        }
    }
}
