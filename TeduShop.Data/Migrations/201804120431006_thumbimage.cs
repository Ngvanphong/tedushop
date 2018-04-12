namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thumbimage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ThumbnailImage", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ThumbnailImage", c => c.String());
        }
    }
}
