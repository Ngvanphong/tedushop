namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsize : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductQuantities");
            AddPrimaryKey("dbo.ProductQuantities", new[] { "ProductId", "SizeId" });
            DropColumn("dbo.ProductQuantities", "ColorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductQuantities", "ColorId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.ProductQuantities");
            AddPrimaryKey("dbo.ProductQuantities", new[] { "ProductId", "SizeId", "ColorId" });
        }
    }
}
