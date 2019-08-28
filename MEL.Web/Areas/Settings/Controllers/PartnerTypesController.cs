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
    public class PartnerTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartnerTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/PartnerTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.PartnerTypes.ToListAsync());
        }

        // GET: Settings/PartnerTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refPartnerType = await _context.PartnerTypes
                .FirstOrDefaultAsync(m => m.RefPartnerTypeId == id);
            
            if (refPartnerType == null)
            {
                return NotFound();
            }

            return View(refPartnerType);
        }

        // GET: Settings/PartnerTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/PartnerTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefPartnerTypeId,PartnerTypeCode,PartnerType")] RefPartnerType refPartnerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refPartnerType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refPartnerType);
        }

        // GET: Settings/PartnerTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refPartnerType = await _context.PartnerTypes.FindAsync(id);

            if (refPartnerType == null)
            {
                return NotFound();
            }
            return View(refPartnerType);
        }

        // POST: Settings/PartnerTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefPartnerTypeId,PartnerTypeCode,PartnerType")] RefPartnerType refPartnerType)
        {
            if (id != refPartnerType.RefPartnerTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refPartnerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefPartnerTypeExists(refPartnerType.RefPartnerTypeId))
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
            return View(refPartnerType);
        }

        // GET: Settings/PartnerTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refPartnerType = await _context.PartnerTypes
					.Include(m => m.Partners)
					.FirstOrDefaultAsync(m => m.RefPartnerTypeId == id);

            if (refPartnerType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refPartnerType.Partners.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refPartnerType);
        }

        // POST: Settings/PartnerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refPartnerType = await _context.PartnerTypes.FindAsync(id);

            _context.PartnerTypes.Remove(refPartnerType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefPartnerTypeExists(int id)
        {
            return _context.PartnerTypes.Any(e => e.RefPartnerTypeId == id);
        }
    }
}
