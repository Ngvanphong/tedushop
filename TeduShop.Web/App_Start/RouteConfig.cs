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