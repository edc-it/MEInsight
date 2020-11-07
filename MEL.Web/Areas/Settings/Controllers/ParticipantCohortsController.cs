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
    public class ParticipantCohortsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipantCohortsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/ParticipantCohorts
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.ParticipantCohorts.ToListAsync());
        }

        // GET: Settings/ParticipantCohorts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refParticipantCohort = await _context.ParticipantCohorts
                .FirstOrDefaultAsync(m => m.RefParticipantCohortId == id);
            
            if (refParticipantCohort == null)
            {
                return NotFound();
            }

            return View(refParticipantCohort);
        }

        // GET: Settings/ParticipantCohorts/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/ParticipantCohorts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefParticipantCohortId,ParticipantCohortCode,ParticipantCohort")] RefParticipantCohort refParticipantCohort)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refParticipantCohort);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refParticipantCohort);
        }

        // GET: Settings/ParticipantCohorts/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refParticipantCohort = await _context.ParticipantCohorts.FindAsync(id);

            if (refParticipantCohort == null)
            {
                return NotFound();
            }
            return View(refParticipantCohort);
        }

        // POST: Settings/ParticipantCohorts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefParticipantCohortId,ParticipantCohortCode,ParticipantCohort")] RefParticipantCohort refParticipantCohort)
        {
            if (id != refParticipantCohort.RefParticipantCohortId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refParticipantCohort);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefParticipantCohortExists(refParticipantCohort.RefParticipantCohortId))
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
            return View(refParticipantCohort);
        }

        // GET: Settings/ParticipantCohorts/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refParticipantCohort = await _context.ParticipantCohorts
                    .FirstOrDefaultAsync(m => m.RefParticipantCohortId == id);

            if (refParticipantCohort == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                

            //relatedCount += refParticipantCohort.ICollection.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refParticipantCohort);
        }

        // POST: Settings/ParticipantCohorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refParticipantCohort = await _context.ParticipantCohorts.FindAsync(id);

            _context.ParticipantCohorts.Remove(refParticipantCohort);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefParticipantCohortExists(int id)
        {
            return _context.ParticipantCohorts.Any(e => e.RefParticipantCohortId == id);
        }
    }
}
