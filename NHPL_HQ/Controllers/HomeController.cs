using System.Linq;
using System.Web.Mvc;
using IdentitySample.Models;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        

        [HttpGet]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Account/Login");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Employee")) 
            {
                return Redirect("~/Home/Staff");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Dentist"))
            {
                return Redirect("~/Home/Staff");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Qualified Dental Nurse"))
            {
                return Redirect("~/Home/Staff");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Qualifying Dental Nurse"))
            {
                return Redirect("~/Home/Staff");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Receptionist"))
            {
                return Redirect("~/Home/Staff");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Practice Manager"))
            {
                return Redirect("~/Rota/Upload");
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("General Manager"))
            {
                return Redirect("~/Rota/Upload");
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
