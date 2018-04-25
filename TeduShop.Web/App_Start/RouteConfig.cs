using System.Web.Mvc;
using System.Web.Routing;

namespace TeduShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
          name: "Contact",
          url: "contact.html",
          defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
           namespaces: new string[] { "TeduShop.Web.Controllers" }
      );
            routes.MapRoute(
            name: "Promotion Product",
            url: "promotion-product.html",
            defaults: new { controller = "Product", action = "PromotionProduct", id = UrlParameter.Optional },
             namespaces: new string[] { "TeduShop.Web.Controllers" }
        );
            routes.MapRoute(
             name: "Hot Product",
             url: "hotproduct.html",
             defaults: new { controller = "Product", action = "HotProduct", id = UrlParameter.Optional },
              namespaces: new string[] { "TeduShop.Web.Controllers" }
         );

            routes.MapRoute(
              name: "Search Product",
              url: "search.html",
              defaults: new { controller = "Product", action = "SearchProduct", id = UrlParameter.Optional },
               namespaces: new string[] { "TeduShop.Web.Controllers" }
          );
            routes.MapRoute(
               name: "Product Caterory",
               url: "{alias}.pc-{id}.html",
               defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
           );
           
            routes.MapRoute(
                name: "Product Detail",
                url: "{alias}.p-{id}.html",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                 namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Post Category",
                url: "{alias}.blog-{id}.html",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional },
                 namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Post Detail",
               url: "{alias}.blog-detail-{id}.html",
               defaults: new { controller = "Post", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
           );


            routes.MapRoute(
                name: "Home",
                url: "index.html",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new string[] { "TeduShop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new string[] { "TeduShop.Web.Controllers" }
            );
        }
    }
}