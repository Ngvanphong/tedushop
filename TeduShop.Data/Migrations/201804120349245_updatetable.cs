namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductTags", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.ProductTags", "Product_ID1", "dbo.Products");
            DropIndex("dbo.ProductTags", new[] { "Product_ID" });
            DropIndex("dbo.ProductTags", new[] { "Product_ID1" });
            AddColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "SizeId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "SizeId");
            AddForeignKey("dbo.OrderDetails", "SizeId", "dbo.Sizes", "ID", cascadeDelete: true);
            DropColumn("dbo.ProductTags", "Product_ID");
            DropColumn("dbo.ProductTags", "Product_ID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductTags", "Product_ID1", c => c.Int());
            AddColumn("dbo.ProductTags", "Product_ID", c => c.Int());
            DropForeignKey("dbo.OrderDetails", "SizeId", "dbo.Sizes");
            DropIndex("dbo.OrderDetails", new[] { "SizeId" });
            DropColumn("dbo.OrderDetails", "SizeId");
            DropColumn("dbo.OrderDetails", "Price");
            CreateIndex("dbo.ProductTags", "Product_ID1");
            CreateIndex("dbo.ProductTags", "Product_ID");
            AddForeignKey("dbo.ProductTags", "Product_ID1", "dbo.Products", "ID");
            AddForeignKey("dbo.ProductTags", "Product_ID", "dbo.Products", "ID");
        }
    }
}
