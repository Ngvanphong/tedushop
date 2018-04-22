namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateprice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 12, scale: 3));
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Decimal(precision: 12, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PromotionPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
