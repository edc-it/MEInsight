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
    public class TLMDistributionPeriodsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TLMDistributionPeriodsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TLMDistributionPeriods
        public async Task<IActionResult> Index(Guid? id)
        {
        
            ViewData["NextController"] = "TLMDistributions";
            ViewData["ParentController"] = null;
            ViewData["ParentId"] = id;
            
            return View(await _context.TLMDistributionPeriods.ToListAsync());
        }

        // GET: TLMDistributionPeriods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMDistributionPeriod = await _context.TLMDistributionPeriods
                .FirstOrDefaultAsync(m => m.TLMDistributionPeriodId == id);
            
            if (tLMDistributionPeriod == null)
            {
                return NotFound();
            }

            //ViewData["ParentId"] = tLMDistributionPeriod.ParentId;

            return View(tLMDistributionPeriod);
        }

        // GET: TLMDistributionPeriods/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create()
        {

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //ViewData["ParentId"] = id;
            return View();
        }

        // POST: TLMDistributionPeriods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TLMDistributionPeriodId,PeriodName,StartDate,EndDate,Closed,ClosedBy,ClosedDate")] TLMDistributionPeriod tLMDistributionPeriod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tLMDistributionPeriod);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
                //return RedirectToAction(nameof(Index), new { id = tLMDistributionPeriod.ParentId });
            }
            //ViewData["ParentId"] = tLMDistributionPeriod.ParentId;
            return View(tLMDistributionPeriod);
        }

        // GET: TLMDistributionPeriods/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMDistributionPeriod = await _context.TLMDistributionPeriods.FindAsync(id);

            if (tLMDistributionPeriod == null)
            {
                return NotFound();
            }
            //ViewData["ParentId"] = tLMDistributionPeriod.ParentId;
            return View(tLMDistributionPeriod);
        }

        // POST: TLMDistributionPeriods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TLMDistributionPeriodId,PeriodName,StartDate,EndDate,Closed,ClosedBy,ClosedDate")] TLMDistributionPeriod tLMDistributionPeriod)
        {
            if (id != tLMDistributionPeriod.TLMDistributionPeriodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tLMDistributionPeriod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLMDistributionPeriodExists(tLMDistributionPeriod.TLMDistributionPeriodId))
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
                //return RedirectToAction(nameof(Index), new { id = tLMDistributionPeriod.ParentId });
            }
            //ViewData["ParentId"] = tLMDistributionPeriod.ParentId;
            return View(tLMDistributionPeriod);
        }

        // GET: TLMDistributionPeriods/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tLMDistributionPeriod = await _context.TLMDistributionPeriods
                .Include(m => m.TLMDistributions)
                .FirstOrDefaultAsync(m => m.TLMDistributionPeriodId == id);

            if (tLMDistributionPeriod == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                
            relatedCount += tLMDistributionPeriod.TLMDistributions.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            //ViewData["ParentId"] = tLMDistributionPeriod.ParentId;

            return View(tLMDistributionPeriod);
        }

        // POST: TLMDistributionPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tLMDistributionPeriod = await _context.TLMDistributionPeriods.FindAsync(id);

            if (tLMDistributionPeriod != null)
            {
                _context.TLMDistributionPeriods.Remove(tLMDistributionPeriod);
            }

            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            return RedirectToAction(nameof(Index));        
            //return RedirectToAction(nameof(Index), new { id = tLMDistributionPeriod.ParentId });
        }

        private bool TLMDistributionPeriodExists(int id)
        {
            return _context.TLMDistributionPeriods.Any(e => e.TLMDistributionPeriodId == id);
        }
    }
}
