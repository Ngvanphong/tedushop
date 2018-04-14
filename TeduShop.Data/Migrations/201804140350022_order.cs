namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CustomerId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "CustomerId");
        }
    }
}
