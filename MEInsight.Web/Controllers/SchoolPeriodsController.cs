using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MEInsight.Entities.Identity;
using MEInsight.Entities.Core;
using MEInsight.Web.Data;

namespace MEInsight.Web.Controllers
{
    [Authorize]
    public class SchoolPeriodsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchoolPeriodsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SchoolPeriods
        public async Task<IActionResult> Index(Guid? id)
        {
            
            ViewData["NextController"] = "";
            ViewData["ParentController"] = "";
            ViewData["ParentId"] = id;
            
              return _context.SchoolPeriods != null ? 
                          View(await _context.SchoolPeriods.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SchoolPeriods'  is null.");
        }

        // GET: SchoolPeriods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SchoolPeriods == null)
            {
                return NotFound();
            }

            var schoolPeriod = await _context.SchoolPeriods
                .FirstOrDefaultAsync(m => m.SchoolPeriodId == id);

            if (schoolPeriod == null)
            {
                return NotFound();
            }

            //ViewData["ParentId"] = schoolPeriod..ParentId;

            return View(schoolPeriod);
        }

        // GET: SchoolPeriods/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = id;
            return View();
        }

        // POST: SchoolPeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolPeriodId,PeriodName,StartDate,EndDate")] SchoolPeriod schoolPeriod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolPeriod);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
                //return RedirectToAction(nameof(Index), new { id = schoolPeriod.ParentId });
            }
            //ViewData["ParentId"] = schoolPeriod.ParentId;
            return View(schoolPeriod);
        }

        // GET: SchoolPeriods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SchoolPeriods == null)
            {
                return NotFound();
            }

            var schoolPeriod = await _context.SchoolPeriods.FindAsync(id);
            if (schoolPeriod == null)
            {
                return NotFound();
            }
            //ViewData["ParentId"] = schoolPeriod.ParentId;
            return View(schoolPeriod);
        }

        // POST: SchoolPeriods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchoolPeriodId,PeriodName,StartDate,EndDate")] SchoolPeriod schoolPeriod)
        {
            if (id != schoolPeriod.SchoolPeriodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolPeriod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolPeriodExists(schoolPeriod.SchoolPeriodId))
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
                //return RedirectToAction(nameof(Index), new { id = schoolPeriod.ParentId });
            }
            //ViewData["ParentId"] = schoolPeriod.ParentId;
            return View(schoolPeriod);
        }

        // GET: SchoolPeriods/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SchoolPeriods == null)
            {
                return NotFound();
            }

            var schoolPeriod = await _context.SchoolPeriods
                .FirstOrDefaultAsync(m => m.SchoolPeriodId == id);
            if (schoolPeriod == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                

            //relatedCount += schoolPeriod.ICollection.Count;

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            //ViewData["ParentId"] = schoolPeriod.ParentId;

            return View(schoolPeriod);
        }

        // POST: SchoolPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SchoolPeriods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SchoolPeriods'  is null.");
            }
            var schoolPeriod = await _context.SchoolPeriods.FindAsync(id);
            if (schoolPeriod != null)
            {
                _context.SchoolPeriods.Remove(schoolPeriod);
            }
            
            await _context.SaveChangesAsync();

            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            return RedirectToAction(nameof(Index));
            //return RedirectToAction(nameof(Index), new { id = schoolPeriod.ParentId });
        }

        private bool SchoolPeriodExists(int id)
        {
          return (_context.SchoolPeriods?.Any(e => e.SchoolPeriodId == id)).GetValueOrDefault();
        }
    }
}
