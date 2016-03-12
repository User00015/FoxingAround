using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5App
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Default",
                url: "{*anything}", // THIS IS THE MAGIC!!!!
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
