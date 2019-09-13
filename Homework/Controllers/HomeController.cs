using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml;
using iTextSharp;
using iTextSharp.tool;
using System.IO;
using Homework.Models;


namespace Homework.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var PDFsList = new List<PDFObject>();
            var data = DBManager.GetData("Select * FROM PDFsTable");
            if (data.HasRows)
            {
                while (data.Read())
                    PDFsList.Add(new PDFObject() { ID = (int)data["ID"], Name = (string)data["Name"], Path = (string)data["Path"] });
            }
            else
            {
                DBManager.InsertData("TRUNCATE TABLE PDFsTABLE");
            }
            return View(PDFsList);
        }
        [HttpPost]
        public ActionResult Index(string editor)
        {
            Byte[] bytes;
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        using (var srHTML = new StringReader(editor))
                        {
                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHTML);

                        }
                        doc.Close();
                    }

                }
                bytes = ms.ToArray();

            }
            var testFile = Path.Combine(@"C:\Users\PANTHRAX\source\repos\Homework\Homework\PDF's", "test.pdf");
            System.IO.File.WriteAllBytes(testFile, bytes);
            return View();

        }
        [HttpGet]
        public ActionResult AddPDF()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPDF(string editor, string pdfname)
        {
            Byte[] bytes;
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        using (var srHTML = new StringReader(editor))
                        {
                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHTML);

                        }
                        doc.Close();
                    }

                }
                bytes = ms.ToArray();

            }

            var testFile = Path.Combine(@"C:\Users\PANTHRAX\source\repos\Homework\Homework\PDFs", string.Format("{0}{1}", pdfname, ".pdf"));
            DBManager.InsertData(string.Format("INSERT INTO PDFsTable VALUES ('{0}','{1}')", pdfname, testFile));

            System.IO.File.WriteAllBytes(testFile, bytes);

            return RedirectToAction("Index");
        }

        public ActionResult DeletePDF(int ID)
        {
            var data=DBManager.GetData(string.Format("SELECT * FROM PDFsTable WHERE ID={0}", ID));

            while (data.Read())
            System.IO.File.Delete((string)data["Path"]);
            DBManager.InsertData(string.Format("DELETE FROM PDFsTable WHERE ID={0};",ID));
            return RedirectToAction("Index");
        }
        public ActionResult PreviewPDF(int ID)
        {
            var data = DBManager.GetData(string.Format("SELECT * FROM PDFsTable WHERE ID={0}", ID));
            data.Read();
            return View(data["Path"]); 

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}