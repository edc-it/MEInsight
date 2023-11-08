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
    public class LocationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/LocationTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.LocationTypes.ToListAsync());
        }

        // GET: Settings/LocationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refLocationType = await _context.LocationTypes
                .FirstOrDefaultAsync(m => m.RefLocationTypeId == id);
            
            if (refLocationType == null)
            {
                return NotFound();
            }

            return View(refLocationType);
        }

        // GET: Settings/LocationTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/LocationTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefLocationTypeId,LocationTypeCode,LocationType,LocationLevel")] RefLocationType refLocationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refLocationType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refLocationType);
        }

        // GET: Settings/LocationTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refLocationType = await _context.LocationTypes.FindAsync(id);

            if (refLocationType == null)
            {
                return NotFound();
            }
            return View(refLocationType);
        }

        // POST: Settings/LocationTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefLocationTypeId,LocationTypeCode,LocationType,LocationLevel")] RefLocationType refLocationType)
        {
            if (id != refLocationType.RefLocationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refLocationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefLocationTypeExists(refLocationType.RefLocationTypeId))
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
            return View(refLocationType);
        }

        // GET: Settings/LocationTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refLocationType = await _context.LocationTypes
					.Include(m => m.Locations)
                    .FirstOrDefaultAsync(m => m.RefLocationTypeId == id);

            if (refLocationType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                

            relatedCount += refLocationType.Locations.Count;

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refLocationType);
        }

        // POST: Settings/LocationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refLocationType = await _context.LocationTypes.FindAsync(id);

            if (refLocationType != null)
            {
                _context.LocationTypes.Remove(refLocationType);
            }

            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefLocationTypeExists(int id)
        {
            return _context.LocationTypes.Any(e => e.RefLocationTypeId == id);
        }
    }
}
