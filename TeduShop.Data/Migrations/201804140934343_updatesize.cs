namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesize : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderDetails");
            AddPrimaryKey("dbo.OrderDetails", new[] { "OrderID", "ProductID", "SizeId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OrderDetails");
            AddPrimaryKey("dbo.OrderDetails", new[] { "OrderID", "ProductID" });
        }
    }
}
