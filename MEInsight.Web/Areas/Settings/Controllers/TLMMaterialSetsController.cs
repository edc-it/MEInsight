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
    public class TLMMaterialSetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TLMMaterialSetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/TLMMaterialSets
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.TLMMaterialSets.ToListAsync());
        }

        // GET: Settings/TLMMaterialSets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMMaterialSet = await _context.TLMMaterialSets
                .FirstOrDefaultAsync(m => m.RefTLMMaterialSetId == id);
            
            if (refTLMMaterialSet == null)
            {
                return NotFound();
            }

            return View(refTLMMaterialSet);
        }

        // GET: Settings/TLMMaterialSets/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/TLMMaterialSets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTLMMaterialSetId,TLMMaterialSetCode,TLMMaterialSet")] RefTLMMaterialSet refTLMMaterialSet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refTLMMaterialSet);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refTLMMaterialSet);
        }

        // GET: Settings/TLMMaterialSets/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMMaterialSet = await _context.TLMMaterialSets.FindAsync(id);

            if (refTLMMaterialSet == null)
            {
                return NotFound();
            }
            return View(refTLMMaterialSet);
        }

        // POST: Settings/TLMMaterialSets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefTLMMaterialSetId,TLMMaterialSetCode,TLMMaterialSet")] RefTLMMaterialSet refTLMMaterialSet)
        {
            if (id != refTLMMaterialSet.RefTLMMaterialSetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refTLMMaterialSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefTLMMaterialSetExists(refTLMMaterialSet.RefTLMMaterialSetId))
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
            return View(refTLMMaterialSet);
        }

        // GET: Settings/TLMMaterialSets/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMMaterialSet = await _context.TLMMaterialSets
					.Include(m => m.TLMMaterials)
					.FirstOrDefaultAsync(m => m.RefTLMMaterialSetId == id);

            if (refTLMMaterialSet == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refTLMMaterialSet.TLMMaterials.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refTLMMaterialSet);
        }

        // POST: Settings/TLMMaterialSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refTLMMaterialSet = await _context.TLMMaterialSets.FindAsync(id);

            if (refTLMMaterialSet != null)
            {
                _context.TLMMaterialSets.Remove(refTLMMaterialSet);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefTLMMaterialSetExists(int id)
        {
            return _context.TLMMaterialSets.Any(e => e.RefTLMMaterialSetId == id);
        }
    }
}
