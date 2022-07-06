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
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeachersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(Guid? id)
        {
            
            ViewData["NextController"] = "";
            ViewData["ParentController"] = "";
            ViewData["ParentId"] = id;
            
            var applicationDbContext = _context.Teachers.Include(t => t.DisabilityTypes).Include(t => t.Locations).Include(t => t.Organizations).Include(t => t.ParticipantCohorts).Include(t => t.ParticipantTypes).Include(t => t.Sex).Include(t => t.TeacherEmploymentTypes).Include(t => t.TeacherPositions).Include(t => t.TeacherTypes)
                    .Where(t => t.OrganizationId == id);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.DisabilityTypes)
                .Include(t => t.Locations)
                .Include(t => t.Organizations)
                .Include(t => t.ParticipantCohorts)
                .Include(t => t.ParticipantTypes)
                .Include(t => t.Sex)
                .Include(t => t.TeacherEmploymentTypes)
                .Include(t => t.TeacherPositions)
                .Include(t => t.TeacherTypes)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = teacher.OrganizationId;

            return View(teacher);
        }

        // GET: Teachers/Create
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
            ViewData["RefTeacherEmploymentTypeId"] = new SelectList(_context.TeacherEmploymentTypes, "RefTeacherEmploymentTypeId", "TeacherEmploymentType");
            ViewData["RefTeacherPositionId"] = new SelectList(_context.TeacherPositions, "RefTeacherPositionId", "TeacherPosition");
            ViewData["RefTeacherTypeId"] = new SelectList(_context.TeacherTypes, "RefTeacherTypeId", "TeacherType");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefTeacherTypeId,RefTeacherPositionId,RefTeacherEmploymentTypeId,GradeLevels,ParticipantId,RegistrationDate,OrganizationId,RefParticipantTypeId,RefParticipantCohortId,ParticipantCode,FirstName,MiddleName,LastName,RefSexId,BirthDate,Age,Disability,RefDisabilityTypeId,Phone,Mobile,Email,Facebook,InstantMessenger,RefLocationId,Address,AdditionalData")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.ParticipantId = Guid.NewGuid();
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), new { id = teacher.OrganizationId });
            }
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", teacher.RefDisabilityTypeId);
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", teacher.RefLocationId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", teacher.OrganizationId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", teacher.RefParticipantCohortId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", teacher.RefParticipantTypeId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", teacher.RefSexId);
            ViewData["RefTeacherEmploymentTypeId"] = new SelectList(_context.TeacherEmploymentTypes, "RefTeacherEmploymentTypeId", "TeacherEmploymentType", teacher.RefTeacherEmploymentTypeId);
            ViewData["RefTeacherPositionId"] = new SelectList(_context.TeacherPositions, "RefTeacherPositionId", "TeacherPosition", teacher.RefTeacherPositionId);
            ViewData["RefTeacherTypeId"] = new SelectList(_context.TeacherTypes, "RefTeacherTypeId", "TeacherType", teacher.RefTeacherTypeId);
            ViewData["ParentId"] = teacher.OrganizationId;
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = teacher.OrganizationId;
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", teacher.RefDisabilityTypeId);
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", teacher.RefLocationId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", teacher.OrganizationId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", teacher.RefParticipantCohortId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", teacher.RefParticipantTypeId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", teacher.RefSexId);
            ViewData["RefTeacherEmploymentTypeId"] = new SelectList(_context.TeacherEmploymentTypes, "RefTeacherEmploymentTypeId", "TeacherEmploymentType", teacher.RefTeacherEmploymentTypeId);
            ViewData["RefTeacherPositionId"] = new SelectList(_context.TeacherPositions, "RefTeacherPositionId", "TeacherPosition", teacher.RefTeacherPositionId);
            ViewData["RefTeacherTypeId"] = new SelectList(_context.TeacherTypes, "RefTeacherTypeId", "TeacherType", teacher.RefTeacherTypeId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RefTeacherTypeId,RefTeacherPositionId,RefTeacherEmploymentTypeId,GradeLevels,ParticipantId,RegistrationDate,OrganizationId,RefParticipantTypeId,RefParticipantCohortId,ParticipantCode,FirstName,MiddleName,LastName,RefSexId,BirthDate,Age,Disability,RefDisabilityTypeId,Phone,Mobile,Email,Facebook,InstantMessenger,RefLocationId,Address,AdditionalData")] Teacher teacher)
        {
            if (id != teacher.ParticipantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.ParticipantId))
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
                return RedirectToAction(nameof(Index), new { id = teacher.OrganizationId });
            }
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", teacher.RefDisabilityTypeId);
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", teacher.RefLocationId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", teacher.OrganizationId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", teacher.RefParticipantCohortId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", teacher.RefParticipantTypeId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", teacher.RefSexId);
            ViewData["RefTeacherEmploymentTypeId"] = new SelectList(_context.TeacherEmploymentTypes, "RefTeacherEmploymentTypeId", "TeacherEmploymentType", teacher.RefTeacherEmploymentTypeId);
            ViewData["RefTeacherPositionId"] = new SelectList(_context.TeacherPositions, "RefTeacherPositionId", "TeacherPosition", teacher.RefTeacherPositionId);
            ViewData["RefTeacherTypeId"] = new SelectList(_context.TeacherTypes, "RefTeacherTypeId", "TeacherType", teacher.RefTeacherTypeId);
            ViewData["ParentId"] = teacher.OrganizationId;
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.DisabilityTypes)
                .Include(t => t.Locations)
                .Include(t => t.Organizations)
                .Include(t => t.ParticipantCohorts)
                .Include(t => t.ParticipantTypes)
                .Include(t => t.Sex)
                .Include(t => t.TeacherEmploymentTypes)
                .Include(t => t.TeacherPositions)
                .Include(t => t.TeacherTypes)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);
            if (teacher == null)
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
            ViewData["ParentId"] = teacher.OrganizationId;

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Teachers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Teachers'  is null.");
            }
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }
            
            await _context.SaveChangesAsync();

            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            //return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index), new { id = teacher.OrganizationId });
        }

        private bool TeacherExists(Guid id)
        {
          return (_context.Teachers?.Any(e => e.ParticipantId == id)).GetValueOrDefault();
        }
    }
}
