using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MEInsight.Entities.Identity;
using MEInsight.Web.Data;
using MEInsight.Entities.TLM;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MEL.Web.Controllers
{
    [Authorize]
    public class TLMMaterialsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _environment;

        public TLMMaterialsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        // GET: TLMMaterials
        public async Task<IActionResult> Index()
        {
        
            ViewData["NextController"] = null;
            ViewData["ParentController"] = null;
            
            var applicationDbContext = _context.TLMMaterials
                .Include(t => t.GradeLevels)
                .Include(t => t.TLMGroups)
                .Include(t => t.TLMLanguages)
                .Include(t => t.TLMMaterialSets)
                .Include(t => t.TLMMaterialTypes)
                .Include(t => t.TLMSubjects);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TLMMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMMaterial = await _context.TLMMaterials
                .Include(t => t.GradeLevels)
                .Include(t => t.TLMGroups)
                .Include(t => t.TLMLanguages)
                .Include(t => t.TLMMaterialSets)
                .Include(t => t.TLMMaterialTypes)
                .Include(t => t.TLMSubjects)
                .FirstOrDefaultAsync(m => m.TLMMaterialId == id);
            
            if (tLMMaterial == null)
            {
                return NotFound();
            }

            //ViewData["ParentId"] = tLMMaterial.ParentId;

            return View(tLMMaterial);
        }

        // GET: TLMMaterials/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create()
        {
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel");
            ViewData["RefTLMGroupId"] = new SelectList(_context.TLMGroups, "RefTLMGroupId", "TLMGroup");
            ViewData["RefTLMLanguageId"] = new SelectList(_context.TLMLanguages, "RefTLMLanguageId", "TLMLanguage");
            ViewData["RefTLMMaterialSetId"] = new SelectList(_context.TLMMaterialSets, "RefTLMMaterialSetId", "TLMMaterialSet");
            ViewData["RefTLMMaterialTypeId"] = new SelectList(_context.TLMMaterialTypes, "RefTLMMaterialTypeId", "TLMMaterialType");
            ViewData["RefTLMSubjectId"] = new SelectList(_context.TLMSubjects, "RefTLMSubjectId", "TLMSubject");
            return View();
        }

        // POST: TLMMaterials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TLMMaterialId,TLMMaterialCode,TLMMaterialName,Description,RefTLMMaterialTypeId,RefGradeLevelId,RefTLMLanguageId,RefTLMSubjectId,RefTLMGroupId,RefTLMMaterialSetId,RatioNumerator,RatioDenominator,Url,FileName,Barcode")] TLMMaterial tLMMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tLMMaterial);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
                //return RedirectToAction(nameof(Index), new { id = tLMMaterial.ParentId });
            }
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel", tLMMaterial.RefGradeLevelId);
            ViewData["RefTLMGroupId"] = new SelectList(_context.TLMGroups, "RefTLMGroupId", "TLMGroup", tLMMaterial.RefTLMGroupId);
            ViewData["RefTLMLanguageId"] = new SelectList(_context.TLMLanguages, "RefTLMLanguageId", "TLMLanguage", tLMMaterial.RefTLMLanguageId);
            ViewData["RefTLMMaterialSetId"] = new SelectList(_context.TLMMaterialSets, "RefTLMMaterialSetId", "TLMMaterialSet", tLMMaterial.RefTLMMaterialSetId);
            ViewData["RefTLMMaterialTypeId"] = new SelectList(_context.TLMMaterialTypes, "RefTLMMaterialTypeId", "TLMMaterialType", tLMMaterial.RefTLMMaterialTypeId);
            ViewData["RefTLMSubjectId"] = new SelectList(_context.TLMSubjects, "RefTLMSubjectId", "TLMSubject", tLMMaterial.RefTLMSubjectId);
            //ViewData["ParentId"] = tLMMaterial.ParentId;
            return View(tLMMaterial);
        }

        // GET: TLMMaterials/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMMaterial = await _context.TLMMaterials.FindAsync(id);

            if (tLMMaterial == null)
            {
                return NotFound();
            }
            //ViewData["ParentId"] = tLMMaterial.ParentId;
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel", tLMMaterial.RefGradeLevelId);
            ViewData["RefTLMGroupId"] = new SelectList(_context.TLMGroups, "RefTLMGroupId", "TLMGroup", tLMMaterial.RefTLMGroupId);
            ViewData["RefTLMLanguageId"] = new SelectList(_context.TLMLanguages, "RefTLMLanguageId", "TLMLanguage", tLMMaterial.RefTLMLanguageId);
            ViewData["RefTLMMaterialSetId"] = new SelectList(_context.TLMMaterialSets, "RefTLMMaterialSetId", "TLMMaterialSet", tLMMaterial.RefTLMMaterialSetId);
            ViewData["RefTLMMaterialTypeId"] = new SelectList(_context.TLMMaterialTypes, "RefTLMMaterialTypeId", "TLMMaterialType", tLMMaterial.RefTLMMaterialTypeId);
            ViewData["RefTLMSubjectId"] = new SelectList(_context.TLMSubjects, "RefTLMSubjectId", "TLMSubject", tLMMaterial.RefTLMSubjectId);
            return View(tLMMaterial);
        }

        // POST: TLMMaterials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TLMMaterialId,TLMMaterialCode,TLMMaterialName,Description,RefTLMMaterialTypeId,RefGradeLevelId,RefTLMLanguageId,RefTLMSubjectId,RefTLMGroupId,RefTLMMaterialSetId,RatioNumerator,RatioDenominator,Url,FileName,Barcode")] TLMMaterial tLMMaterial)
        {
            if (id != tLMMaterial.TLMMaterialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tLMMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLMMaterialExists(tLMMaterial.TLMMaterialId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD UPDATED";
                TempData["message"] = "Record successfully updated";

                return RedirectToAction(nameof(Index));                
                //return RedirectToAction(nameof(Index), new { id = tLMMaterial.ParentId });
            }
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel", tLMMaterial.RefGradeLevelId);
            ViewData["RefTLMGroupId"] = new SelectList(_context.TLMGroups, "RefTLMGroupId", "TLMGroup", tLMMaterial.RefTLMGroupId);
            ViewData["RefTLMLanguageId"] = new SelectList(_context.TLMLanguages, "RefTLMLanguageId", "TLMLanguage", tLMMaterial.RefTLMLanguageId);
            ViewData["RefTLMMaterialSetId"] = new SelectList(_context.TLMMaterialSets, "RefTLMMaterialSetId", "TLMMaterialSet", tLMMaterial.RefTLMMaterialSetId);
            ViewData["RefTLMMaterialTypeId"] = new SelectList(_context.TLMMaterialTypes, "RefTLMMaterialTypeId", "TLMMaterialType", tLMMaterial.RefTLMMaterialTypeId);
            ViewData["RefTLMSubjectId"] = new SelectList(_context.TLMSubjects, "RefTLMSubjectId", "TLMSubject", tLMMaterial.RefTLMSubjectId);
            //ViewData["ParentId"] = tLMMaterial.ParentId;
            return View(tLMMaterial);
        }

        // GET: TLMMaterials/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMMaterial = await _context.TLMMaterials
                .Include(t => t.GradeLevels)
                .Include(t => t.TLMGroups)
                .Include(t => t.TLMLanguages)
                .Include(t => t.TLMMaterialSets)
                .Include(t => t.TLMMaterialTypes)
                .Include(t => t.TLMSubjects)
                .Include(t => t.TLMDistributionDetails)
                    .FirstOrDefaultAsync(m => m.TLMMaterialId == id);

            if (tLMMaterial == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                    relatedCount += tLMMaterial.TLMDistributionDetails.Count();

            if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            //ViewData["ParentId"] = tLMMaterial.ParentId;

            return View(tLMMaterial);
        }

        // POST: TLMMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tLMMaterial = await _context.TLMMaterials.FindAsync(id);

            _context.TLMMaterials.Remove(tLMMaterial);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            return RedirectToAction(nameof(Index));        
            //return RedirectToAction(nameof(Index), new { id = tLMMaterial.ParentId });
        }

        /// <summary>
        /// Uploads a document related to the TLM Material
        /// i.e. a Scanned PDF with the Material cover/image
        /// </summary>
        /// <param name="file">File to upload</param>
        /// <param name="id">Group Id</param>
        /// <returns></returns>
        [HttpPost, ActionName("Upload")]
        public async Task<IActionResult> Upload(IFormFile file, int? id)
        {
            if (file == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            TLMMaterial tLMMaterial = await _context.TLMMaterials.FindAsync(id);

            if (tLMMaterial == null)
            {
                return NotFound();
            }

            long size = file.Length;

            // Get folder path URL = ~/wwwroot/uploads
            var filePath = Path.Combine(_environment.WebRootPath, "uploads");

            if (size > 0)
            {
                // Limit file uploads to 50M
                if (size > 52428800)
                {
                    TempData["messageType"] = "error";
                    TempData["messageTitle"] = "FILE NOT UPLOADED";
                    TempData["message"] = "File size exceeded the maximum size permitted (50MB)";
                    return RedirectToAction("Details", "TLMDistributions", new { id });
                }

                // Limit file types to .jpg, .jpeg, .png, .doc, and .pdf only
                if (!CheckFileType(file.FileName))
                {
                    TempData["messageType"] = "error";
                    TempData["messageTitle"] = "FILE NOT UPLOADED";
                    TempData["message"] = "File type not valid. Upload PDF, Documents, or Images";
                    return RedirectToAction("Details", "TLMDistributions", new { id });
                }

                // Current file extension
                string extension = Path.GetExtension(file.FileName);

                // Set Filename format (i.e. Document-20190815141805-{id}{.pdf})
                var fileName = "Document" + DateTime.Now.ToString("-yyyyMMddHHmmss-") + id + extension;

                // Upload file to server
                using (var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Build host URL and /uploads/ path
                var request = HttpContext.Request;
                var uriBuilder = new UriBuilder
                {
                    Host = request.Host.Host,
                    Scheme = request.Scheme,
                    Path = "uploads/" + fileName
                };

                if (request.Host.Port.HasValue)
                    uriBuilder.Port = request.Host.Port.Value;

                var url = uriBuilder.ToString();

                // Update database with FileName and URL
                try
                {
                    tLMMaterial.FileName = fileName;
                    tLMMaterial.Url = url;
                    _context.Update(tLMMaterial);

                    TempData["messageType"] = "success";
                    TempData["messageTitle"] = "RECORDS UPDATED";
                    TempData["message"] = "Records updated successfully";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLMMaterialExists(tLMMaterial.TLMMaterialId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = tLMMaterial.TLMMaterialId });
            }

            TempData["messageType"] = "error";
            TempData["messageTitle"] = "FILE NOT UPLOADED";
            TempData["message"] = "A file could not be uploaded error";

            return RedirectToAction("Details", "TLMMaterials", new { id });
        }

        private bool TLMMaterialExists(int id)
        {
            return _context.TLMMaterials.Any(e => e.TLMMaterialId == id);
        }

        #region Helpers
        //Allowed file upload types
        bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                case ".doc":
                    return true;
                case ".docx":
                    return true;
                case ".pdf":
                    return true;
                default:
                    return false;
            }
        }
        #endregion
    }
}
