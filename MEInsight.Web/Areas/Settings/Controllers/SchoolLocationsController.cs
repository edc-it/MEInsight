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
    public class SchoolLocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolLocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/SchoolLocations
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.SchoolLocations.ToListAsync());
        }

        // GET: Settings/SchoolLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolLocation = await _context.SchoolLocations
                .FirstOrDefaultAsync(m => m.RefSchoolLocationId == id);
            
            if (refSchoolLocation == null)
            {
                return NotFound();
            }

            return View(refSchoolLocation);
        }

        // GET: Settings/SchoolLocations/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/SchoolLocations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefSchoolLocationId,SchoolLocationCode,SchoolLocation")] RefSchoolLocation refSchoolLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refSchoolLocation);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refSchoolLocation);
        }

        // GET: Settings/SchoolLocations/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolLocation = await _context.SchoolLocations.FindAsync(id);

            if (refSchoolLocation == null)
            {
                return NotFound();
            }
            return View(refSchoolLocation);
        }

        // POST: Settings/SchoolLocations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefSchoolLocationId,SchoolLocationCode,SchoolLocation")] RefSchoolLocation refSchoolLocation)
        {
            if (id != refSchoolLocation.RefSchoolLocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refSchoolLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefSchoolLocationExists(refSchoolLocation.RefSchoolLocationId))
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
            return View(refSchoolLocation);
        }

        // GET: Settings/SchoolLocations/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolLocation = await _context.SchoolLocations
					.Include(m => m.Schools)
					.FirstOrDefaultAsync(m => m.RefSchoolLocationId == id);

            if (refSchoolLocation == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                
            relatedCount += refSchoolLocation.Schools.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refSchoolLocation);
        }

        // POST: Settings/SchoolLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refSchoolLocation = await _context.SchoolLocations.FindAsync(id);

            _context.SchoolLocations.Remove(refSchoolLocation);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefSchoolLocationExists(int id)
        {
            return _context.SchoolLocations.Any(e => e.RefSchoolLocationId == id);
        }
    }
}
