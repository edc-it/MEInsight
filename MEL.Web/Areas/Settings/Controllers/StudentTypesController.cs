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
    public class StudentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/StudentTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.StudentTypes.ToListAsync());
        }

        // GET: Settings/StudentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentType = await _context.StudentTypes
                .FirstOrDefaultAsync(m => m.RefStudentTypeId == id);
            
            if (refStudentType == null)
            {
                return NotFound();
            }

            return View(refStudentType);
        }

        // GET: Settings/StudentTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/StudentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefStudentTypeId,StudentTypeCode,StudentType")] RefStudentType refStudentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refStudentType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refStudentType);
        }

        // GET: Settings/StudentTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentType = await _context.StudentTypes.FindAsync(id);

            if (refStudentType == null)
            {
                return NotFound();
            }
            return View(refStudentType);
        }

        // POST: Settings/StudentTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefStudentTypeId,StudentTypeCode,StudentType")] RefStudentType refStudentType)
        {
            if (id != refStudentType.RefStudentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refStudentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefStudentTypeExists(refStudentType.RefStudentTypeId))
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
            return View(refStudentType);
        }

        // GET: Settings/StudentTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentType = await _context.StudentTypes
					.Include(m => m.Students)
					.FirstOrDefaultAsync(m => m.RefStudentTypeId == id);

            if (refStudentType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refStudentType.Students.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refStudentType);
        }

        // POST: Settings/StudentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refStudentType = await _context.StudentTypes.FindAsync(id);

            _context.StudentTypes.Remove(refStudentType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefStudentTypeExists(int id)
        {
            return _context.StudentTypes.Any(e => e.RefStudentTypeId == id);
        }
    }
}
