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
    public class AttendanceUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/AttendanceUnits
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.AttendanceUnits.ToListAsync());
        }

        // GET: Settings/AttendanceUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refAttendanceUnit = await _context.AttendanceUnits
                .FirstOrDefaultAsync(m => m.RefAttendanceUnitId == id);
            
            if (refAttendanceUnit == null)
            {
                return NotFound();
            }

            return View(refAttendanceUnit);
        }

        // GET: Settings/AttendanceUnits/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/AttendanceUnits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefAttendanceUnitId,AttendanceUnitCode,AttendanceUnit,AttendanceUnitId")] RefAttendanceUnit refAttendanceUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refAttendanceUnit);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refAttendanceUnit);
        }

        // GET: Settings/AttendanceUnits/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refAttendanceUnit = await _context.AttendanceUnits.FindAsync(id);

            if (refAttendanceUnit == null)
            {
                return NotFound();
            }
            return View(refAttendanceUnit);
        }

        // POST: Settings/AttendanceUnits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefAttendanceUnitId,AttendanceUnitCode,AttendanceUnit,AttendanceUnitId")] RefAttendanceUnit refAttendanceUnit)
        {
            if (id != refAttendanceUnit.RefAttendanceUnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refAttendanceUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefAttendanceUnitExists(refAttendanceUnit.RefAttendanceUnitId))
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
            return View(refAttendanceUnit);
        }

        // GET: Settings/AttendanceUnits/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var refAttendanceUnit = await _context.AttendanceUnits
					.Include(m => m.ProgramAssessments)
					.Include(m => m.Programs)
					.FirstOrDefaultAsync(m => m.RefAttendanceUnitId == id);

            if (refAttendanceUnit == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refAttendanceUnit.ProgramAssessments.Count();
			relatedCount += refAttendanceUnit.Programs.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refAttendanceUnit);
        }

        // POST: Settings/AttendanceUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refAttendanceUnit = await _context.AttendanceUnits.FindAsync(id);

            _context.AttendanceUnits.Remove(refAttendanceUnit);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefAttendanceUnitExists(int id)
        {
            return _context.AttendanceUnits.Any(e => e.RefAttendanceUnitId == id);
        }
    }
}
