using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
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
using NHPL_HQ.Models;
using IdentitySample.Models;
using System.Globalization;
using System.Data.Entity;

namespace NHPL_HQ.Controllers
{
    public class RotaController : Controller
    {
        public Shift shiftModel = new Shift();
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin, General Manager")]
        // GET: Rota
        public ActionResult Index()
        {
            ShiftRotaVM viewModel = new ShiftRotaVM();

            IEnumerable<Shift> shiftMondayList = db.Shifts.AsEnumerable().Where(x => x.ShiftDate.DayOfWeek == DayOfWeek.Monday).ToList();
            IEnumerable<Shift> shiftTeusdayList = db.Shifts.AsEnumerable().Where(x => x.ShiftDate.DayOfWeek == DayOfWeek.Tuesday).ToList();
            IEnumerable<Shift> shiftWednesdayList = db.Shifts.AsEnumerable().Where(x => x.ShiftDate.DayOfWeek == DayOfWeek.Wednesday).ToList();
            IEnumerable<Shift> shiftThursdayList = db.Shifts.AsEnumerable().Where(x => x.ShiftDate.DayOfWeek == DayOfWeek.Thursday).ToList();
            IEnumerable<Shift> shiftFridayList = db.Shifts.AsEnumerable().Where(x => x.ShiftDate.DayOfWeek == DayOfWeek.Friday).ToList();
            IEnumerable<Shift> shiftSaturdayList = db.Shifts.AsEnumerable().Where(x => x.ShiftDate.DayOfWeek == DayOfWeek.Saturday).ToList();
            IEnumerable<Shift> shiftSundayList = db.Shifts.AsEnumerable().Where(x => x.ShiftDate.DayOfWeek == DayOfWeek.Sunday).ToList();

            viewModel.ShiftDatesMonday = shiftMondayList;
            viewModel.ShiftDatesTeusday = shiftTeusdayList;
            viewModel.ShiftDatesWednesday = shiftWednesdayList;
            viewModel.ShiftDatesThursday = shiftThursdayList;
            viewModel.ShiftDatesFriday = shiftFridayList;
            viewModel.ShiftDatesSaturday = shiftSaturdayList;
            viewModel.ShiftDatesSunday = shiftSundayList;

            return View(viewModel);
        }

        [Authorize(Roles = "Admin, General Manager")]
        [HttpGet]
        public ActionResult UploadRota()
        {
            ShiftRotaVM viewModel = new ShiftRotaVM();
            return View();
        }

        [Authorize(Roles = "Admin, General Manager")]
        [HttpPost]
        public ActionResult UploadRota([Bind(Include = "Rota, Practice, Location, Employee, ShiftDate")]List<HttpPostedFileBase> files, string txt)
        {
            string path = Server.MapPath("~/Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            ViewBag.Message = "File caught successfully!!";
            foreach (HttpPostedFileBase file in files)
            {
                string fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(path + "\\" + fileName);
                file.SaveAs(filePath);
                if (!System.IO.File.Exists(filePath))
                {
                    return View("Error");
                }
                else
                {
                    ViewBag.SuccessMessage += string.Format("{0} Uploaded!<br />", fileName);
                }

                string input = fileName;
                int index = input.IndexOf("-");
                if (index > 0)
                    input = input.Substring(0, index);

                var rotaFile = new Models.File()
                {
                    FileName = fileName,
                    FileType = FileType.Document,
                    ContentType = file.ContentType,
                    PracticeName = input
                };

                //var practiceDbList = db.Practices.AsEnumerable();

                using (var reader = new System.IO.BinaryReader(file.InputStream))
                {
                    rotaFile.Content = reader.ReadBytes(file.ContentLength);
                }
                db.Files.Add(rotaFile);
                db.SaveChanges();


                _Application applicationclass = new Application();
                Document activeDocument = applicationclass.Documents.Open(filePath);

                ////applicationclass.Visible = false;
                //
                //int tablesCount = activeDocument.Tables.Count;
                //var rotaNameText = "";
                //
                //System.Data.DataSet dtSetResult = new System.Data.DataSet(); 
                //foreach (Microsoft.Office.Interop.Word.Table tb in activeDocument.Tables)
                //{
                //    int rowscount = tb.Rows.Count;
                //    int columnscount = tb.Columns.Count;
                //    System.Data.DataTable dtResult = new System.Data.DataTable();
                //   
                //    if (tb.Rows.Count == 1 && tb.Columns.Count == 1)
                //    {
                //        var cell = tb.Cell(0, 0);
                //        rotaNameText = cell.Range.Text;
                //        //rotaNameText = shiftModel.Practice.Name;
                //    }
                //    else
                //    {
                //        for (int i = 0; i < rowscount; i++)
                //        {
                //            string text = "";
                //            for (int j = 0; j < columnscount; j++)
                //            {
                //                if (i == 0 && j >= 1)
                //                {
                //                    var cell = tb.Cell(i, j);
                //                    text = cell.Range.Text;
                //                    shift.Location(text);
                //                }
                //                else if (i >= 1 && j == 0)
                //                {
                //                    var cell = tb.Cell(i, j);
                //                    text = cell.Range.Text;
                //                    db.Users.Attach(shift.Employee);
                //                    var employeeInDb = db.Shifts.Include(x => x.Employee).SingleOrDefault(x => x.Id == shift.Id);
                //                    if (employeeInDb != null)
                //                    {
                //                        db.Entry(employeeInDb).CurrentValues.SetValues(shift);
                //                        employeeInDb.Employee = shift.Employee;
                //                    }
                //                }
                //                else if(i >= 1 && j >= 1)
                //                {
                //                    var cell = tb.Cell(i, j);
                //                    text = cell.Range.Text;
                //                    shift.ShiftDate(text);
                //                }
                //                //rota.Name = text;
                //                //var rotaNameInDb = db.Rotas.SingleOrDefault(i => i.Id == text.Length)
                //                //Referencing the column in the new row by number, starting from 0.
                //                //dataRow[i] = tb.Rows[i].ToString();
                //            }
                //
                //            //dtResult.Rows.Add(dataRow);
                //        }
                //    }
                //}

                Document document = applicationclass.ActiveDocument;
                document.Close();
                System.IO.File.Delete(filePath.ToString());
                return View();
            }
            return View();
        }
    }
}