namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.ProductCategories", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.PostCategories", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Posts", "CreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PostCategories", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductCategories", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
