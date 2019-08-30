namespace petogram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLike : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Like", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Like");
        }
    }
}