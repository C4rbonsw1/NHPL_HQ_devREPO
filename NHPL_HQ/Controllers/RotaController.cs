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

namespace NHPL_HQ.Controllers
{
    public class RotaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin, General Manager")]
        // GET: Rota
        public ActionResult Index()
        {
            ShiftRotaVM viewModel = new ShiftRotaVM();
            List<ShiftRotaVM> shiftVML = new List<ShiftRotaVM>();
            List<Shift> shiftList = db.Shifts.Where(x => x.ShiftAvailability == true && x.ShiftDate >= x.WeekStart && x.ShiftDate <= x.WeekEnd).ToList();
            foreach (Shift shift in shiftList) 
            {
                var dateNameString = shift.ShiftDate.ToString("ddd");
                if (dateNameString == "Mon")
                {
                    List<Shift> shiftMondayList = db.Shifts.ToList();
                    foreach (Shift shiftMonday in shiftMondayList)
                    {
                        shiftVML.Add(new ShiftRotaVM()
                        {
                            Shift = shiftMonday,
                        }
                        );
                    }
                    viewModel.ShiftDatesMonday = shiftMondayList;
                }
                shiftVML.Add(new ShiftRotaVM()
                {
                    Shift = shift,
                }
                ); 
            }

            List<ShiftRotaVM> rotaVML = new List<ShiftRotaVM>();
            List<Rota> rotaList = db.Rotas.ToList();
            foreach (Rota rota in rotaList)
            {
                shiftVML.Add(new ShiftRotaVM()
                {
                    Rota = rota,
                }
                );
            }

            ShiftRotaVM shiftRotaVM = new ShiftRotaVM();
            
            viewModel.ShiftViewModel = shiftList;
            viewModel.RotaViewModel = rotaList;


            return View(viewModel);
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
            ViewBag.Message = "File caught successfully!!";
            foreach (HttpPostedFileBase file in files)
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
                Document activeDocument = applicationclass.Documents.Open(filePath);
                //applicationclass.Visible = false;


                System.Data.DataSet dtSetResult = new System.Data.DataSet(); 
                foreach (Microsoft.Office.Interop.Word.Table tb in activeDocument.Tables)
                {
                    Rota rota = new Rota();

                    System.Data.DataTable dtResult = new System.Data.DataTable();
                    int rowscount = tb.Rows.Count;
                    int columnscount = tb.Columns.Count;
                    for (int i = 0; i < rowscount; i++)
                    {
                        // Create the row outside the inner loop, only want a new table row for each GridView row
                        DataRow dataRow = dtResult.NewRow();
                        for (int j = 0; j < columnscount; j++)
                        {
                            var cell = tb.Cell(i, j);
                            var text = cell.Range.Text;
                            if (tb.Columns.Count <= 1) 
                            {
                                rota.Name = text;
                                //var rotaNameInDb = db.Rotas.SingleOrDefault(i => i.Id == text.Length)
                            }

                            //Referencing the column in the new row by number, starting from 0.
                            //dataRow[j] = tb.Rows[i].ToString();

                        }
                        //dtResult.Rows.Add(dataRow);
                    }
                   
                }
                Document document = applicationclass.ActiveDocument;
                document.Close();
                System.IO.File.Delete(filePath.ToString());
                return View();
            }
            return View();
        }
    }
}

                //    System.Data.DataTable dtResult = new System.Data.DataTable();
                //    int rowsCount = tb.Rows.Count;
                //    int columnsCount = tb.Columns.Count;
                //    for (int i = 0; i < rowsCount; i++) 
                //    {
                //        DataRow dRow = dtResult.NewRow();
                //        var cell = tb.Cell(i, 1);
                //        var text = cell.Range.Text;
                //
                //        for (int j = 0; j < columnsCount; j++) 
                //        {
                //            var headingCell = tb.Cell(i, j);
                //            var headingText = cell.Range.Text;
                //            dtResult.Rows.Add(headingText);
                //            //dRow[j - 1] = tb.Rows[1].Cells[j].ToString();
                //        }
                //        dtResult.Rows.Add(text);
                //    }
                    //foreach (Row row in tb.Rows)
                    //{
                    //    for (int aRow = 1; aRow <= tb.Rows.Count; aRow++)
                    //    {
                    //        //int columnCount = tb.Columns.Count;
                    //        var cell = tb.Cell(aRow, 1);
                    //        var text = cell.Range.Text;
                    //    }
                    //}
                    //foreach (Cell aCell in row.Cells)
                    //{
                    //    var cell = tb.Cell(row, 1);
                    //
                    //    //data.Add(aCellText = aCell.Range.Text.Trim());
                    //    //ViewBag.Application = data.ToString();
                    //    return View(data);
                    //}

                    //ViewBag.Raw = dtResult;