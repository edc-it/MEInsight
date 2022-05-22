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
    public class EnrollmentStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/EnrollmentStatus
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.EnrollmentStatus.ToListAsync());
        }

        // GET: Settings/EnrollmentStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEnrollmentStatus = await _context.EnrollmentStatus
                .FirstOrDefaultAsync(m => m.RefEnrollmentStatusId == id);
            
            if (refEnrollmentStatus == null)
            {
                return NotFound();
            }

            return View(refEnrollmentStatus);
        }

        // GET: Settings/EnrollmentStatus/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/EnrollmentStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefEnrollmentStatusId,EnrollmentStatusCode,EnrollmentStatus")] RefEnrollmentStatus refEnrollmentStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refEnrollmentStatus);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refEnrollmentStatus);
        }

        // GET: Settings/EnrollmentStatus/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEnrollmentStatus = await _context.EnrollmentStatus.FindAsync(id);

            if (refEnrollmentStatus == null)
            {
                return NotFound();
            }
            return View(refEnrollmentStatus);
        }

        // POST: Settings/EnrollmentStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefEnrollmentStatusId,EnrollmentStatusCode,EnrollmentStatus")] RefEnrollmentStatus refEnrollmentStatus)
        {
            if (id != refEnrollmentStatus.RefEnrollmentStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refEnrollmentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefEnrollmentStatusExists(refEnrollmentStatus.RefEnrollmentStatusId))
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
            return View(refEnrollmentStatus);
        }

        // GET: Settings/EnrollmentStatus/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEnrollmentStatus = await _context.EnrollmentStatus
					.Include(m => m.GroupEnrollments)
                    .FirstOrDefaultAsync(m => m.RefEnrollmentStatusId == id);

            if (refEnrollmentStatus == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refEnrollmentStatus.GroupEnrollments.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refEnrollmentStatus);
        }

        // POST: Settings/EnrollmentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refEnrollmentStatus = await _context.EnrollmentStatus.FindAsync(id);

            _context.EnrollmentStatus.Remove(refEnrollmentStatus);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefEnrollmentStatusExists(int id)
        {
            return _context.EnrollmentStatus.Any(e => e.RefEnrollmentStatusId == id);
        }
    }
}
