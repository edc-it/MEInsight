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
    public class ProgramTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgramTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/ProgramTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.ProgramTypes.ToListAsync());
        }

        // GET: Settings/ProgramTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refProgramType = await _context.ProgramTypes
                .FirstOrDefaultAsync(m => m.RefProgramTypeId == id);
            
            if (refProgramType == null)
            {
                return NotFound();
            }

            return View(refProgramType);
        }

        // GET: Settings/ProgramTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/ProgramTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefProgramTypeId,ProgramTypeCode,ProgramType")] RefProgramType refProgramType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refProgramType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refProgramType);
        }

        // GET: Settings/ProgramTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refProgramType = await _context.ProgramTypes.FindAsync(id);

            if (refProgramType == null)
            {
                return NotFound();
            }
            return View(refProgramType);
        }

        // POST: Settings/ProgramTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefProgramTypeId,ProgramTypeCode,ProgramType")] RefProgramType refProgramType)
        {
            if (id != refProgramType.RefProgramTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refProgramType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefProgramTypeExists(refProgramType.RefProgramTypeId))
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
            return View(refProgramType);
        }

        // GET: Settings/ProgramTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refProgramType = await _context.ProgramTypes
					.Include(m => m.Programs)
					.FirstOrDefaultAsync(m => m.RefProgramTypeId == id);

            if (refProgramType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refProgramType.Programs.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refProgramType);
        }

        // POST: Settings/ProgramTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refProgramType = await _context.ProgramTypes.FindAsync(id);

            _context.ProgramTypes.Remove(refProgramType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefProgramTypeExists(int id)
        {
            return _context.ProgramTypes.Any(e => e.RefProgramTypeId == id);
        }
    }
}
