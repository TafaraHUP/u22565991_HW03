using System;
using u22565991_HW03.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Helpers;
using System.Collections;
using System.IO;
using Rotativa;


namespace u22565991_HW03.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities1 db = new LibraryEntities1();
        public ActionResult Home(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var viewModel = new CombinedViewModel
            {
                Students = db.students.OrderBy(s => s.studentId).ToPagedList(pageNumber, pageSize),
                Borrows = db.borrows.OrderBy(s => s.borrowId).ToPagedList(pageNumber, pageSize),
                Books = db.books.OrderBy(b => b.bookId).ToPagedList(pageNumber, pageSize)
            };

            return View(viewModel);
        }

        public ActionResult Maintain(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var viewModel = new CombinedViewModel
            {
                Authors = db.authors.OrderBy(s => s.authorId).ToPagedList(pageNumber, pageSize),
                Types = db.types.OrderBy(b => b.typeId).ToPagedList(pageNumber, pageSize),
                Borrows = db.borrows.OrderBy(b => b.borrowId).ToPagedList(pageNumber, pageSize)

            };

            return View(viewModel);
        }

        public ActionResult Report()
        {
            var studentBorrowingData = db.borrows
                .GroupBy(b => b.studentId)
                .Select(g => new StudentBorrowingData
                {
                    StudentId = g.Key ?? 0, 
            BorrowCount = g.Count()
                })
                .OrderByDescending(x => x.BorrowCount)
                .Take(10) 
                .ToList();

            var combinedViewModel = new CombinedViewModel
            {
                StudentBorrowingData = studentBorrowingData
            };

            return View(combinedViewModel);
        }

        public ActionResult SaveReport(string filename)
        {
            string pdfFilePath = Server.MapPath("~/App_Data/" + filename + ".pdf");
            var reportContent = GenerateReportContent(); // Function to generate report HTML content

            var pdf = new ActionAsPdf("ReportToPdf")
            {
                FileName = pdfFilePath
            };

            var fileContents = pdf.BuildPdf(ControllerContext);

            // Save the PDF file to the desired location
            System.IO.File.WriteAllBytes(pdfFilePath, fileContents);

            return File(fileContents, "application/pdf", filename + ".pdf");
        }

        public ActionResult ReportToPdf()
        {
            var reportContent = GenerateReportContent(); // Function to generate report HTML content
            return Content(reportContent, "text/html");
        }

        private string GenerateReportContent()
        {
            // Generate the HTML content for the report
            // Example:
            var htmlContent = "<html><body><h1>Sample Report</h1><p>This is a sample report content.</p></body></html>";

            return htmlContent;
        }

        public ActionResult DocumentArchive()
        {
            var reportDirectory = Server.MapPath("~/SavedReports");
            var files = Directory.GetFiles(reportDirectory);

            List<DocumentModel> documents = new List<DocumentModel>();
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                documents.Add(new DocumentModel { FileName = fileName, FilePath = file });
            }

            return View(documents);
        }


        public FileResult Download(string fileName)
        {
            var filePath = Path.Combine(Server.MapPath("~/SavedReports"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", fileName);
        }

        public ActionResult Delete(string fileName)
        {
            var filePath = Path.Combine(Server.MapPath("~/SavedReports"), fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return RedirectToAction("DocumentArchive");
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