namespace petogram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikeCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "LikeCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "LikeCount");
        }
    }
}
