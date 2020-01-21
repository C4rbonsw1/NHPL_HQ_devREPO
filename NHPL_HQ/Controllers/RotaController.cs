using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace NHPL_HQ.Controllers
{
    public class RotaController : Controller
    {
        [Authorize(Roles = "Admin, General Manager")]
        // GET: Rota
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, General Manager")]
        [HttpGet]
        public ActionResult UploadRota()
        {
            return View();
        }

        [Authorize(Roles = "Admin, General Manager")]
        [HttpPost]
        public ActionResult UploadRota(List<HttpPostedFileBase> files)
        {
            string path = Server.MapPath("~/Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (HttpPostedFileBase file in files)
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(path + "\\" + fileName);
                    file.SaveAs(filePath);
                    
                    if (!System.IO.File.Exists(filePath))
                    {
                        return View("Error");
                    }
                    ViewBag.SuccessMessage += string.Format("{0} Uploaded!<br />", fileName);
                }
            }
            return View();
        }
    }
}