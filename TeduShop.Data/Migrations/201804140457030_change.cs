namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "Quntity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Quntity", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "Quantity");
        }
    }
}
