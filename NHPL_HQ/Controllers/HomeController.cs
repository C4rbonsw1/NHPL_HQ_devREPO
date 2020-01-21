using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) {
                return Redirect("~/Account/Login");
            }
                return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Staff()
        {
            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public ActionResult Supervisor()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "General Manager")]
        public ActionResult GeneralManager()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Practice Manager")]
        public ActionResult PracticeManager()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "President")]
        public ActionResult President()
        {
            return View();
        }

    }
}
