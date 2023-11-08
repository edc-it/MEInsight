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
    public class StudentYearOfStudiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentYearOfStudiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings/StudentYearOfStudies
        public async Task<IActionResult> Index()
        {
        
            return View(await _context.StudentYearOfStudies.ToListAsync());
        }

        // GET: Settings/StudentYearOfStudies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentYearOfStudy = await _context.StudentYearOfStudies
                .FirstOrDefaultAsync(m => m.RefStudentYearOfStudyId == id);
            
            if (refStudentYearOfStudy == null)
            {
                return NotFound();
            }

            return View(refStudentYearOfStudy);
        }

        // GET: Settings/StudentYearOfStudies/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/StudentYearOfStudies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefStudentYearOfStudyId,StudentYearOfStudyCode,StudentYearOfStudy")] RefStudentYearOfStudy refStudentYearOfStudy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(refStudentYearOfStudy);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));
            }
            return View(refStudentYearOfStudy);
        }

        // GET: Settings/StudentYearOfStudies/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentYearOfStudy = await _context.StudentYearOfStudies.FindAsync(id);

            if (refStudentYearOfStudy == null)
            {
                return NotFound();
            }
            return View(refStudentYearOfStudy);
        }

        // POST: Settings/StudentYearOfStudies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefStudentYearOfStudyId,StudentYearOfStudyCode,StudentYearOfStudy")] RefStudentYearOfStudy refStudentYearOfStudy)
        {
            if (id != refStudentYearOfStudy.RefStudentYearOfStudyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(refStudentYearOfStudy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefStudentYearOfStudyExists(refStudentYearOfStudy.RefStudentYearOfStudyId))
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
            return View(refStudentYearOfStudy);
        }

        // GET: Settings/StudentYearOfStudies/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refStudentYearOfStudy = await _context.StudentYearOfStudies
                    .FirstOrDefaultAsync(m => m.RefStudentYearOfStudyId == id);

            if (refStudentYearOfStudy == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                

            //relatedCount += refStudentYearOfStudy.ICollection.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(refStudentYearOfStudy);
        }

        // POST: Settings/StudentYearOfStudies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refStudentYearOfStudy = await _context.StudentYearOfStudies.FindAsync(id);

            if (refStudentYearOfStudy != null)
            {
                _context.StudentYearOfStudies.Remove(refStudentYearOfStudy);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";
        
            return RedirectToAction(nameof(Index));
        }

        private bool RefStudentYearOfStudyExists(int id)
        {
            return _context.StudentYearOfStudies.Any(e => e.RefStudentYearOfStudyId == id);
        }
    }
}
