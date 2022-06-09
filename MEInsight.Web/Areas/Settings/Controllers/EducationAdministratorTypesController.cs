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
    public class EducationAdministratorTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationAdministratorTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/EducationAdministratorTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.EducationAdministratorTypes.ToListAsync());
        }

        // GET: Settings/EducationAdministratorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEducationAdministratorType = await _context.EducationAdministratorTypes
                .FirstOrDefaultAsync(m => m.RefEducationAdministratorTypeId == id);
            
            if (refEducationAdministratorType == null)
            {
                return NotFound();
            }

            return View(refEducationAdministratorType);
        }

        // GET: Settings/EducationAdministratorTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/EducationAdministratorTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefEducationAdministratorTypeId,EducationAdministratorTypeCode,EducationAdministratorType")] RefEducationAdministratorType refEducationAdministratorType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refEducationAdministratorType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refEducationAdministratorType);
        }

        // GET: Settings/EducationAdministratorTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEducationAdministratorType = await _context.EducationAdministratorTypes.FindAsync(id);

            if (refEducationAdministratorType == null)
            {
                return NotFound();
            }
            return View(refEducationAdministratorType);
        }

        // POST: Settings/EducationAdministratorTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefEducationAdministratorTypeId,EducationAdministratorTypeCode,EducationAdministratorType")] RefEducationAdministratorType refEducationAdministratorType)
        {
            if (id != refEducationAdministratorType.RefEducationAdministratorTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refEducationAdministratorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefEducationAdministratorTypeExists(refEducationAdministratorType.RefEducationAdministratorTypeId))
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
            return View(refEducationAdministratorType);
        }

        // GET: Settings/EducationAdministratorTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEducationAdministratorType = await _context.EducationAdministratorTypes
					.Include(m => m.EducationAdministrators)
					.FirstOrDefaultAsync(m => m.RefEducationAdministratorTypeId == id);

            if (refEducationAdministratorType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refEducationAdministratorType.EducationAdministrators.Count;

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refEducationAdministratorType);
        }

        // POST: Settings/EducationAdministratorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refEducationAdministratorType = await _context.EducationAdministratorTypes.FindAsync(id);

            if (refEducationAdministratorType != null)
            {
                _context.EducationAdministratorTypes.Remove(refEducationAdministratorType);
            }

            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefEducationAdministratorTypeExists(int id)
        {
            return _context.EducationAdministratorTypes.Any(e => e.RefEducationAdministratorTypeId == id);
        }
    }
}
