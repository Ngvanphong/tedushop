namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIcollection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTags", "Product_ID", c => c.Int());
            AddColumn("dbo.ProductTags", "Product_ID1", c => c.Int());
            CreateIndex("dbo.ProductTags", "Product_ID");
            CreateIndex("dbo.ProductTags", "Product_ID1");
            AddForeignKey("dbo.ProductTags", "Product_ID", "dbo.Products", "ID");
            AddForeignKey("dbo.ProductTags", "Product_ID1", "dbo.Products", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTags", "Product_ID1", "dbo.Products");
            DropForeignKey("dbo.ProductTags", "Product_ID", "dbo.Products");
            DropIndex("dbo.ProductTags", new[] { "Product_ID1" });
            DropIndex("dbo.ProductTags", new[] { "Product_ID" });
            DropColumn("dbo.ProductTags", "Product_ID1");
            DropColumn("dbo.ProductTags", "Product_ID");
        }
    }
}
