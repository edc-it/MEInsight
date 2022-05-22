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
    public class TLMLanguagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TLMLanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/TLMLanguages
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.TLMLanguages.ToListAsync());
        }

        // GET: Settings/TLMLanguages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMLanguage = await _context.TLMLanguages
                .FirstOrDefaultAsync(m => m.RefTLMLanguageId == id);
            
            if (refTLMLanguage == null)
            {
                return NotFound();
            }

            return View(refTLMLanguage);
        }

        // GET: Settings/TLMLanguages/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/TLMLanguages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTLMLanguageId,TLMLanguageCode,TLMLanguage")] RefTLMLanguage refTLMLanguage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refTLMLanguage);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refTLMLanguage);
        }

        // GET: Settings/TLMLanguages/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMLanguage = await _context.TLMLanguages.FindAsync(id);

            if (refTLMLanguage == null)
            {
                return NotFound();
            }
            return View(refTLMLanguage);
        }

        // POST: Settings/TLMLanguages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefTLMLanguageId,TLMLanguageCode,TLMLanguage")] RefTLMLanguage refTLMLanguage)
        {
            if (id != refTLMLanguage.RefTLMLanguageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refTLMLanguage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefTLMLanguageExists(refTLMLanguage.RefTLMLanguageId))
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
            return View(refTLMLanguage);
        }

        // GET: Settings/TLMLanguages/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMLanguage = await _context.TLMLanguages
					.Include(m => m.TLMMaterials)
					.FirstOrDefaultAsync(m => m.RefTLMLanguageId == id);

            if (refTLMLanguage == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refTLMLanguage.TLMMaterials.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refTLMLanguage);
        }

        // POST: Settings/TLMLanguages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refTLMLanguage = await _context.TLMLanguages.FindAsync(id);

            _context.TLMLanguages.Remove(refTLMLanguage);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefTLMLanguageExists(int id)
        {
            return _context.TLMLanguages.Any(e => e.RefTLMLanguageId == id);
        }
    }
}
