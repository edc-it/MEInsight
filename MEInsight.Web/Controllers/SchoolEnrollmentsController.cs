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
    public class SchoolEnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SchoolEnrollmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SchoolEnrollments
        public async Task<IActionResult> Index(Guid? id)
        {
            
            ViewData["NextController"] = "";
            ViewData["ParentController"] = "";
            ViewData["ParentId"] = id;
            
            var applicationDbContext = _context.SchoolEnrollments.Include(s => s.GradeLevels).Include(s => s.ParticipantTypes).Include(s => s.SchoolPeriods).Include(s => s.Schools)
                    .Where(s => s.OrganizationId == id);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SchoolEnrollments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SchoolEnrollments == null)
            {
                return NotFound();
            }

            var schoolEnrollment = await _context.SchoolEnrollments
                .Include(s => s.GradeLevels)
                .Include(s => s.ParticipantTypes)
                .Include(s => s.SchoolPeriods)
                .Include(s => s.Schools)
                .FirstOrDefaultAsync(m => m.SchoolEnrollmentId == id);

            if (schoolEnrollment == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = schoolEnrollment.OrganizationId;

            return View(schoolEnrollment);
        }

        // GET: SchoolEnrollments/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = id;
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel");
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType");
            ViewData["SchoolPeriodId"] = new SelectList(_context.SchoolPeriods, "SchoolPeriodId", "PeriodName");
            ViewData["OrganizationId"] = new SelectList(_context.Schools, "OrganizationId", "OrganizationCode");
            return View();
        }

        // POST: SchoolEnrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolEnrollmentId,RegistrationDate,OrganizationId,SchoolPeriodId,RefParticipantTypeId,RefGradeLevelId,Male,Female,DisabledMale,DisabledFemale")] SchoolEnrollment schoolEnrollment)
        {
            if (ModelState.IsValid)
            {
                schoolEnrollment.SchoolEnrollmentId = Guid.NewGuid();
                _context.Add(schoolEnrollment);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), new { id = schoolEnrollment.OrganizationId });
            }
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel", schoolEnrollment.RefGradeLevelId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", schoolEnrollment.RefParticipantTypeId);
            ViewData["SchoolPeriodId"] = new SelectList(_context.SchoolPeriods, "SchoolPeriodId", "PeriodName", schoolEnrollment.SchoolPeriodId);
            ViewData["OrganizationId"] = new SelectList(_context.Schools, "OrganizationId", "OrganizationCode", schoolEnrollment.OrganizationId);
            ViewData["ParentId"] = schoolEnrollment.OrganizationId;
            return View(schoolEnrollment);
        }

        // GET: SchoolEnrollments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.SchoolEnrollments == null)
            {
                return NotFound();
            }

            var schoolEnrollment = await _context.SchoolEnrollments.FindAsync(id);
            if (schoolEnrollment == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = schoolEnrollment.OrganizationId;
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel", schoolEnrollment.RefGradeLevelId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", schoolEnrollment.RefParticipantTypeId);
            ViewData["SchoolPeriodId"] = new SelectList(_context.SchoolPeriods, "SchoolPeriodId", "PeriodName", schoolEnrollment.SchoolPeriodId);
            ViewData["OrganizationId"] = new SelectList(_context.Schools, "OrganizationId", "OrganizationCode", schoolEnrollment.OrganizationId);
            return View(schoolEnrollment);
        }

        // POST: SchoolEnrollments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SchoolEnrollmentId,RegistrationDate,OrganizationId,SchoolPeriodId,RefParticipantTypeId,RefGradeLevelId,Male,Female,DisabledMale,DisabledFemale")] SchoolEnrollment schoolEnrollment)
        {
            if (id != schoolEnrollment.SchoolEnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolEnrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolEnrollmentExists(schoolEnrollment.SchoolEnrollmentId))
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

                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), new { id = schoolEnrollment.OrganizationId });
            }
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel", schoolEnrollment.RefGradeLevelId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", schoolEnrollment.RefParticipantTypeId);
            ViewData["SchoolPeriodId"] = new SelectList(_context.SchoolPeriods, "SchoolPeriodId", "PeriodName", schoolEnrollment.SchoolPeriodId);
            ViewData["OrganizationId"] = new SelectList(_context.Schools, "OrganizationId", "OrganizationCode", schoolEnrollment.OrganizationId);
            ViewData["ParentId"] = schoolEnrollment.OrganizationId;
            return View(schoolEnrollment);
        }

        // GET: SchoolEnrollments/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.SchoolEnrollments == null)
            {
                return NotFound();
            }

            var schoolEnrollment = await _context.SchoolEnrollments
                .Include(s => s.GradeLevels)
                .Include(s => s.ParticipantTypes)
                .Include(s => s.SchoolPeriods)
                .Include(s => s.Schools)
                .FirstOrDefaultAsync(m => m.SchoolEnrollmentId == id);
            if (schoolEnrollment == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

            //relatedCount += schoolEnrollment.ICollection.Count;

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            ViewData["ParentId"] = schoolEnrollment.OrganizationId;

            return View(schoolEnrollment);
        }

        // POST: SchoolEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.SchoolEnrollments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SchoolEnrollments'  is null.");
            }
            var schoolEnrollment = await _context.SchoolEnrollments.FindAsync(id);
            if (schoolEnrollment != null)
            {
                _context.SchoolEnrollments.Remove(schoolEnrollment);
            }
            
            await _context.SaveChangesAsync();

            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            //return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index), new { id = schoolEnrollment.OrganizationId });
        }

        private bool SchoolEnrollmentExists(Guid id)
        {
          return (_context.SchoolEnrollments?.Any(e => e.SchoolEnrollmentId == id)).GetValueOrDefault();
        }
    }
}
