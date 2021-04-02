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
using MEL.Web.Areas.Settings.Models.ViewModels;

namespace MEL.Web.Areas.Settings.Controllers
{
    [Area("Settings")]
    [Authorize]
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/Locations
        public async Task<IActionResult> Index(string id)
        {
            ViewData["NextController"] = "Locations";
        
            var applicationDbContext = _context.Locations
                .Where(r => r.ParentLocationId == id)
                .Include(r => r.LocationTypes)
                .Include(r => r.ParentLocations)
                .Select(x => new LocationsViewModel {
                    RefLocationId = x.RefLocationId,
                    ParentLocationId = x.ParentLocationId,
                    ParentLocation = x.ParentLocations.LocationName,
                    LocationName = x.LocationName,
                    RefLocationTypeId = x.RefLocationTypeId,
                    LocationType = x.LocationTypes.LocationType,
                    LocationLevel = x.LocationTypes.LocationLevel,
                    Count = x.Locations.Count()
                });

            ViewData["ParentId"] = id;
            
            ViewData["BackParentId"] = await _context.Locations
                .Where(x => x.ParentLocationId == id)
                .Select(x => x.ParentLocations.ParentLocationId)
                .FirstOrDefaultAsync();

            if (_context.LocationTypes.Any())
            {
                ViewData["MaxLocationLevel"] = await _context.LocationTypes.MaxAsync(x => x.LocationLevel);
            }
            else
            {
                ViewData["MaxLocationLevel"] = null;
            }
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Settings/Locations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refLocation = await _context.Locations
                .Include(r => r.LocationTypes)
                .Include(r => r.ParentLocations)
                .FirstOrDefaultAsync(m => m.RefLocationId == id);
            
            if (refLocation == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = refLocation.ParentLocationId;

            return View(refLocation);
        }

        // GET: Settings/Locations/Create
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Create(string id)
        {
            if (id != null)
            {
                var parent = await _context.Locations
                .Include(x => x.LocationTypes)
                .Where(x => x.RefLocationId == id)
                .FirstOrDefaultAsync();
                
                ViewData["RefLocationTypeId"] = new SelectList(_context.LocationTypes
                .Where(x => x.LocationLevel > parent.LocationTypes.LocationLevel), "RefLocationTypeId", "LocationType");
                
                ViewData["ParentLocationName"] = parent.LocationName;
                
                ViewData["ParentId"] = id;
            }
            else
            {
                ViewData["RefLocationTypeId"] = new SelectList(_context.LocationTypes, "RefLocationTypeId", "LocationType");
            }
            
            return View();
        }

        // POST: Settings/Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefLocationId,LocationName,RefLocationTypeId,ParentLocationId,Latitude,Longitude")] RefLocation refLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refLocation);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index), new { id = refLocation.ParentLocationId });
            }

            var parent = await _context.Locations
                .Include(x => x.LocationTypes)
                .Where(x => x.RefLocationId == refLocation.RefLocationId)
                .FirstOrDefaultAsync();

            ViewData["RefLocationTypeId"] = new SelectList(_context.LocationTypes
                .Where(x => x.LocationLevel > parent.LocationTypes.LocationLevel), "RefLocationTypeId", "LocationType");
            ViewData["ParentLocationName"] = parent.LocationName;
            ViewData["ParentId"] = refLocation.ParentLocationId;

            return View(refLocation);
        }

        // GET: Settings/Locations/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refLocation = await _context.Locations.FindAsync(id);

            if (refLocation == null)
            {
                return NotFound();
            }
            ViewData["RefLocationTypeId"] = new SelectList(_context.LocationTypes, "RefLocationTypeId", "LocationType", refLocation.RefLocationTypeId);
            ViewData["ParentLocationId"] = new SelectList(_context.Locations, "RefLocationId", "LocationName", refLocation.ParentLocationId);
            ViewData["ParentId"] = refLocation.ParentLocationId;
            return View(refLocation);
        }

        // POST: Settings/Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RefLocationId,LocationName,RefLocationTypeId,ParentLocationId,Latitude,Longitude")] RefLocation refLocation)
        {
            if (id != refLocation.RefLocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefLocationExists(refLocation.RefLocationId))
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
                
                return RedirectToAction(nameof(Index), new { id = refLocation.ParentLocationId });
            }
            ViewData["RefLocationTypeId"] = new SelectList(_context.LocationTypes, "RefLocationTypeId", "LocationType", refLocation.RefLocationTypeId);
            ViewData["ParentLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", refLocation.ParentLocationId);
            ViewData["ParentId"] = refLocation.ParentLocationId;
            return View(refLocation);
        }

        // GET: Settings/Locations/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refLocation = await _context.Locations
					.Include(r => r.LocationTypes)
					.Include(r => r.ParentLocations)
					.Include(m => m.Organizations)
					.Include(m => m.Participants)
					.Include(m => m.SchoolClusters)
					.Include(m => m.Locations)
					.FirstOrDefaultAsync(m => m.RefLocationId == id);

            ViewData["ParentId"] = refLocation.ParentLocationId;

            if (refLocation == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refLocation.Organizations.Count();    
            relatedCount += refLocation.Participants.Count();
			relatedCount += refLocation.SchoolClusters.Count();
			relatedCount += refLocation.Locations.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refLocation);
        }

        // POST: Settings/Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var refLocation = await _context.Locations.FindAsync(id);

            _context.Locations.Remove(refLocation);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index), new { id = refLocation.ParentLocationId });
        }

        private bool RefLocationExists(string id)
        {
            return _context.Locations.Any(e => e.RefLocationId == id);
        }
    }
}
