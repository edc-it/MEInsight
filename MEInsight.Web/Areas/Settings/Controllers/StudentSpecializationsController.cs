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
    public class StudentSpecializationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentSpecializationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/StudentSpecializations
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.StudentSpecializations.ToListAsync());
        }

        // GET: Settings/StudentSpecializations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentSpecialization = await _context.StudentSpecializations
                .FirstOrDefaultAsync(m => m.RefStudentSpecializationId == id);
            
            if (refStudentSpecialization == null)
            {
                return NotFound();
            }

            return View(refStudentSpecialization);
        }

        // GET: Settings/StudentSpecializations/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/StudentSpecializations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefStudentSpecializationId,StudentSpecializationCode,StudentSpecialization")] RefStudentSpecialization refStudentSpecialization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refStudentSpecialization);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refStudentSpecialization);
        }

        // GET: Settings/StudentSpecializations/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentSpecialization = await _context.StudentSpecializations.FindAsync(id);

            if (refStudentSpecialization == null)
            {
                return NotFound();
            }
            return View(refStudentSpecialization);
        }

        // POST: Settings/StudentSpecializations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefStudentSpecializationId,StudentSpecializationCode,StudentSpecialization")] RefStudentSpecialization refStudentSpecialization)
        {
            if (id != refStudentSpecialization.RefStudentSpecializationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refStudentSpecialization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefStudentSpecializationExists(refStudentSpecialization.RefStudentSpecializationId))
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
            return View(refStudentSpecialization);
        }

        // GET: Settings/StudentSpecializations/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentSpecialization = await _context.StudentSpecializations
					.Include(m => m.Students)
					.FirstOrDefaultAsync(m => m.RefStudentSpecializationId == id);

            if (refStudentSpecialization == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refStudentSpecialization.Students.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refStudentSpecialization);
        }

        // POST: Settings/StudentSpecializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refStudentSpecialization = await _context.StudentSpecializations.FindAsync(id);

            _context.StudentSpecializations.Remove(refStudentSpecialization);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefStudentSpecializationExists(int id)
        {
            return _context.StudentSpecializations.Any(e => e.RefStudentSpecializationId == id);
        }
    }
}
