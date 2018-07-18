using System.Web.Mvc;
using System.Web.Routing;
using WebApp.Controllers;

namespace WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: DefaultController.ControllerName,
                url: "{action}/{id}",
                defaults: new {
                    controller = DefaultController.ControllerName,
                    action = DefaultController.IndexActionName,
                    id = UrlParameter.Optional }
            );

            routes.MapMvcAttributeRoutes();
        }
    }
}
