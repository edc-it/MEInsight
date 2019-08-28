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
    public class TLMMaterialTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TLMMaterialTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/TLMMaterialTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.TLMMaterialTypes.ToListAsync());
        }

        // GET: Settings/TLMMaterialTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMMaterialType = await _context.TLMMaterialTypes
                .FirstOrDefaultAsync(m => m.RefTLMMaterialTypeId == id);
            
            if (refTLMMaterialType == null)
            {
                return NotFound();
            }

            return View(refTLMMaterialType);
        }

        // GET: Settings/TLMMaterialTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/TLMMaterialTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTLMMaterialTypeId,TLMMaterialTypeCode,TLMMaterialType")] RefTLMMaterialType refTLMMaterialType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refTLMMaterialType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refTLMMaterialType);
        }

        // GET: Settings/TLMMaterialTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMMaterialType = await _context.TLMMaterialTypes.FindAsync(id);

            if (refTLMMaterialType == null)
            {
                return NotFound();
            }
            return View(refTLMMaterialType);
        }

        // POST: Settings/TLMMaterialTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefTLMMaterialTypeId,TLMMaterialTypeCode,TLMMaterialType")] RefTLMMaterialType refTLMMaterialType)
        {
            if (id != refTLMMaterialType.RefTLMMaterialTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refTLMMaterialType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefTLMMaterialTypeExists(refTLMMaterialType.RefTLMMaterialTypeId))
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
            return View(refTLMMaterialType);
        }

        // GET: Settings/TLMMaterialTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMMaterialType = await _context.TLMMaterialTypes
					.Include(m => m.TLMMaterials)
					.FirstOrDefaultAsync(m => m.RefTLMMaterialTypeId == id);

            if (refTLMMaterialType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                
            relatedCount += refTLMMaterialType.TLMMaterials.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refTLMMaterialType);
        }

        // POST: Settings/TLMMaterialTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refTLMMaterialType = await _context.TLMMaterialTypes.FindAsync(id);

            _context.TLMMaterialTypes.Remove(refTLMMaterialType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefTLMMaterialTypeExists(int id)
        {
            return _context.TLMMaterialTypes.Any(e => e.RefTLMMaterialTypeId == id);
        }
    }
}
