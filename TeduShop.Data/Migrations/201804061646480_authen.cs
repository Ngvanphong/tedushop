namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authen : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IdentityRoles", newName: "AppRoles");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "AppUserRoles");
            RenameTable(name: "dbo.ApplicationUsers", newName: "AppUsers");
            RenameTable(name: "dbo.IdentityUserClaims", newName: "AppUserClaims");
            RenameTable(name: "dbo.IdentityUserLogins", newName: "AppUserLogins");
            RenameColumn(table: "dbo.AppUserClaims", name: "ApplicationUser_Id", newName: "AppUser_Id");
            RenameColumn(table: "dbo.AppUserLogins", name: "ApplicationUser_Id", newName: "AppUser_Id");
            RenameColumn(table: "dbo.AppUserRoles", name: "ApplicationUser_Id", newName: "AppUser_Id");
            RenameIndex(table: "dbo.AppUserRoles", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
            RenameIndex(table: "dbo.AppUserClaims", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
            RenameIndex(table: "dbo.AppUserLogins", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
            DropPrimaryKey("dbo.AppUserClaims");
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        URL = c.String(nullable: false, maxLength: 256),
                        DisplayOrder = c.Int(nullable: false),
                        ParentId = c.String(maxLength: 50, unicode: false),
                        Status = c.Boolean(nullable: false),
                        IconCss = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Functions", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        FunctionId = c.String(maxLength: 50, unicode: false),
                        CanCreate = c.Boolean(nullable: false),
                        CanRead = c.Boolean(nullable: false),
                        CanUpdate = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppRoles", t => t.RoleId)
                .ForeignKey("dbo.Functions", t => t.FunctionId)
                .Index(t => t.RoleId)
                .Index(t => t.FunctionId);
            
            AddColumn("dbo.AppRoles", "Description", c => c.String());
            AddColumn("dbo.AppRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AppUsers", "Address", c => c.String(maxLength: 256));
            AddColumn("dbo.AppUsers", "Avatar", c => c.String());
            AddColumn("dbo.AppUsers", "Status", c => c.Boolean());
            AddColumn("dbo.AppUsers", "Gender", c => c.Boolean());
            AlterColumn("dbo.AppUserClaims", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AppUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AppUserClaims", "UserId");
            DropColumn("dbo.AppUsers", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppUsers", "Adress", c => c.String(maxLength: 256));
            DropForeignKey("dbo.Permissions", "FunctionId", "dbo.Functions");
            DropForeignKey("dbo.Permissions", "RoleId", "dbo.AppRoles");
            DropForeignKey("dbo.Functions", "ParentId", "dbo.Functions");
            DropIndex("dbo.Permissions", new[] { "FunctionId" });
            DropIndex("dbo.Permissions", new[] { "RoleId" });
            DropIndex("dbo.Functions", new[] { "ParentId" });
            DropPrimaryKey("dbo.AppUserClaims");
            AlterColumn("dbo.AppUserClaims", "UserId", c => c.String());
            AlterColumn("dbo.AppUserClaims", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.AppUsers", "Gender");
            DropColumn("dbo.AppUsers", "Status");
            DropColumn("dbo.AppUsers", "Avatar");
            DropColumn("dbo.AppUsers", "Address");
            DropColumn("dbo.AppRoles", "Discriminator");
            DropColumn("dbo.AppRoles", "Description");
            DropTable("dbo.Permissions");
            DropTable("dbo.Functions");
            AddPrimaryKey("dbo.AppUserClaims", "Id");
            RenameIndex(table: "dbo.AppUserLogins", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.AppUserClaims", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.AppUserRoles", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.AppUserRoles", name: "AppUser_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.AppUserLogins", name: "AppUser_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.AppUserClaims", name: "AppUser_Id", newName: "ApplicationUser_Id");
            RenameTable(name: "dbo.AppUserLogins", newName: "IdentityUserLogins");
            RenameTable(name: "dbo.AppUserClaims", newName: "IdentityUserClaims");
            RenameTable(name: "dbo.AppUsers", newName: "ApplicationUsers");
            RenameTable(name: "dbo.AppUserRoles", newName: "IdentityUserRoles");
            RenameTable(name: "dbo.AppRoles", newName: "IdentityRoles");
        }
    }
}
