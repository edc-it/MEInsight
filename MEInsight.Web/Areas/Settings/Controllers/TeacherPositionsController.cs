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
    public class TeacherPositionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherPositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/TeacherPositions
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.TeacherPositions.ToListAsync());
        }

        // GET: Settings/TeacherPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTeacherPosition = await _context.TeacherPositions
                .FirstOrDefaultAsync(m => m.RefTeacherPositionId == id);
            
            if (refTeacherPosition == null)
            {
                return NotFound();
            }

            return View(refTeacherPosition);
        }

        // GET: Settings/TeacherPositions/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/TeacherPositions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTeacherPositionId,TeacherPositionCode,TeacherPosition")] RefTeacherPosition refTeacherPosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refTeacherPosition);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refTeacherPosition);
        }

        // GET: Settings/TeacherPositions/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTeacherPosition = await _context.TeacherPositions.FindAsync(id);

            if (refTeacherPosition == null)
            {
                return NotFound();
            }
            return View(refTeacherPosition);
        }

        // POST: Settings/TeacherPositions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefTeacherPositionId,TeacherPositionCode,TeacherPosition")] RefTeacherPosition refTeacherPosition)
        {
            if (id != refTeacherPosition.RefTeacherPositionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refTeacherPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefTeacherPositionExists(refTeacherPosition.RefTeacherPositionId))
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
            return View(refTeacherPosition);
        }

        // GET: Settings/TeacherPositions/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTeacherPosition = await _context.TeacherPositions
					.Include(m => m.Teachers)
					.FirstOrDefaultAsync(m => m.RefTeacherPositionId == id);

            if (refTeacherPosition == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refTeacherPosition.Teachers.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refTeacherPosition);
        }

        // POST: Settings/TeacherPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refTeacherPosition = await _context.TeacherPositions.FindAsync(id);

            _context.TeacherPositions.Remove(refTeacherPosition);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefTeacherPositionExists(int id)
        {
            return _context.TeacherPositions.Any(e => e.RefTeacherPositionId == id);
        }
    }
}
