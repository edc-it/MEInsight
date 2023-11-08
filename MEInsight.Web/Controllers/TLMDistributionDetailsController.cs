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


namespace MEL.Web.Controllers
{
    [Authorize]
    public class TLMDistributionDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TLMDistributionDetailsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TLMDistributionDetails
        public async Task<IActionResult> Index(Guid? id)
        {
        
            ViewData["NextController"] = null;
            ViewData["ParentController"] = "TLMDistributions";
            ViewData["ParentId"] = id;
            
            var applicationDbContext = _context.TLMDistributionDetails
                .Include(t => t.TLMDistributions)
                .Include(t => t.TLMMaterials).ThenInclude(t => t.TLMMaterialTypes)
                .Include(t => t.TLMMaterials).ThenInclude(t => t.TLMSubjects)
                .Include(t => t.TLMMaterials).ThenInclude(t => t.TLMLanguages)
                .Where(t => t.TLMDistributionId == id);

            // TLM Distribution Period Details
            var distribution = await _context.TLMDistributions
                .Include(t => t.TLMDistributionPeriods)
                .Include(t => t.OrganizationsFrom)
                .Include(t => t.OrganizationsTo)
                .Include(t => t.TLMDistributionStatus)
                .Where(t => t.TLMDistributionId == id)
                .FirstOrDefaultAsync();

            if (distribution != null)
            {
                // Returns distributionPeriod object
                ViewData["Distribution"] = distribution;

                ViewData["Closed"] = distribution.TLMDistributionPeriods.Closed;
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TLMDistributionDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMDistributionDetail = await _context.TLMDistributionDetails
                .Include(t => t.TLMDistributions)
                .Include(t => t.TLMMaterials)
                .FirstOrDefaultAsync(m => m.TLMDistributionDetailId == id);
            
            if (tLMDistributionDetail == null)
            {
                return NotFound();
            }

            // TLM Distribution Period Details
            var distributionPeriod = await _context.TLMDistributions
                .Where(t => t.TLMDistributionId == tLMDistributionDetail.TLMDistributionId)
                .Select(t => t.TLMDistributionPeriods)
                .FirstOrDefaultAsync();

            if (distributionPeriod != null)
            {
                // Returns distributionPeriod object
                ViewData["DistributionPeriod"] = distributionPeriod;

                ViewData["Closed"] = distributionPeriod.Closed;
            }

            ViewData["ParentId"] = tLMDistributionDetail.TLMDistributionId;

            return View(tLMDistributionDetail);
        }

        // GET: TLMDistributionDetails/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = id;
            ViewData["TLMMaterialId"] = new SelectList(_context.TLMMaterials
                .Select(x => new {
                    x.TLMMaterialId,
                    TLMMaterialName = x.TLMMaterialName + " (" + x.TLMMaterialCode + ")"
                }), "TLMMaterialId", "TLMMaterialName");
            return View();
        }

        // POST: TLMDistributionDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TLMDistributionDetailId,RegistrationDate,TLMDistributionId,TLMMaterialId,QuantityShipped,QuantityReceived,Comment")] TLMDistributionDetail tLMDistributionDetail)
        {
            if (ModelState.IsValid)
            {
                tLMDistributionDetail.TLMDistributionDetailId = Guid.NewGuid();
                _context.Add(tLMDistributionDetail);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), new { id = tLMDistributionDetail.TLMDistributionId });
            }
            ViewData["TLMDistributionId"] = new SelectList(_context.TLMDistributions, "TLMDistributionId", "TrackingCode", tLMDistributionDetail.TLMDistributionId);
            ViewData["TLMMaterialId"] = new SelectList(_context.TLMMaterials
                .Select(x => new {
                    x.TLMMaterialId,
                    TLMMaterialName = x.TLMMaterialName + " (" + x.TLMMaterialCode + ")"
                }), "TLMMaterialId", "TLMMaterialName");
            ViewData["ParentId"] = tLMDistributionDetail.TLMDistributionId;
            return View(tLMDistributionDetail);
        }

        // GET: TLMDistributionDetails/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMDistributionDetail = await _context.TLMDistributionDetails.FindAsync(id);

            if (tLMDistributionDetail == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = tLMDistributionDetail.TLMDistributionId;
            ViewData["TLMMaterialId"] = new SelectList(_context.TLMMaterials
                .Select(x => new {
                    x.TLMMaterialId,
                    TLMMaterialName = x.TLMMaterialName + " (" + x.TLMMaterialCode + ")"
                }), "TLMMaterialId", "TLMMaterialName", tLMDistributionDetail.TLMMaterialId);
            return View(tLMDistributionDetail);
        }

        // POST: TLMDistributionDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TLMDistributionDetailId,RegistrationDate,TLMDistributionId,TLMMaterialId,QuantityShipped,QuantityReceived,Comment")] TLMDistributionDetail tLMDistributionDetail)
        {
            if (id != tLMDistributionDetail.TLMDistributionDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tLMDistributionDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLMDistributionDetailExists(tLMDistributionDetail.TLMDistributionDetailId))
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
                return RedirectToAction(nameof(Index), new { id = tLMDistributionDetail.TLMDistributionId });
            }
            ViewData["TLMMaterialId"] = new SelectList(_context.TLMMaterials
                .Select(x => new {
                    x.TLMMaterialId,
                    TLMMaterialName = x.TLMMaterialName + " (" + x.TLMMaterialCode + ")"
                }), "TLMMaterialId", "TLMMaterialName", tLMDistributionDetail.TLMMaterialId);
            ViewData["ParentId"] = tLMDistributionDetail.TLMDistributionId;
            return View(tLMDistributionDetail);
        }

        // GET: TLMDistributionDetails/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMDistributionDetail = await _context.TLMDistributionDetails
                .Include(t => t.TLMDistributions)
                .Include(t => t.TLMMaterials)
                    .FirstOrDefaultAsync(m => m.TLMDistributionDetailId == id);

            if (tLMDistributionDetail == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
            //relatedCount += tLMDistributionDetail.ICollection.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            ViewData["ParentId"] = tLMDistributionDetail.TLMDistributionId;

            return View(tLMDistributionDetail);
        }

        // POST: TLMDistributionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tLMDistributionDetail = await _context.TLMDistributionDetails.FindAsync(id);

            if (tLMDistributionDetail != null)
            {
                _context.TLMDistributionDetails.Remove(tLMDistributionDetail);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            return RedirectToAction(nameof(Index), new { id = tLMDistributionDetail!.TLMDistributionId });
        }

        private bool TLMDistributionDetailExists(Guid id)
        {
            return _context.TLMDistributionDetails.Any(e => e.TLMDistributionDetailId == id);
        }
    }
}
