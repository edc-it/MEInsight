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
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace MEL.Web.Controllers
{
    [Authorize]
    public class TLMDistributionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _environment;

        public TLMDistributionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        // GET: TLMDistributions
        public async Task<IActionResult> Index(int? id)
        {
        
            ViewData["NextController"] = "TLMDistributionDetails";
            ViewData["ParentController"] = "TLMDistributionPeriods";
            ViewData["ParentId"] = id;
            
            // TLM Distribution List
            var applicationDbContext = _context.TLMDistributions
                .Include(t => t.TLMDistributionStatus)
                .Include(t => t.OrganizationsFrom)
                .Include(t => t.OrganizationsTo)
                .Include(t => t.ParentTLMDistributions)
                .Include(t => t.TLMDistributionPeriods)
                .Where(t => t.TLMDistributionPeriodId == id);

            // TLM Distribution Period Details
            var distributionPeriod = await _context.TLMDistributionPeriods
                .Where(t => t.TLMDistributionPeriodId == id).FirstOrDefaultAsync();

            // Returns distributionPeriod object
            ViewData["DistributionPeriod"] = distributionPeriod;

            ViewData["Closed"] = distributionPeriod.Closed;

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TLMDistributions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMDistribution = await _context.TLMDistributions
                .Include(t => t.TLMDistributionStatus)
                .Include(t => t.OrganizationsFrom)
                .Include(t => t.OrganizationsTo)
                .Include(t => t.ParentTLMDistributions)
                .Include(t => t.TLMDistributionPeriods)
                .FirstOrDefaultAsync(m => m.TLMDistributionId == id);
            
            if (tLMDistribution == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = tLMDistribution.TLMDistributionPeriodId;
            ViewData["Closed"] = tLMDistribution.TLMDistributionPeriods.Closed;

            return View(tLMDistribution);
        }

        // GET: TLMDistributions/Create
        [Authorize(Policy = "RequireCreateRole")]
        public async Task<IActionResult> Create(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            // *** For JSTree ***
            //Logged User OrganizationId
            Guid? userOrganizacionId = (await _userManager.GetUserAsync(HttpContext.User))?.OrganizationId;

            if (userOrganizacionId == null)
            {
                return NotFound();
            }

            //Get Logged User Organization
            var organization = await _context.Organizations
                    .SingleOrDefaultAsync(o => o.OrganizationId == userOrganizacionId);

            if (organization == null)
            {
                return NotFound();
            }

            //Returns the top hiearchy organization for JSTree
            ViewData["ParentOrganizationId"] = organization.OrganizationId;



            ViewData["ParentId"] = id;
            ViewData["RefTLMDistributionStatusId"] = new SelectList(_context.TLMDistributionStatus, "RefTLMDistributionStatusId", "DistributionStatus");
            ViewData["OrganizationIdFrom"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode");
            ViewData["OrganizationIdTo"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode");
            ViewData["ParentTLMDistributionId"] = new SelectList(_context.TLMDistributions, "TLMDistributionId", "TrackingCode");
            ViewData["TLMDistributionPeriodId"] = new SelectList(_context.TLMDistributionPeriods, "TLMDistributionPeriodId", "PeriodName");
            return View();
        }

        // POST: TLMDistributions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TLMDistributionId,RegistrationDate,TLMDistributionPeriodId,OrganizationIdFrom,OrganizationIdTo,ShippedDate,ReceivedDate,ReceivedBy,TrackingCode,RefTLMDistributionStatusId,Url,FileName,ParentTLMDistributionId")] TLMDistribution tLMDistribution)
        {
            if (ModelState.IsValid)
            {
                tLMDistribution.TLMDistributionId = Guid.NewGuid();
                _context.Add(tLMDistribution);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), new { id = tLMDistribution.TLMDistributionPeriodId });
            }
            ViewData["RefTLMDistributionStatusId"] = new SelectList(_context.TLMDistributionStatus, "RefTLMDistributionStatusId", "DistributionStatus", tLMDistribution.RefTLMDistributionStatusId);
            ViewData["OrganizationIdFrom"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", tLMDistribution.OrganizationIdFrom);
            ViewData["OrganizationIdTo"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", tLMDistribution.OrganizationIdTo);
            ViewData["ParentTLMDistributionId"] = new SelectList(_context.TLMDistributions, "TLMDistributionId", "TrackingCode", tLMDistribution.ParentTLMDistributionId);
            ViewData["TLMDistributionPeriodId"] = new SelectList(_context.TLMDistributionPeriods, "TLMDistributionPeriodId", "PeriodName", tLMDistribution.TLMDistributionPeriodId);
            ViewData["ParentId"] = tLMDistribution.TLMDistributionPeriodId;
            return View(tLMDistribution);
        }

        // GET: TLMDistributions/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMDistribution = await _context.TLMDistributions.FindAsync(id);

            if (tLMDistribution == null)
            {
                return NotFound();
            }

            // *** For JSTree ***
            //Logged User OrganizationId
            Guid? userOrganizacionId = (await _userManager.GetUserAsync(HttpContext.User))?.OrganizationId;

            if (userOrganizacionId == null)
            {
                return NotFound();
            }

            //Get Logged User Organization
            var organization = await _context.Organizations
                    .SingleOrDefaultAsync(o => o.OrganizationId == userOrganizacionId);

            if (organization == null)
            {
                return NotFound();
            }

            //Returns the top hiearchy organization for JSTree
            ViewData["ParentOrganizationId"] = organization.OrganizationId;

            ViewData["ParentId"] = tLMDistribution.TLMDistributionPeriodId;
            ViewData["RefTLMDistributionStatusId"] = new SelectList(_context.TLMDistributionStatus, "RefTLMDistributionStatusId", "DistributionStatus", tLMDistribution.RefTLMDistributionStatusId);
            ViewData["OrganizationIdFrom"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", tLMDistribution.OrganizationIdFrom);
            ViewData["OrganizationIdTo"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", tLMDistribution.OrganizationIdTo);
            ViewData["ParentTLMDistributionId"] = new SelectList(_context.TLMDistributions, "TLMDistributionId", "TrackingCode", tLMDistribution.ParentTLMDistributionId);
            ViewData["TLMDistributionPeriodId"] = new SelectList(_context.TLMDistributionPeriods, "TLMDistributionPeriodId", "PeriodName", tLMDistribution.TLMDistributionPeriodId);
            return View(tLMDistribution);
        }

        // POST: TLMDistributions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TLMDistributionId,RegistrationDate,TLMDistributionPeriodId,OrganizationIdFrom,OrganizationIdTo,ShippedDate,ReceivedDate,ReceivedBy,TrackingCode,RefTLMDistributionStatusId,Url,FileName,ParentTLMDistributionId")] TLMDistribution tLMDistribution)
        {
            if (id != tLMDistribution.TLMDistributionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tLMDistribution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLMDistributionExists(tLMDistribution.TLMDistributionId))
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

                //return RedirectToAction(nameof(Index));                
                return RedirectToAction(nameof(Index), new { id = tLMDistribution.TLMDistributionPeriodId });
            }
            ViewData["RefTLMDistributionStatusId"] = new SelectList(_context.TLMDistributionStatus, "RefTLMDistributionStatusId", "DistributionStatus", tLMDistribution.RefTLMDistributionStatusId);
            ViewData["OrganizationIdFrom"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", tLMDistribution.OrganizationIdFrom);
            ViewData["OrganizationIdTo"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", tLMDistribution.OrganizationIdTo);
            ViewData["ParentTLMDistributionId"] = new SelectList(_context.TLMDistributions, "TLMDistributionId", "TrackingCode", tLMDistribution.ParentTLMDistributionId);
            ViewData["TLMDistributionPeriodId"] = new SelectList(_context.TLMDistributionPeriods, "TLMDistributionPeriodId", "PeriodName", tLMDistribution.TLMDistributionPeriodId);
            ViewData["ParentId"] = tLMDistribution.TLMDistributionPeriodId;
            return View(tLMDistribution);
        }

        // GET: TLMDistributions/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMDistribution = await _context.TLMDistributions
                .Include(t => t.TLMDistributionStatus)
                .Include(t => t.OrganizationsFrom)
                .Include(t => t.OrganizationsTo)
                .Include(t => t.ParentTLMDistributions)
                .Include(t => t.TLMDistributionPeriods)
                .Include(t => t.TLMDistributionDetails)
                    .FirstOrDefaultAsync(m => m.TLMDistributionId == id);

            if (tLMDistribution == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
            relatedCount += tLMDistribution.TLMDistributionDetails.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            ViewData["ParentId"] = tLMDistribution.TLMDistributionPeriodId;

            return View(tLMDistribution);
        }

        // POST: TLMDistributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tLMDistribution = await _context.TLMDistributions.FindAsync(id);

            _context.TLMDistributions.Remove(tLMDistribution);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            //return RedirectToAction(nameof(Index));        
            return RedirectToAction(nameof(Index), new { id = tLMDistribution.TLMDistributionPeriodId });
        }

        /// <summary>
        /// Uploads a document related to the TLM Distribution
        /// i.e. a Scanned PDF with the Waybill or Delivery note
        /// </summary>
        /// <param name="file">File to upload</param>
        /// <param name="id">Group Id</param>
        /// <returns></returns>
        [HttpPost, ActionName("Upload")]
        public async Task<IActionResult> Upload(IFormFile file, Guid? id)
        {
            if (file == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            TLMDistribution tLMDistribution = await _context.TLMDistributions.FindAsync(id);

            if (tLMDistribution == null)
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
                    tLMDistribution.FileName = fileName;
                    tLMDistribution.Url = url;
                    _context.Update(tLMDistribution);

                    TempData["messageType"] = "success";
                    TempData["messageTitle"] = "RECORDS UPDATED";
                    TempData["message"] = "Records updated successfully";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLMDistributionExists(tLMDistribution.TLMDistributionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = tLMDistribution.TLMDistributionId });
            }

            TempData["messageType"] = "error";
            TempData["messageTitle"] = "FILE NOT UPLOADED";
            TempData["message"] = "A file could not be uploaded error";

            return RedirectToAction("Details", "TLMDistributions", new { id });
        }

        private bool TLMDistributionExists(Guid id)
        {
            return _context.TLMDistributions.Any(e => e.TLMDistributionId == id);
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
