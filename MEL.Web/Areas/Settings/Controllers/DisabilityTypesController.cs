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
    public class DisabilityTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisabilityTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/DisabilityTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.DisabilityTypes.ToListAsync());
        }

        // GET: Settings/DisabilityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refDisabilityType = await _context.DisabilityTypes
                .FirstOrDefaultAsync(m => m.RefDisabilityTypeId == id);
            
            if (refDisabilityType == null)
            {
                return NotFound();
            }

            return View(refDisabilityType);
        }

        // GET: Settings/DisabilityTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/DisabilityTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefDisabilityTypeId,DisabilityTypeCode,DisabilityType")] RefDisabilityType refDisabilityType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refDisabilityType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refDisabilityType);
        }

        // GET: Settings/DisabilityTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refDisabilityType = await _context.DisabilityTypes.FindAsync(id);

            if (refDisabilityType == null)
            {
                return NotFound();
            }
            return View(refDisabilityType);
        }

        // POST: Settings/DisabilityTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefDisabilityTypeId,DisabilityTypeCode,DisabilityType")] RefDisabilityType refDisabilityType)
        {
            if (id != refDisabilityType.RefDisabilityTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refDisabilityType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefDisabilityTypeExists(refDisabilityType.RefDisabilityTypeId))
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
            return View(refDisabilityType);
        }

        // GET: Settings/DisabilityTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refDisabilityType = await _context.DisabilityTypes
					.Include(m => m.Participants)
					.FirstOrDefaultAsync(m => m.RefDisabilityTypeId == id);

            if (refDisabilityType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refDisabilityType.Participants.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refDisabilityType);
        }

        // POST: Settings/DisabilityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refDisabilityType = await _context.DisabilityTypes.FindAsync(id);

            _context.DisabilityTypes.Remove(refDisabilityType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefDisabilityTypeExists(int id)
        {
            return _context.DisabilityTypes.Any(e => e.RefDisabilityTypeId == id);
        }
    }
}
