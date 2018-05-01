namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changepament : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "TotalPayment", c => c.Decimal(precision: 12, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "TotalPayment", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
