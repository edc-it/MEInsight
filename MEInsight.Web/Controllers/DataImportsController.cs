using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MEInsight.Entities.Programs;
using MEInsight.Web.Data;
using MEInsight.Web.Extensions;

namespace MEInsight.Web.Controllers
{
    public class DataImportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DataImportsController> _logger;
        private IWebHostEnvironment _environment;

        public DataImportsController(ApplicationDbContext context, ILogger<DataImportsController> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        // GET: DataImports
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Uploads an Excel formatted template related to the GroupId

        /// </summary>
        /// <param name="file">File to upload</param>
        /// <param name="id">Group Id</param>
        /// <returns></returns>
        [HttpPost, ActionName("Upload")]
        public IActionResult Upload(IFormFile file/*, Guid? id*/)
        {
            if (file == null)
            {
                TempData["messageType"] = "error";
                TempData["messageTitle"] = "FILE NOT UPLOADED";
                TempData["message"] = "A file could not be uploaded error";

                return RedirectToAction("Index", "DataImports", new { /*id*/ });
            }

            // TODO
            // Checks GroupId to import into
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //Group group = await _context.Groups.FindAsync(id);

            //if (group == null)
            //{
            //    return NotFound();
            //}

            long size = file.Length;

            if (size > 0)
            {
                // Limit file uploads to 50M
                if (size > 52428800)
                {
                    TempData["messageType"] = "error";
                    TempData["messageTitle"] = "FILE NOT UPLOADED";
                    TempData["message"] = "File size exceeded the maximum size permitted (50MB)";
                    return RedirectToAction("Index", "DataImports", new { /*id*/ });
                }

                //// Limit file types to .jpg, .jpeg, .png, .doc, and .pdf only
                if (!CheckFileType(file.FileName))
                {
                    TempData["messageType"] = "error";
                    TempData["messageTitle"] = "FILE NOT UPLOADED";
                    TempData["message"] = "File type not valid. Upload Excel xlsx documents only";
                    return RedirectToAction("Index", "DataImports", new { /*id*/});
                }

                ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupName");

                // Read Excel file
                var excelResult = HelperExtensions.ReadExcelSheet(file);

                return View(excelResult);
            }

            TempData["messageType"] = "error";
            TempData["messageTitle"] = "FILE NOT UPLOADED";
            TempData["message"] = "A file could not be uploaded error";

            return RedirectToAction("Index", "DataImports", new { /*id*/ });

        }

        #region Helpers
        //Allowed file upload types
        static bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".xlsx":
                    return true;
                //case ".jpg":
                //    return true;
                //case ".jpeg":
                //    return true;
                //case ".png":
                //    return true;
                //case ".doc":
                //    return true;
                //case ".docx":
                //    return true;
                //case ".pdf":
                //    return true;
                default:
                    return false;
            }
        }
        #endregion
    }
}
