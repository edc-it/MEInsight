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
    public class TeacherEmploymentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherEmploymentTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/TeacherEmploymentTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.TeacherEmploymentTypes.ToListAsync());
        }

        // GET: Settings/TeacherEmploymentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTeacherEmploymentType = await _context.TeacherEmploymentTypes
                .FirstOrDefaultAsync(m => m.RefTeacherEmploymentTypeId == id);
            
            if (refTeacherEmploymentType == null)
            {
                return NotFound();
            }

            return View(refTeacherEmploymentType);
        }

        // GET: Settings/TeacherEmploymentTypes/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/TeacherEmploymentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTeacherEmploymentTypeId,TeacherEmploymentTypeCode,TeacherEmploymentType")] RefTeacherEmploymentType refTeacherEmploymentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refTeacherEmploymentType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refTeacherEmploymentType);
        }

        // GET: Settings/TeacherEmploymentTypes/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTeacherEmploymentType = await _context.TeacherEmploymentTypes.FindAsync(id);

            if (refTeacherEmploymentType == null)
            {
                return NotFound();
            }
            return View(refTeacherEmploymentType);
        }

        // POST: Settings/TeacherEmploymentTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefTeacherEmploymentTypeId,TeacherEmploymentTypeCode,TeacherEmploymentType")] RefTeacherEmploymentType refTeacherEmploymentType)
        {
            if (id != refTeacherEmploymentType.RefTeacherEmploymentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refTeacherEmploymentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefTeacherEmploymentTypeExists(refTeacherEmploymentType.RefTeacherEmploymentTypeId))
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
            return View(refTeacherEmploymentType);
        }

        // GET: Settings/TeacherEmploymentTypes/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTeacherEmploymentType = await _context.TeacherEmploymentTypes
                    .FirstOrDefaultAsync(m => m.RefTeacherEmploymentTypeId == id);

            if (refTeacherEmploymentType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                

            //relatedCount += refTeacherEmploymentType.ICollection.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refTeacherEmploymentType);
        }

        // POST: Settings/TeacherEmploymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refTeacherEmploymentType = await _context.TeacherEmploymentTypes.FindAsync(id);

            if (refTeacherEmploymentType != null)
            {
                _context.TeacherEmploymentTypes.Remove(refTeacherEmploymentType);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefTeacherEmploymentTypeExists(int id)
        {
            return _context.TeacherEmploymentTypes.Any(e => e.RefTeacherEmploymentTypeId == id);
        }
    }
}
