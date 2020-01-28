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
            //else if (User.Identity.IsAuthenticated) 
            //{
            //    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Id = model.Role.Id};
            //    var userRole = dbContext.Roles.Where(x => x.Id == model.Role.Id).First();
            //    return Redirect("~/Home/Staff");
            //}
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
