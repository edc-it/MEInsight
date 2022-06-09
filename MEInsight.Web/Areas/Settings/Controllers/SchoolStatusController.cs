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
    public class SchoolStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/SchoolStatus
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.SchoolStatus.ToListAsync());
        }

        // GET: Settings/SchoolStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolStatus = await _context.SchoolStatus
                .FirstOrDefaultAsync(m => m.RefSchoolStatusId == id);
            
            if (refSchoolStatus == null)
            {
                return NotFound();
            }

            return View(refSchoolStatus);
        }

        // GET: Settings/SchoolStatus/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/SchoolStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefSchoolStatusId,SchoolStatusCode,SchoolStatus")] RefSchoolStatus refSchoolStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refSchoolStatus);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refSchoolStatus);
        }

        // GET: Settings/SchoolStatus/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolStatus = await _context.SchoolStatus.FindAsync(id);

            if (refSchoolStatus == null)
            {
                return NotFound();
            }
            return View(refSchoolStatus);
        }

        // POST: Settings/SchoolStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefSchoolStatusId,SchoolStatusCode,SchoolStatus")] RefSchoolStatus refSchoolStatus)
        {
            if (id != refSchoolStatus.RefSchoolStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refSchoolStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefSchoolStatusExists(refSchoolStatus.RefSchoolStatusId))
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
            return View(refSchoolStatus);
        }

        // GET: Settings/SchoolStatus/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolStatus = await _context.SchoolStatus
					.Include(m => m.Schools)
					.FirstOrDefaultAsync(m => m.RefSchoolStatusId == id);

            if (refSchoolStatus == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refSchoolStatus.Schools.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refSchoolStatus);
        }

        // POST: Settings/SchoolStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refSchoolStatus = await _context.SchoolStatus.FindAsync(id);

            if (refSchoolStatus != null)
            {
                _context.SchoolStatus.Remove(refSchoolStatus);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefSchoolStatusExists(int id)
        {
            return _context.SchoolStatus.Any(e => e.RefSchoolStatusId == id);
        }
    }
}
