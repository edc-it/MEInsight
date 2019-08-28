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
    public class SchoolClustersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolClustersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/SchoolClusters
        public async Task<IActionResult> Index()
        {
        
            var applicationDbContext = _context.SchoolClusters.Include(r => r.Locations);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Settings/SchoolClusters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolCluster = await _context.SchoolClusters
                .Include(r => r.Locations)
                .FirstOrDefaultAsync(m => m.RefSchoolClusterId == id);
            
            if (refSchoolCluster == null)
            {
                return NotFound();
            }

            return View(refSchoolCluster);
        }

        // GET: Settings/SchoolClusters/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId");
            return View();
        }

        // POST: Settings/SchoolClusters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefSchoolClusterId,SchoolClusterCode,SchoolCluster,RefLocationId")] RefSchoolCluster refSchoolCluster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refSchoolCluster);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", refSchoolCluster.RefLocationId);
            return View(refSchoolCluster);
        }

        // GET: Settings/SchoolClusters/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolCluster = await _context.SchoolClusters.FindAsync(id);

            if (refSchoolCluster == null)
            {
                return NotFound();
            }
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", refSchoolCluster.RefLocationId);
            return View(refSchoolCluster);
        }

        // POST: Settings/SchoolClusters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefSchoolClusterId,SchoolClusterCode,SchoolCluster,RefLocationId")] RefSchoolCluster refSchoolCluster)
        {
            if (id != refSchoolCluster.RefSchoolClusterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refSchoolCluster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefSchoolClusterExists(refSchoolCluster.RefSchoolClusterId))
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
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", refSchoolCluster.RefLocationId);
            return View(refSchoolCluster);
        }

        // GET: Settings/SchoolClusters/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolCluster = await _context.SchoolClusters
					.Include(r => r.Locations)
					.Include(m => m.Schools)
					.FirstOrDefaultAsync(m => m.RefSchoolClusterId == id);

            if (refSchoolCluster == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refSchoolCluster.Schools.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refSchoolCluster);
        }

        // POST: Settings/SchoolClusters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refSchoolCluster = await _context.SchoolClusters.FindAsync(id);

            _context.SchoolClusters.Remove(refSchoolCluster);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefSchoolClusterExists(int id)
        {
            return _context.SchoolClusters.Any(e => e.RefSchoolClusterId == id);
        }
    }
}
