using System.Web.Mvc;

namespace Notifications.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
            => View(new Model.Message());
    }
}