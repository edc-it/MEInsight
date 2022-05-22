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
    public class TLMGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TLMGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/TLMGroups
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.TLMGroups.ToListAsync());
        }

        // GET: Settings/TLMGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMGroup = await _context.TLMGroups
                .FirstOrDefaultAsync(m => m.RefTLMGroupId == id);
            
            if (refTLMGroup == null)
            {
                return NotFound();
            }

            return View(refTLMGroup);
        }

        // GET: Settings/TLMGroups/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/TLMGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTLMGroupId,TLMGroupCode,TLMGroup")] RefTLMGroup refTLMGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refTLMGroup);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refTLMGroup);
        }

        // GET: Settings/TLMGroups/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMGroup = await _context.TLMGroups.FindAsync(id);

            if (refTLMGroup == null)
            {
                return NotFound();
            }
            return View(refTLMGroup);
        }

        // POST: Settings/TLMGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefTLMGroupId,TLMGroupCode,TLMGroup")] RefTLMGroup refTLMGroup)
        {
            if (id != refTLMGroup.RefTLMGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refTLMGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefTLMGroupExists(refTLMGroup.RefTLMGroupId))
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
            return View(refTLMGroup);
        }

        // GET: Settings/TLMGroups/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refTLMGroup = await _context.TLMGroups
					.Include(m => m.TLMMaterials)
					.FirstOrDefaultAsync(m => m.RefTLMGroupId == id);

            if (refTLMGroup == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refTLMGroup.TLMMaterials.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refTLMGroup);
        }

        // POST: Settings/TLMGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refTLMGroup = await _context.TLMGroups.FindAsync(id);

            _context.TLMGroups.Remove(refTLMGroup);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefTLMGroupExists(int id)
        {
            return _context.TLMGroups.Any(e => e.RefTLMGroupId == id);
        }
    }
}
