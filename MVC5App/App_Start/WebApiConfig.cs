using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace MVC5App
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
            configuration.Routes.MapHttpRoute("DefaultApiWithId", "Api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
            configuration.Routes.MapHttpRoute("DefaultApiWithAction", "Api/{controller}/{action}");
            configuration.Routes.MapHttpRoute("DefaultApiGet", "Api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            configuration.Routes.MapHttpRoute("DefaultApiPost", "Api/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            configuration.Routes.MapHttpRoute("DefaultApiPut", "Api/{controller}", new { action = "Put" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });
            configuration.Routes.MapHttpRoute("DefaultApiOptions", "Api/{controller}", new { action = "Options" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Options) });
        }
    }
}