namespace petogram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikesFix : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IsLiked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.PostId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Posts", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "CommentCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropColumn("dbo.Posts", "CommentCount");
            DropColumn("dbo.Posts", "LikeCount");
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
        }
    }
}
