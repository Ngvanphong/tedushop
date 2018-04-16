namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedata78 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostCategories", "HomeOrder", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostCategories", "HomeOrder");
        }
    }
}
