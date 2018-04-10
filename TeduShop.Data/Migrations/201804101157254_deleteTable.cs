namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SupportOnlines", "Department");
            DropColumn("dbo.SupportOnlines", "Yahoo");
            DropTable("dbo.SuytemConfigs");
            DropTable("dbo.VisitorStatistic");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VisitorStatistic",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        VisitorDate = c.DateTime(nullable: false),
                        IPAddress = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SuytemConfigs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 256),
                        ValueString = c.String(maxLength: 256),
                        ValueInt = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.SupportOnlines", "Yahoo", c => c.String(maxLength: 256));
            AddColumn("dbo.SupportOnlines", "Department", c => c.String(maxLength: 256));
        }
    }
}
