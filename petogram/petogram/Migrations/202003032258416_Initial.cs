namespace petogram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Likes");
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Posts", "CommentCount", c => c.Int(nullable: false));
            AlterColumn("dbo.Likes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Likes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropPrimaryKey("dbo.Likes");
            AlterColumn("dbo.Likes", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "CommentCount");
            DropTable("dbo.Comments");
            AddPrimaryKey("dbo.Likes", new[] { "Id", "PostId", "UserId" });
        }
    }
}
