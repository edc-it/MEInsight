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
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Students
        public async Task<IActionResult> Index(Guid? id)
        {
            
            ViewData["NextController"] = "";
            ViewData["ParentController"] = "";
            ViewData["ParentId"] = id;
            
            var applicationDbContext = _context.Students.Include(s => s.DisabilityTypes).Include(s => s.Locations).Include(s => s.Organizations).Include(s => s.ParticipantCohorts).Include(s => s.ParticipantTypes).Include(s => s.Sex).Include(s => s.StudentSpecializations).Include(s => s.StudentTypes).Include(s => s.StudentYearOfStudies)
                    .Where(s => s.OrganizationId == id);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.DisabilityTypes)
                .Include(s => s.Locations)
                .Include(s => s.Organizations)
                .Include(s => s.ParticipantCohorts)
                .Include(s => s.ParticipantTypes)
                .Include(s => s.Sex)
                .Include(s => s.StudentSpecializations)
                .Include(s => s.StudentTypes)
                .Include(s => s.StudentYearOfStudies)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);

            if (student == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = student.OrganizationId;

            return View(student);
        }

        // GET: Students/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create(Guid? id)
        {

            //if (id == null)
            //{
            //    return NotFound();
            //}

            ViewData["ParentId"] = id;
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType");
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId");
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode");
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort");
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType");
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex");
            ViewData["RefStudentSpecializationId"] = new SelectList(_context.StudentSpecializations, "RefStudentSpecializationId", "StudentSpecialization");
            ViewData["RefStudentTypeId"] = new SelectList(_context.StudentTypes, "RefStudentTypeId", "StudentType");
            ViewData["RefStudentYearOfStudyId"] = new SelectList(_context.StudentYearOfStudies, "RefStudentYearOfStudyId", "StudentYearOfStudy");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentCode,RefStudentTypeId,RefStudentSpecializationId,ParentGuardian,RefStudentYearOfStudyId,ParticipantId,RegistrationDate,OrganizationId,RefParticipantTypeId,RefParticipantCohortId,ParticipantCode,FirstName,MiddleName,LastName,RefSexId,BirthDate,Age,Disability,RefDisabilityTypeId,Phone,Mobile,Email,Facebook,InstantMessenger,RefLocationId,Address,AdditionalData")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.ParticipantId = Guid.NewGuid();
                _context.Add(student);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), new { id = student.OrganizationId });
            }
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", student.RefDisabilityTypeId);
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", student.RefLocationId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", student.OrganizationId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", student.RefParticipantCohortId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", student.RefParticipantTypeId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", student.RefSexId);
            ViewData["RefStudentSpecializationId"] = new SelectList(_context.StudentSpecializations, "RefStudentSpecializationId", "StudentSpecialization", student.RefStudentSpecializationId);
            ViewData["RefStudentTypeId"] = new SelectList(_context.StudentTypes, "RefStudentTypeId", "StudentType", student.RefStudentTypeId);
            ViewData["RefStudentYearOfStudyId"] = new SelectList(_context.StudentYearOfStudies, "RefStudentYearOfStudyId", "StudentYearOfStudy", student.RefStudentYearOfStudyId);
            ViewData["ParentId"] = student.OrganizationId;
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = student.OrganizationId;
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", student.RefDisabilityTypeId);
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", student.RefLocationId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", student.OrganizationId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", student.RefParticipantCohortId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", student.RefParticipantTypeId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", student.RefSexId);
            ViewData["RefStudentSpecializationId"] = new SelectList(_context.StudentSpecializations, "RefStudentSpecializationId", "StudentSpecialization", student.RefStudentSpecializationId);
            ViewData["RefStudentTypeId"] = new SelectList(_context.StudentTypes, "RefStudentTypeId", "StudentType", student.RefStudentTypeId);
            ViewData["RefStudentYearOfStudyId"] = new SelectList(_context.StudentYearOfStudies, "RefStudentYearOfStudyId", "StudentYearOfStudy", student.RefStudentYearOfStudyId);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StudentCode,RefStudentTypeId,RefStudentSpecializationId,ParentGuardian,RefStudentYearOfStudyId,ParticipantId,RegistrationDate,OrganizationId,RefParticipantTypeId,RefParticipantCohortId,ParticipantCode,FirstName,MiddleName,LastName,RefSexId,BirthDate,Age,Disability,RefDisabilityTypeId,Phone,Mobile,Email,Facebook,InstantMessenger,RefLocationId,Address,AdditionalData")] Student student)
        {
            if (id != student.ParticipantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ParticipantId))
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
                return RedirectToAction(nameof(Index), new { id = student.OrganizationId });
            }
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", student.RefDisabilityTypeId);
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", student.RefLocationId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", student.OrganizationId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", student.RefParticipantCohortId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", student.RefParticipantTypeId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", student.RefSexId);
            ViewData["RefStudentSpecializationId"] = new SelectList(_context.StudentSpecializations, "RefStudentSpecializationId", "StudentSpecialization", student.RefStudentSpecializationId);
            ViewData["RefStudentTypeId"] = new SelectList(_context.StudentTypes, "RefStudentTypeId", "StudentType", student.RefStudentTypeId);
            ViewData["RefStudentYearOfStudyId"] = new SelectList(_context.StudentYearOfStudies, "RefStudentYearOfStudyId", "StudentYearOfStudy", student.RefStudentYearOfStudyId);
            ViewData["ParentId"] = student.OrganizationId;
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.DisabilityTypes)
                .Include(s => s.Locations)
                .Include(s => s.Organizations)
                .Include(s => s.ParticipantCohorts)
                .Include(s => s.ParticipantTypes)
                .Include(s => s.Sex)
                .Include(s => s.StudentSpecializations)
                .Include(s => s.StudentTypes)
                .Include(s => s.StudentYearOfStudies)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);
            if (student == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            ViewData["ParentId"] = student.OrganizationId;

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();

            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            //return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index), new { id = student.OrganizationId });
        }

        private bool StudentExists(Guid id)
        {
          return (_context.Students?.Any(e => e.ParticipantId == id)).GetValueOrDefault();
        }
    }
}
