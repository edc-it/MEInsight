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
    public class TLMSubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TLMSubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/TLMSubjects
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.TLMSubjects.ToListAsync());
        }

        // GET: Settings/TLMSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMSubject = await _context.TLMSubjects
                .FirstOrDefaultAsync(m => m.RefTLMSubjectId == id);
            
            if (refTLMSubject == null)
            {
                return NotFound();
            }

            return View(refTLMSubject);
        }

        // GET: Settings/TLMSubjects/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/TLMSubjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTLMSubjectId,TLMSubjectCode,TLMSubject")] RefTLMSubject refTLMSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refTLMSubject);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refTLMSubject);
        }

        // GET: Settings/TLMSubjects/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMSubject = await _context.TLMSubjects.FindAsync(id);

            if (refTLMSubject == null)
            {
                return NotFound();
            }
            return View(refTLMSubject);
        }

        // POST: Settings/TLMSubjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefTLMSubjectId,TLMSubjectCode,TLMSubject")] RefTLMSubject refTLMSubject)
        {
            if (id != refTLMSubject.RefTLMSubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refTLMSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefTLMSubjectExists(refTLMSubject.RefTLMSubjectId))
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
            return View(refTLMSubject);
        }

        // GET: Settings/TLMSubjects/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMSubject = await _context.TLMSubjects
					.Include(m => m.TLMMaterials)
					.FirstOrDefaultAsync(m => m.RefTLMSubjectId == id);

            if (refTLMSubject == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refTLMSubject.TLMMaterials.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refTLMSubject);
        }

        // POST: Settings/TLMSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refTLMSubject = await _context.TLMSubjects.FindAsync(id);

            if (refTLMSubject != null)
            {
                _context.TLMSubjects.Remove(refTLMSubject);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefTLMSubjectExists(int id)
        {
            return _context.TLMSubjects.Any(e => e.RefTLMSubjectId == id);
        }
    }
}
