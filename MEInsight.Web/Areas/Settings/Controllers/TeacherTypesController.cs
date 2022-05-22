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
    public class TeacherTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/TeacherTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.TeacherTypes.ToListAsync());
        }

        // GET: Settings/TeacherTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTeacherType = await _context.TeacherTypes
                .FirstOrDefaultAsync(m => m.RefTeacherTypeId == id);
            
            if (refTeacherType == null)
            {
                return NotFound();
            }

            return View(refTeacherType);
        }

        // GET: Settings/TeacherTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/TeacherTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTeacherTypeId,TeacherTypeCode,TeacherType")] RefTeacherType refTeacherType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refTeacherType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refTeacherType);
        }

        // GET: Settings/TeacherTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTeacherType = await _context.TeacherTypes.FindAsync(id);

            if (refTeacherType == null)
            {
                return NotFound();
            }
            return View(refTeacherType);
        }

        // POST: Settings/TeacherTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefTeacherTypeId,TeacherTypeCode,TeacherType")] RefTeacherType refTeacherType)
        {
            if (id != refTeacherType.RefTeacherTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refTeacherType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefTeacherTypeExists(refTeacherType.RefTeacherTypeId))
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
            return View(refTeacherType);
        }

        // GET: Settings/TeacherTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTeacherType = await _context.TeacherTypes
					.Include(m => m.Teachers)
					.FirstOrDefaultAsync(m => m.RefTeacherTypeId == id);

            if (refTeacherType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refTeacherType.Teachers.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refTeacherType);
        }

        // POST: Settings/TeacherTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refTeacherType = await _context.TeacherTypes.FindAsync(id);

            _context.TeacherTypes.Remove(refTeacherType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefTeacherTypeExists(int id)
        {
            return _context.TeacherTypes.Any(e => e.RefTeacherTypeId == id);
        }
    }
}
