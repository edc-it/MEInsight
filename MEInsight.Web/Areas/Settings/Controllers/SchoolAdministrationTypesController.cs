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
    public class SchoolAdministrationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolAdministrationTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/SchoolAdministrationTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.SchoolAdministrationTypes.ToListAsync());
        }

        // GET: Settings/SchoolAdministrationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolAdministrationType = await _context.SchoolAdministrationTypes
                .FirstOrDefaultAsync(m => m.RefSchoolAdministrationTypeId == id);
            
            if (refSchoolAdministrationType == null)
            {
                return NotFound();
            }

            return View(refSchoolAdministrationType);
        }

        // GET: Settings/SchoolAdministrationTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/SchoolAdministrationTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefSchoolAdministrationTypeId,SchoolAdministrationTypeCode,SchoolAdministrationType")] RefSchoolAdministrationType refSchoolAdministrationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refSchoolAdministrationType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refSchoolAdministrationType);
        }

        // GET: Settings/SchoolAdministrationTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolAdministrationType = await _context.SchoolAdministrationTypes.FindAsync(id);

            if (refSchoolAdministrationType == null)
            {
                return NotFound();
            }
            return View(refSchoolAdministrationType);
        }

        // POST: Settings/SchoolAdministrationTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefSchoolAdministrationTypeId,SchoolAdministrationTypeCode,SchoolAdministrationType")] RefSchoolAdministrationType refSchoolAdministrationType)
        {
            if (id != refSchoolAdministrationType.RefSchoolAdministrationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refSchoolAdministrationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefSchoolAdministrationTypeExists(refSchoolAdministrationType.RefSchoolAdministrationTypeId))
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
            return View(refSchoolAdministrationType);
        }

        // GET: Settings/SchoolAdministrationTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolAdministrationType = await _context.SchoolAdministrationTypes
					.Include(m => m.Schools)
					.FirstOrDefaultAsync(m => m.RefSchoolAdministrationTypeId == id);

            if (refSchoolAdministrationType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refSchoolAdministrationType.Schools.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refSchoolAdministrationType);
        }

        // POST: Settings/SchoolAdministrationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refSchoolAdministrationType = await _context.SchoolAdministrationTypes.FindAsync(id);

            _context.SchoolAdministrationTypes.Remove(refSchoolAdministrationType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefSchoolAdministrationTypeExists(int id)
        {
            return _context.SchoolAdministrationTypes.Any(e => e.RefSchoolAdministrationTypeId == id);
        }
    }
}
