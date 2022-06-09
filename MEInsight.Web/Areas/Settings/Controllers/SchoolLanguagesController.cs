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
    public class SchoolLanguagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolLanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/SchoolLanguages
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.SchoolLanguages.ToListAsync());
        }

        // GET: Settings/SchoolLanguages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolLanguage = await _context.SchoolLanguages
                .FirstOrDefaultAsync(m => m.RefSchoolLanguageId == id);
            
            if (refSchoolLanguage == null)
            {
                return NotFound();
            }

            return View(refSchoolLanguage);
        }

        // GET: Settings/SchoolLanguages/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/SchoolLanguages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefSchoolLanguageId,SchoolLanguageCode,SchoolLanguage")] RefSchoolLanguage refSchoolLanguage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refSchoolLanguage);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refSchoolLanguage);
        }

        // GET: Settings/SchoolLanguages/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolLanguage = await _context.SchoolLanguages.FindAsync(id);

            if (refSchoolLanguage == null)
            {
                return NotFound();
            }
            return View(refSchoolLanguage);
        }

        // POST: Settings/SchoolLanguages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefSchoolLanguageId,SchoolLanguageCode,SchoolLanguage")] RefSchoolLanguage refSchoolLanguage)
        {
            if (id != refSchoolLanguage.RefSchoolLanguageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refSchoolLanguage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefSchoolLanguageExists(refSchoolLanguage.RefSchoolLanguageId))
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
            return View(refSchoolLanguage);
        }

        // GET: Settings/SchoolLanguages/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSchoolLanguage = await _context.SchoolLanguages
                    .FirstOrDefaultAsync(m => m.RefSchoolLanguageId == id);

            if (refSchoolLanguage == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                

            relatedCount += refSchoolLanguage.Schools.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refSchoolLanguage);
        }

        // POST: Settings/SchoolLanguages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refSchoolLanguage = await _context.SchoolLanguages.FindAsync(id);

            if (refSchoolLanguage != null)
            {
                _context.SchoolLanguages.Remove(refSchoolLanguage);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefSchoolLanguageExists(int id)
        {
            return _context.SchoolLanguages.Any(e => e.RefSchoolLanguageId == id);
        }
    }
}
