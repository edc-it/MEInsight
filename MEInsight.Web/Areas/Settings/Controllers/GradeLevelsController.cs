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
    public class GradeLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradeLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/GradeLevels
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.GradeLevels.ToListAsync());
        }

        // GET: Settings/GradeLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refGradeLevel = await _context.GradeLevels
                .FirstOrDefaultAsync(m => m.RefGradeLevelId == id);
            
            if (refGradeLevel == null)
            {
                return NotFound();
            }

            return View(refGradeLevel);
        }

        // GET: Settings/GradeLevels/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/GradeLevels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefGradeLevelId,GradeLevelCode,GradeLevel,GradeLevelId")] RefGradeLevel refGradeLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refGradeLevel);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refGradeLevel);
        }

        // GET: Settings/GradeLevels/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refGradeLevel = await _context.GradeLevels.FindAsync(id);

            if (refGradeLevel == null)
            {
                return NotFound();
            }
            return View(refGradeLevel);
        }

        // POST: Settings/GradeLevels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefGradeLevelId,GradeLevelCode,GradeLevel,GradeLevelId")] RefGradeLevel refGradeLevel)
        {
            if (id != refGradeLevel.RefGradeLevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refGradeLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefGradeLevelExists(refGradeLevel.RefGradeLevelId))
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
            return View(refGradeLevel);
        }

        // GET: Settings/GradeLevels/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refGradeLevel = await _context.GradeLevels
					.Include(m => m.Groups)
					.Include(m => m.SchoolEnrollments)
					.Include(m => m.TLMMaterials)
					.FirstOrDefaultAsync(m => m.RefGradeLevelId == id);

            if (refGradeLevel == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refGradeLevel.Groups.Count();
			relatedCount += refGradeLevel.SchoolEnrollments.Count();
			relatedCount += refGradeLevel.TLMMaterials.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refGradeLevel);
        }

        // POST: Settings/GradeLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refGradeLevel = await _context.GradeLevels.FindAsync(id);

            if (refGradeLevel != null)
            {
                _context.GradeLevels.Remove(refGradeLevel);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefGradeLevelExists(int id)
        {
            return _context.GradeLevels.Any(e => e.RefGradeLevelId == id);
        }
    }
}
