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
    public class EducationAdministratorOfficesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationAdministratorOfficesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/EducationAdministratorOffices
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.EducationAdministratorOffices.ToListAsync());
        }

        // GET: Settings/EducationAdministratorOffices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEducationAdministratorOffice = await _context.EducationAdministratorOffices
                .FirstOrDefaultAsync(m => m.RefEducationAdministratorOfficeId == id);
            
            if (refEducationAdministratorOffice == null)
            {
                return NotFound();
            }

            return View(refEducationAdministratorOffice);
        }

        // GET: Settings/EducationAdministratorOffices/Create
        [Authorize(Policy = "RequireMELRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/EducationAdministratorOffices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefEducationAdministratorOfficeId,EducationAdministratorOfficeCode,EducationAdministratorOffice")] RefEducationAdministratorOffice refEducationAdministratorOffice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refEducationAdministratorOffice);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refEducationAdministratorOffice);
        }

        // GET: Settings/EducationAdministratorOffices/Edit/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEducationAdministratorOffice = await _context.EducationAdministratorOffices.FindAsync(id);

            if (refEducationAdministratorOffice == null)
            {
                return NotFound();
            }
            return View(refEducationAdministratorOffice);
        }

        // POST: Settings/EducationAdministratorOffices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefEducationAdministratorOfficeId,EducationAdministratorOfficeCode,EducationAdministratorOffice")] RefEducationAdministratorOffice refEducationAdministratorOffice)
        {
            if (id != refEducationAdministratorOffice.RefEducationAdministratorOfficeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refEducationAdministratorOffice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefEducationAdministratorOfficeExists(refEducationAdministratorOffice.RefEducationAdministratorOfficeId))
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
            return View(refEducationAdministratorOffice);
        }

        // GET: Settings/EducationAdministratorOffices/Delete/5
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refEducationAdministratorOffice = await _context.EducationAdministratorOffices
					.Include(m => m.EducationAdministrators)
                    .FirstOrDefaultAsync(m => m.RefEducationAdministratorOfficeId == id);

            if (refEducationAdministratorOffice == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

			relatedCount += refEducationAdministratorOffice.EducationAdministrators.Count();

			if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refEducationAdministratorOffice);
        }

        // POST: Settings/EducationAdministratorOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refEducationAdministratorOffice = await _context.EducationAdministratorOffices.FindAsync(id);

            _context.EducationAdministratorOffices.Remove(refEducationAdministratorOffice);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefEducationAdministratorOfficeExists(int id)
        {
            return _context.EducationAdministratorOffices.Any(e => e.RefEducationAdministratorOfficeId == id);
        }
    }
}
