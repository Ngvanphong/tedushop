namespace DamvayShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemaxlenght : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SystemConfigs", "ValueString", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SystemConfigs", "ValueString", c => c.String(maxLength: 50));
        }
    }
}
