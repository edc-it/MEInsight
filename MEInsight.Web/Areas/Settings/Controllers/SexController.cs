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
    public class SexController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SexController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/Sex
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.Sex.ToListAsync());
        }

        // GET: Settings/Sex/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSex = await _context.Sex
                .FirstOrDefaultAsync(m => m.RefSexId == id);
            
            if (refSex == null)
            {
                return NotFound();
            }

            return View(refSex);
        }

        // GET: Settings/Sex/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/Sex/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefSexId,Sex,SexId")] RefSex refSex)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refSex);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refSex);
        }

        // GET: Settings/Sex/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSex = await _context.Sex.FindAsync(id);

            if (refSex == null)
            {
                return NotFound();
            }
            return View(refSex);
        }

        // POST: Settings/Sex/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefSexId,Sex,SexId")] RefSex refSex)
        {
            if (id != refSex.RefSexId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refSex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefSexExists(refSex.RefSexId))
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
            return View(refSex);
        }

        // GET: Settings/Sex/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refSex = await _context.Sex
                .Include(x => x.Participants)
                .FirstOrDefaultAsync(m => m.RefSexId == id);

            if (refSex == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                
            relatedCount += refSex.Participants.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refSex);
        }

        // POST: Settings/Sex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refSex = await _context.Sex.FindAsync(id);

            if (refSex != null)
            {
                _context.Sex.Remove(refSex);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefSexExists(int id)
        {
            return _context.Sex.Any(e => e.RefSexId == id);
        }
    }
}
