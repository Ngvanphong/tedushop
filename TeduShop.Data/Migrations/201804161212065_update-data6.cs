namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedata6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        Path = c.String(maxLength: 250),
                        Caption = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            AddColumn("dbo.Posts", "Tags", c => c.String(maxLength: 256));
            DropColumn("dbo.Posts", "HotFlag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "HotFlag", c => c.Boolean());
            DropForeignKey("dbo.PostImages", "PostId", "dbo.Posts");
            DropIndex("dbo.PostImages", new[] { "PostId" });
            DropColumn("dbo.Posts", "Tags");
            DropTable("dbo.PostImages");
        }
    }
}
