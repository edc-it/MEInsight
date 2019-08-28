using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using MEL.Data;
using MEL.Entities.Reference;

namespace MEL.Web.Areas.Settings.Controllers
{
    [Area("Settings")]
    [Authorize]
    public class EvaluationStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/EvaluationStatus
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.EvaluationStatus.ToListAsync());
        }

        // GET: Settings/EvaluationStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEvaluationStatus = await _context.EvaluationStatus
                .FirstOrDefaultAsync(m => m.EvaluationStatusId == id);
            
            if (refEvaluationStatus == null)
            {
                return NotFound();
            }

            return View(refEvaluationStatus);
        }

        // GET: Settings/EvaluationStatus/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/EvaluationStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvaluationStatusId,EvaluationStatusCode,EvaluationStatus")] RefEvaluationStatus refEvaluationStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refEvaluationStatus);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refEvaluationStatus);
        }

        // GET: Settings/EvaluationStatus/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEvaluationStatus = await _context.EvaluationStatus.FindAsync(id);

            if (refEvaluationStatus == null)
            {
                return NotFound();
            }
            return View(refEvaluationStatus);
        }

        // POST: Settings/EvaluationStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EvaluationStatusId,EvaluationStatusCode,EvaluationStatus")] RefEvaluationStatus refEvaluationStatus)
        {
            if (id != refEvaluationStatus.EvaluationStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refEvaluationStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefEvaluationStatusExists(refEvaluationStatus.EvaluationStatusId))
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
            return View(refEvaluationStatus);
        }

        // GET: Settings/EvaluationStatus/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEvaluationStatus = await _context.EvaluationStatus
					.Include(m => m.GroupEvaluations)
					.Include(m => m.ProgramAssessments)
					.FirstOrDefaultAsync(m => m.EvaluationStatusId == id);

            if (refEvaluationStatus == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
               
            relatedCount += refEvaluationStatus.GroupEvaluations.Count();
			relatedCount += refEvaluationStatus.ProgramAssessments.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refEvaluationStatus);
        }

        // POST: Settings/EvaluationStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refEvaluationStatus = await _context.EvaluationStatus.FindAsync(id);

            _context.EvaluationStatus.Remove(refEvaluationStatus);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefEvaluationStatusExists(int id)
        {
            return _context.EvaluationStatus.Any(e => e.EvaluationStatusId == id);
        }
    }
}
