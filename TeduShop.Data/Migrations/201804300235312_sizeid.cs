namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sizeid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "CustomerEmail", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "CustomerEmail", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
