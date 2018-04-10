namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "HomeOrder", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "HomeOrder");
        }
    }
}
