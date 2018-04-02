namespace TeduShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TeduShop.Model.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TeduShop.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TeduShop.Data.TeduShopDbContext context)
        {
            CreateProdutCategorySamble(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));
            //var user = new ApplicationUser()
            //{
            //    UserName = "vanphong",
            //    Email = "ngvanphong2012@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "ngvanphong",
            //};
            //manager.Create(user, "1234567");
            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //};

            //var adminUser = manager.FindByEmail("ngvanphong2012@gmail.com");
            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            
        }
        private void CreateProdutCategorySamble(TeduShopDbContext dbContext)
        {
            if (dbContext.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>
            {
                new ProductCategory(){Name="Áo sơ mi", Alias="Ao-so-mi", Status=true },
                new ProductCategory(){Name="Áo thun", Alias="Ao-thun", Status=true },
                new ProductCategory(){Name="Đầm váy", Alias="Dam-vay", Status=true },
            };
                dbContext.ProductCategories.AddRange(listProductCategory);
                dbContext.SaveChanges();
            }
           
        }
    }
}
