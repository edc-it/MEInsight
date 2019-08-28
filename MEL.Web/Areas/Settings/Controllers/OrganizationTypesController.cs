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
    public class OrganizationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizationTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/OrganizationTypes
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.OrganizationTypes.ToListAsync());
        }

        // GET: Settings/OrganizationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refOrganizationType = await _context.OrganizationTypes
                .FirstOrDefaultAsync(m => m.RefOrganizationTypeId == id);
            
            if (refOrganizationType == null)
            {
                return NotFound();
            }

            return View(refOrganizationType);
        }

        // GET: Settings/OrganizationTypes/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/OrganizationTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefOrganizationTypeId,OrganizationType,OrganizationTypeCode")] RefOrganizationType refOrganizationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refOrganizationType);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refOrganizationType);
        }

        // GET: Settings/OrganizationTypes/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refOrganizationType = await _context.OrganizationTypes.FindAsync(id);

            if (refOrganizationType == null)
            {
                return NotFound();
            }
            return View(refOrganizationType);
        }

        // POST: Settings/OrganizationTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefOrganizationTypeId,OrganizationType,OrganizationTypeCode")] RefOrganizationType refOrganizationType)
        {
            if (id != refOrganizationType.RefOrganizationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refOrganizationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefOrganizationTypeExists(refOrganizationType.RefOrganizationTypeId))
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
            return View(refOrganizationType);
        }

        // GET: Settings/OrganizationTypes/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refOrganizationType = await _context.OrganizationTypes
                .Include(m => m.Organizations)
                .Include(m => m.Programs)
                .FirstOrDefaultAsync(m => m.RefOrganizationTypeId == id);

            if (refOrganizationType == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                

            relatedCount += refOrganizationType.Organizations.Count();
            relatedCount += refOrganizationType.Programs.Count();

            if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refOrganizationType);
        }

        // POST: Settings/OrganizationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refOrganizationType = await _context.OrganizationTypes.FindAsync(id);

            _context.OrganizationTypes.Remove(refOrganizationType);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefOrganizationTypeExists(int id)
        {
            return _context.OrganizationTypes.Any(e => e.RefOrganizationTypeId == id);
        }
    }
}
