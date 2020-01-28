using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using System;
using Document = Microsoft.Office.Interop.Word.Document;

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
        public ActionResult UploadRota(List<HttpPostedFileBase> files, string txt)
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

                    /*Needs to save copy of original document to encrypted/secure folder*/

                    if (!System.IO.File.Exists(filePath))
                    {
                        return View("Error");
                    }
                    else
                    {
                        ViewBag.SuccessMessage += string.Format("{0} Uploaded!<br />", fileName);
                    }

                    _Application applicationclass = new Application();
                    var activeDocument = applicationclass.Documents.Open(filePath);
                    applicationclass.Visible = false;
                    Document document = applicationclass.ActiveDocument;
                    document.Close();

                    string wordHTML = System.IO.File.ReadAllText(filePath.ToString());
                    foreach (Match match in Regex.Matches(wordHTML, "<v:imagedata.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase))
                    {
                        wordHTML = Regex.Replace(wordHTML, match.Groups[1].Value, "Temp/" + match.Groups[1].Value);
                    }
                    System.IO.File.Delete(filePath.ToString());
                    ViewBag.WordHtml = wordHTML;
                }
            }
            return View();
        }
    }
}