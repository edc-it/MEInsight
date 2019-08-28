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
    public class SchoolTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/SchoolTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.SchoolTypes.ToListAsync());
        }

        // GET: Settings/SchoolTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolType = await _context.SchoolTypes
                .FirstOrDefaultAsync(m => m.RefSchoolTypeId == id);
            
            if (refSchoolType == null)
            {
                return NotFound();
            }

            return View(refSchoolType);
        }

        // GET: Settings/SchoolTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/SchoolTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefSchoolTypeId,SchoolTypeCode,SchoolType")] RefSchoolType refSchoolType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refSchoolType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refSchoolType);
        }

        // GET: Settings/SchoolTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolType = await _context.SchoolTypes.FindAsync(id);

            if (refSchoolType == null)
            {
                return NotFound();
            }
            return View(refSchoolType);
        }

        // POST: Settings/SchoolTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefSchoolTypeId,SchoolTypeCode,SchoolType")] RefSchoolType refSchoolType)
        {
            if (id != refSchoolType.RefSchoolTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refSchoolType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefSchoolTypeExists(refSchoolType.RefSchoolTypeId))
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
            return View(refSchoolType);
        }

        // GET: Settings/SchoolTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolType = await _context.SchoolTypes
					.Include(m => m.Schools)
					.FirstOrDefaultAsync(m => m.RefSchoolTypeId == id);

            if (refSchoolType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                
            relatedCount += refSchoolType.Schools.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refSchoolType);
        }

        // POST: Settings/SchoolTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refSchoolType = await _context.SchoolTypes.FindAsync(id);

            _context.SchoolTypes.Remove(refSchoolType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefSchoolTypeExists(int id)
        {
            return _context.SchoolTypes.Any(e => e.RefSchoolTypeId == id);
        }
    }
}
