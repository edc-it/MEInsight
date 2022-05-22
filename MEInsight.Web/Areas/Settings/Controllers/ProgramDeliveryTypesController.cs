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
    public class ProgramDeliveryTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgramDeliveryTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/ProgramDeliveryTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.ProgramDeliveryTypes.ToListAsync());
        }

        // GET: Settings/ProgramDeliveryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refProgramDeliveryType = await _context.ProgramDeliveryTypes
                .FirstOrDefaultAsync(m => m.RefProgramDeliveryTypeId == id);
            
            if (refProgramDeliveryType == null)
            {
                return NotFound();
            }

            return View(refProgramDeliveryType);
        }

        // GET: Settings/ProgramDeliveryTypes/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/ProgramDeliveryTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefProgramDeliveryTypeId,ProgramDeliveryTypeCode,ProgramDeliveryType")] RefProgramDeliveryType refProgramDeliveryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refProgramDeliveryType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refProgramDeliveryType);
        }

        // GET: Settings/ProgramDeliveryTypes/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refProgramDeliveryType = await _context.ProgramDeliveryTypes.FindAsync(id);

            if (refProgramDeliveryType == null)
            {
                return NotFound();
            }
            return View(refProgramDeliveryType);
        }

        // POST: Settings/ProgramDeliveryTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefProgramDeliveryTypeId,ProgramDeliveryTypeCode,ProgramDeliveryType")] RefProgramDeliveryType refProgramDeliveryType)
        {
            if (id != refProgramDeliveryType.RefProgramDeliveryTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refProgramDeliveryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefProgramDeliveryTypeExists(refProgramDeliveryType.RefProgramDeliveryTypeId))
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
            return View(refProgramDeliveryType);
        }

        // GET: Settings/ProgramDeliveryTypes/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refProgramDeliveryType = await _context.ProgramDeliveryTypes
                    .FirstOrDefaultAsync(m => m.RefProgramDeliveryTypeId == id);

            if (refProgramDeliveryType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                

            //relatedCount += refProgramDeliveryType.ICollection.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refProgramDeliveryType);
        }

        // POST: Settings/ProgramDeliveryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refProgramDeliveryType = await _context.ProgramDeliveryTypes.FindAsync(id);

            _context.ProgramDeliveryTypes.Remove(refProgramDeliveryType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefProgramDeliveryTypeExists(int id)
        {
            return _context.ProgramDeliveryTypes.Any(e => e.RefProgramDeliveryTypeId == id);
        }
    }
}
