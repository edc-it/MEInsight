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
    public class AssessmentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssessmentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/AssessmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AssessmentTypes.ToListAsync());
        }

        // GET: Settings/AssessmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refAssessmentType = await _context.AssessmentTypes
                .FirstOrDefaultAsync(m => m.RefAssessmentTypeId == id);
            
            if (refAssessmentType == null)
            {
                return NotFound();
            }

            return View(refAssessmentType);
        }

        // GET: Settings/AssessmentTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/AssessmentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefAssessmentTypeId,AssessmentTypeCode,AssessmentType")] RefAssessmentType refAssessmentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refAssessmentType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refAssessmentType);
        }

        // GET: Settings/AssessmentTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refAssessmentType = await _context.AssessmentTypes.FindAsync(id);

            if (refAssessmentType == null)
            {
                return NotFound();
            }
            return View(refAssessmentType);
        }

        // POST: Settings/AssessmentTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefAssessmentTypeId,AssessmentTypeCode,AssessmentType")] RefAssessmentType refAssessmentType)
        {
            if (id != refAssessmentType.RefAssessmentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refAssessmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefAssessmentTypeExists(refAssessmentType.RefAssessmentTypeId))
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
            return View(refAssessmentType);
        }

        // GET: Settings/AssessmentTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refAssessmentType = await _context.AssessmentTypes
					.Include(m => m.ProgramAssessments)
                    .FirstOrDefaultAsync(m => m.RefAssessmentTypeId == id);

            if (refAssessmentType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                
            relatedCount += refAssessmentType.ProgramAssessments.Count;

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refAssessmentType);
        }

        // POST: Settings/AssessmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var refAssessmentType = await _context.AssessmentTypes.FindAsync(id);

            if (refAssessmentType != null)
            {
                _context.AssessmentTypes.Remove(refAssessmentType);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefAssessmentTypeExists(int id)
        {
            return _context.AssessmentTypes.Any(e => e.RefAssessmentTypeId == id);
        }
    }
}
