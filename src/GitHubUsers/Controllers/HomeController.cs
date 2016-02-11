using System.Web.Mvc;

namespace GitHubUsers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "GitHubUsers";

            return View("Index");
        }
    }
}
