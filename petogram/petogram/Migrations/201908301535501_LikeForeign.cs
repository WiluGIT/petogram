namespace petogram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikeForeign : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Likes");
            AlterColumn("dbo.Likes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Likes", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Likes");
            AlterColumn("dbo.Likes", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Likes", new[] { "Id", "PostId", "UserId" });
        }
    }
}
