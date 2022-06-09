using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using MEInsight.Web.Data;
using MEInsight.Entities.Reference;

namespace MEInsight.Web.Areas.Settings.Controllers
{
    [Area("Settings")]
    [Authorize]
    public class TLMDistributionStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TLMDistributionStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/TLMDistributionStatus
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.TLMDistributionStatus.ToListAsync());
        }

        // GET: Settings/TLMDistributionStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMDistributionStatus = await _context.TLMDistributionStatus
                .FirstOrDefaultAsync(m => m.RefTLMDistributionStatusId == id);
            
            if (refTLMDistributionStatus == null)
            {
                return NotFound();
            }

            return View(refTLMDistributionStatus);
        }

        // GET: Settings/TLMDistributionStatus/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/TLMDistributionStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTLMDistributionStatusId,DistributionStatusCode,DistributionStatus")] RefTLMDistributionStatus refTLMDistributionStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refTLMDistributionStatus);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refTLMDistributionStatus);
        }

        // GET: Settings/TLMDistributionStatus/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMDistributionStatus = await _context.TLMDistributionStatus.FindAsync(id);

            if (refTLMDistributionStatus == null)
            {
                return NotFound();
            }
            return View(refTLMDistributionStatus);
        }

        // POST: Settings/TLMDistributionStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefTLMDistributionStatusId,DistributionStatusCode,DistributionStatus")] RefTLMDistributionStatus refTLMDistributionStatus)
        {
            if (id != refTLMDistributionStatus.RefTLMDistributionStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refTLMDistributionStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefTLMDistributionStatusExists(refTLMDistributionStatus.RefTLMDistributionStatusId))
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
            }
            return View(refTLMDistributionStatus);
        }

        // GET: Settings/TLMDistributionStatus/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMDistributionStatus = await _context.TLMDistributionStatus
					.Include(m => m.TLMDistributions)
					.FirstOrDefaultAsync(m => m.RefTLMDistributionStatusId == id);

            if (refTLMDistributionStatus == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refTLMDistributionStatus.TLMDistributions.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refTLMDistributionStatus);
        }

        // POST: Settings/TLMDistributionStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refTLMDistributionStatus = await _context.TLMDistributionStatus.FindAsync(id);

            if (refTLMDistributionStatus != null)
            {
                _context.TLMDistributionStatus.Remove(refTLMDistributionStatus);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefTLMDistributionStatusExists(int id)
        {
            return _context.TLMDistributionStatus.Any(e => e.RefTLMDistributionStatusId == id);
        }
    }
}
