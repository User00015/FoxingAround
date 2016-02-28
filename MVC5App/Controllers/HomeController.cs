using System.Web.Helpers;
using System.Web.Mvc;
using MVC5App.DynamoDb;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MVC5App.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

    }
}