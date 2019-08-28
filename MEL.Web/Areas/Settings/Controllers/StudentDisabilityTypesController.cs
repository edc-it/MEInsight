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
    public class StudentDisabilityTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentDisabilityTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/StudentDisabilityTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.DisabilityTypes.ToListAsync());
        }

        // GET: Settings/StudentDisabilityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentDisabilityType = await _context.DisabilityTypes
                .FirstOrDefaultAsync(m => m.RefStudentDisabilityTypeId == id);
            
            if (refStudentDisabilityType == null)
            {
                return NotFound();
            }

            return View(refStudentDisabilityType);
        }

        // GET: Settings/StudentDisabilityTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/StudentDisabilityTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefStudentDisabilityTypeId,DisabilityTypeCode,DisabilityType")] RefStudentDisabilityType refStudentDisabilityType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refStudentDisabilityType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refStudentDisabilityType);
        }

        // GET: Settings/StudentDisabilityTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentDisabilityType = await _context.DisabilityTypes.FindAsync(id);

            if (refStudentDisabilityType == null)
            {
                return NotFound();
            }
            return View(refStudentDisabilityType);
        }

        // POST: Settings/StudentDisabilityTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefStudentDisabilityTypeId,DisabilityTypeCode,DisabilityType")] RefStudentDisabilityType refStudentDisabilityType)
        {
            if (id != refStudentDisabilityType.RefStudentDisabilityTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refStudentDisabilityType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefStudentDisabilityTypeExists(refStudentDisabilityType.RefStudentDisabilityTypeId))
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
            return View(refStudentDisabilityType);
        }

        // GET: Settings/StudentDisabilityTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentDisabilityType = await _context.DisabilityTypes
					.Include(m => m.Students)
					.FirstOrDefaultAsync(m => m.RefStudentDisabilityTypeId == id);

            if (refStudentDisabilityType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refStudentDisabilityType.Students.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refStudentDisabilityType);
        }

        // POST: Settings/StudentDisabilityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refStudentDisabilityType = await _context.DisabilityTypes.FindAsync(id);

            _context.DisabilityTypes.Remove(refStudentDisabilityType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefStudentDisabilityTypeExists(int id)
        {
            return _context.DisabilityTypes.Any(e => e.RefStudentDisabilityTypeId == id);
        }
    }
}
