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
    public class ParticipantTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipantTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/ParticipantTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.ParticipantTypes.ToListAsync());
        }

        // GET: Settings/ParticipantTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refParticipantType = await _context.ParticipantTypes
                .FirstOrDefaultAsync(m => m.RefParticipantTypeId == id);
            
            if (refParticipantType == null)
            {
                return NotFound();
            }

            return View(refParticipantType);
        }

        // GET: Settings/ParticipantTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/ParticipantTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefParticipantTypeId,ParticipantTypeCode,ParticipantType")] RefParticipantType refParticipantType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refParticipantType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refParticipantType);
        }

        // GET: Settings/ParticipantTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refParticipantType = await _context.ParticipantTypes.FindAsync(id);

            if (refParticipantType == null)
            {
                return NotFound();
            }
            return View(refParticipantType);
        }

        // POST: Settings/ParticipantTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefParticipantTypeId,ParticipantTypeCode,ParticipantType")] RefParticipantType refParticipantType)
        {
            if (id != refParticipantType.RefParticipantTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refParticipantType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefParticipantTypeExists(refParticipantType.RefParticipantTypeId))
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
            return View(refParticipantType);
        }

        // GET: Settings/ParticipantTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refParticipantType = await _context.ParticipantTypes
					.Include(m => m.Participants)
					.Include(m => m.SchoolEnrollments)
                    .FirstOrDefaultAsync(m => m.RefParticipantTypeId == id);

            if (refParticipantType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refParticipantType.Participants.Count();
			relatedCount += refParticipantType.SchoolEnrollments.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refParticipantType);
        }

        // POST: Settings/ParticipantTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refParticipantType = await _context.ParticipantTypes.FindAsync(id);

            if (refParticipantType != null)
            {
                _context.ParticipantTypes.Remove(refParticipantType);
            }

            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefParticipantTypeExists(int id)
        {
            return _context.ParticipantTypes.Any(e => e.RefParticipantTypeId == id);
        }
    }
}
