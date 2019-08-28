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
    public class PartnerSectorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartnerSectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/PartnerSectors
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.PartnerSectors.ToListAsync());
        }

        // GET: Settings/PartnerSectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refPartnerSector = await _context.PartnerSectors
                .FirstOrDefaultAsync(m => m.RefPartnerSectorId == id);
            
            if (refPartnerSector == null)
            {
                return NotFound();
            }

            return View(refPartnerSector);
        }

        // GET: Settings/PartnerSectors/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/PartnerSectors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefPartnerSectorId,PartnerSectorCode,PartnerSector")] RefPartnerSector refPartnerSector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refPartnerSector);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refPartnerSector);
        }

        // GET: Settings/PartnerSectors/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refPartnerSector = await _context.PartnerSectors.FindAsync(id);

            if (refPartnerSector == null)
            {
                return NotFound();
            }
            return View(refPartnerSector);
        }

        // POST: Settings/PartnerSectors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefPartnerSectorId,PartnerSectorCode,PartnerSector")] RefPartnerSector refPartnerSector)
        {
            if (id != refPartnerSector.RefPartnerSectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refPartnerSector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefPartnerSectorExists(refPartnerSector.RefPartnerSectorId))
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
            return View(refPartnerSector);
        }

        // GET: Settings/PartnerSectors/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refPartnerSector = await _context.PartnerSectors
					.Include(m => m.Partners)
					.FirstOrDefaultAsync(m => m.RefPartnerSectorId == id);

            if (refPartnerSector == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refPartnerSector.Partners.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refPartnerSector);
        }

        // POST: Settings/PartnerSectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refPartnerSector = await _context.PartnerSectors.FindAsync(id);

            _context.PartnerSectors.Remove(refPartnerSector);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefPartnerSectorExists(int id)
        {
            return _context.PartnerSectors.Any(e => e.RefPartnerSectorId == id);
        }
    }
}
