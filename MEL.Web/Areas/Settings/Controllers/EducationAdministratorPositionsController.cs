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
    public class EducationAdministratorPositionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationAdministratorPositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/EducationAdministratorPositions
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.EducationAdministratorPositions.ToListAsync());
        }

        // GET: Settings/EducationAdministratorPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEducationAdministratorPosition = await _context.EducationAdministratorPositions
                .FirstOrDefaultAsync(m => m.RefEducationAdministratorPositionId == id);
            
            if (refEducationAdministratorPosition == null)
            {
                return NotFound();
            }

            return View(refEducationAdministratorPosition);
        }

        // GET: Settings/EducationAdministratorPositions/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/EducationAdministratorPositions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefEducationAdministratorPositionId,EducationAdministratorPositionCode,EducationAdministratorPosition")] RefEducationAdministratorPosition refEducationAdministratorPosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refEducationAdministratorPosition);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refEducationAdministratorPosition);
        }

        // GET: Settings/EducationAdministratorPositions/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEducationAdministratorPosition = await _context.EducationAdministratorPositions.FindAsync(id);

            if (refEducationAdministratorPosition == null)
            {
                return NotFound();
            }
            return View(refEducationAdministratorPosition);
        }

        // POST: Settings/EducationAdministratorPositions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefEducationAdministratorPositionId,EducationAdministratorPositionCode,EducationAdministratorPosition")] RefEducationAdministratorPosition refEducationAdministratorPosition)
        {
            if (id != refEducationAdministratorPosition.RefEducationAdministratorPositionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refEducationAdministratorPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefEducationAdministratorPositionExists(refEducationAdministratorPosition.RefEducationAdministratorPositionId))
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
            return View(refEducationAdministratorPosition);
        }

        // GET: Settings/EducationAdministratorPositions/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEducationAdministratorPosition = await _context.EducationAdministratorPositions
					.Include(m => m.EducationAdministrators)
                    .FirstOrDefaultAsync(m => m.RefEducationAdministratorPositionId == id);

            if (refEducationAdministratorPosition == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refEducationAdministratorPosition.EducationAdministrators.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refEducationAdministratorPosition);
        }

        // POST: Settings/EducationAdministratorPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refEducationAdministratorPosition = await _context.EducationAdministratorPositions.FindAsync(id);

            _context.EducationAdministratorPositions.Remove(refEducationAdministratorPosition);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefEducationAdministratorPositionExists(int id)
        {
            return _context.EducationAdministratorPositions.Any(e => e.RefEducationAdministratorPositionId == id);
        }
    }
}
