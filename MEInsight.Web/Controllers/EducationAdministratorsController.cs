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
    public class EducationAdministratorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EducationAdministratorsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EducationAdministrators
        public async Task<IActionResult> Index(Guid? id)
        {
            
            ViewData["NextController"] = "";
            ViewData["ParentController"] = "";
            ViewData["ParentId"] = id;
            
            var applicationDbContext = _context.EducationAdministrators.Include(e => e.DisabilityTypes).Include(e => e.Locations).Include(e => e.Organizations).Include(e => e.ParticipantCohorts).Include(e => e.ParticipantTypes).Include(e => e.Sex).Include(e => e.EducationAdministratorOffices).Include(e => e.EducationAdministratorPositions).Include(e => e.EducationAdministratorTypes)
                    .Where(e => e.OrganizationId == id);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EducationAdministrators/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.EducationAdministrators == null)
            {
                return NotFound();
            }

            var educationAdministrator = await _context.EducationAdministrators
                .Include(e => e.DisabilityTypes)
                .Include(e => e.Locations)
                .Include(e => e.Organizations)
                .Include(e => e.ParticipantCohorts)
                .Include(e => e.ParticipantTypes)
                .Include(e => e.Sex)
                .Include(e => e.EducationAdministratorOffices)
                .Include(e => e.EducationAdministratorPositions)
                .Include(e => e.EducationAdministratorTypes)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);

            if (educationAdministrator == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = educationAdministrator.OrganizationId;

            return View(educationAdministrator);
        }

        // GET: EducationAdministrators/Create
        [Authorize(Policy = "RequireCreateRole")]
        public async Task<IActionResult> Create(Guid? id)
        {

            //if (id == null)
            //{
            //    return NotFound();
            //}

            // *** For JSTree ***
            //Logged User OrganizationId
            Guid? userOrganizacionId = (await _userManager.GetUserAsync(HttpContext.User))?.OrganizationId;

            if (userOrganizacionId == null)
            {
                return NotFound();
            }

            //Get Logged User Organization
            var organization = await _context.Organizations
                    .SingleOrDefaultAsync(o => o.OrganizationId == userOrganizacionId);

            if (organization == null)
            {
                return NotFound();
            }

            //Returns the top hiearchy organization for JSTree
            ViewData["ParentOrganizationId"] = organization.OrganizationId;

            // *** Select Lists ***
            // RefLocation
            var locationTypes = _context.LocationTypes;
            ViewData["RefLocationTypes"] = locationTypes;
            ViewData["RefLocationTypesCount"] = locationTypes.Count();

            ViewData["RefLocationId"] = new SelectList(_context.Locations
                .Include(x => x.LocationTypes)
                .Where(x =>
                    x.LocationTypes.LocationLevel == 1 &&
                    x.ParentLocationId == null)
                .Select(x => new
                {
                    x.RefLocationId,
                    x.LocationName
                }), "RefLocationId", "LocationName");

            ViewData["ParentId"] = id;
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType");
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId");
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode");
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort");
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType");
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex");
            ViewData["RefEducationAdministratorOfficeId"] = new SelectList(_context.EducationAdministratorOffices, "RefEducationAdministratorOfficeId", "EducationAdministratorOffice");
            ViewData["RefEducationAdministratorPositionId"] = new SelectList(_context.EducationAdministratorPositions, "RefEducationAdministratorPositionId", "EducationAdministratorPosition");
            ViewData["RefEducationAdministratorTypeId"] = new SelectList(_context.EducationAdministratorTypes, "RefEducationAdministratorTypeId", "EducationAdministratorType");
            return View();
        }

        // POST: EducationAdministrators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefEducationAdministratorTypeId,RefEducationAdministratorPositionId,RefEducationAdministratorOfficeId,ParticipantId,RegistrationDate,OrganizationId,RefParticipantTypeId,RefParticipantCohortId,ParticipantCode,FirstName,MiddleName,LastName,RefSexId,BirthDate,Age,Disability,RefDisabilityTypeId,Phone,Mobile,Email,Facebook,InstantMessenger,RefLocationId,Address,AdditionalData")] EducationAdministrator educationAdministrator)
        {
            if (ModelState.IsValid)
            {
                educationAdministrator.ParticipantId = Guid.NewGuid();
                _context.Add(educationAdministrator);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), new { id = educationAdministrator.OrganizationId });
            }
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", educationAdministrator.RefDisabilityTypeId);
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", educationAdministrator.RefLocationId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", educationAdministrator.OrganizationId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", educationAdministrator.RefParticipantCohortId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", educationAdministrator.RefParticipantTypeId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", educationAdministrator.RefSexId);
            ViewData["RefEducationAdministratorOfficeId"] = new SelectList(_context.EducationAdministratorOffices, "RefEducationAdministratorOfficeId", "EducationAdministratorOffice", educationAdministrator.RefEducationAdministratorOfficeId);
            ViewData["RefEducationAdministratorPositionId"] = new SelectList(_context.EducationAdministratorPositions, "RefEducationAdministratorPositionId", "EducationAdministratorPosition", educationAdministrator.RefEducationAdministratorPositionId);
            ViewData["RefEducationAdministratorTypeId"] = new SelectList(_context.EducationAdministratorTypes, "RefEducationAdministratorTypeId", "EducationAdministratorType", educationAdministrator.RefEducationAdministratorTypeId);
            ViewData["ParentId"] = educationAdministrator.OrganizationId;
            return View(educationAdministrator);
        }

        // GET: EducationAdministrators/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.EducationAdministrators == null)
            {
                return NotFound();
            }

            var educationAdministrator = await _context.EducationAdministrators.FindAsync(id);
            if (educationAdministrator == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = educationAdministrator.OrganizationId;
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", educationAdministrator.RefDisabilityTypeId);
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", educationAdministrator.RefLocationId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", educationAdministrator.OrganizationId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", educationAdministrator.RefParticipantCohortId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", educationAdministrator.RefParticipantTypeId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", educationAdministrator.RefSexId);
            ViewData["RefEducationAdministratorOfficeId"] = new SelectList(_context.EducationAdministratorOffices, "RefEducationAdministratorOfficeId", "EducationAdministratorOffice", educationAdministrator.RefEducationAdministratorOfficeId);
            ViewData["RefEducationAdministratorPositionId"] = new SelectList(_context.EducationAdministratorPositions, "RefEducationAdministratorPositionId", "EducationAdministratorPosition", educationAdministrator.RefEducationAdministratorPositionId);
            ViewData["RefEducationAdministratorTypeId"] = new SelectList(_context.EducationAdministratorTypes, "RefEducationAdministratorTypeId", "EducationAdministratorType", educationAdministrator.RefEducationAdministratorTypeId);
            return View(educationAdministrator);
        }

        // POST: EducationAdministrators/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RefEducationAdministratorTypeId,RefEducationAdministratorPositionId,RefEducationAdministratorOfficeId,ParticipantId,RegistrationDate,OrganizationId,RefParticipantTypeId,RefParticipantCohortId,ParticipantCode,FirstName,MiddleName,LastName,RefSexId,BirthDate,Age,Disability,RefDisabilityTypeId,Phone,Mobile,Email,Facebook,InstantMessenger,RefLocationId,Address,AdditionalData")] EducationAdministrator educationAdministrator)
        {
            if (id != educationAdministrator.ParticipantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationAdministrator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationAdministratorExists(educationAdministrator.ParticipantId))
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
                return RedirectToAction(nameof(Index), new { id = educationAdministrator.OrganizationId });
            }
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", educationAdministrator.RefDisabilityTypeId);
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", educationAdministrator.RefLocationId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", educationAdministrator.OrganizationId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", educationAdministrator.RefParticipantCohortId);
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType", educationAdministrator.RefParticipantTypeId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", educationAdministrator.RefSexId);
            ViewData["RefEducationAdministratorOfficeId"] = new SelectList(_context.EducationAdministratorOffices, "RefEducationAdministratorOfficeId", "EducationAdministratorOffice", educationAdministrator.RefEducationAdministratorOfficeId);
            ViewData["RefEducationAdministratorPositionId"] = new SelectList(_context.EducationAdministratorPositions, "RefEducationAdministratorPositionId", "EducationAdministratorPosition", educationAdministrator.RefEducationAdministratorPositionId);
            ViewData["RefEducationAdministratorTypeId"] = new SelectList(_context.EducationAdministratorTypes, "RefEducationAdministratorTypeId", "EducationAdministratorType", educationAdministrator.RefEducationAdministratorTypeId);
            ViewData["ParentId"] = educationAdministrator.OrganizationId;
            return View(educationAdministrator);
        }

        // GET: EducationAdministrators/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.EducationAdministrators == null)
            {
                return NotFound();
            }

            var educationAdministrator = await _context.EducationAdministrators
                .Include(e => e.DisabilityTypes)
                .Include(e => e.Locations)
                .Include(e => e.Organizations)
                .Include(e => e.ParticipantCohorts)
                .Include(e => e.ParticipantTypes)
                .Include(e => e.Sex)
                .Include(e => e.EducationAdministratorOffices)
                .Include(e => e.EducationAdministratorPositions)
                .Include(e => e.EducationAdministratorTypes)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);
            if (educationAdministrator == null)
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
            ViewData["ParentId"] = educationAdministrator.OrganizationId;

            return View(educationAdministrator);
        }

        // POST: EducationAdministrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.EducationAdministrators == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EducationAdministrators'  is null.");
            }
            var educationAdministrator = await _context.EducationAdministrators.FindAsync(id);
            if (educationAdministrator != null)
            {
                _context.EducationAdministrators.Remove(educationAdministrator);
            }
            
            await _context.SaveChangesAsync();

            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            //return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index), new { id = educationAdministrator.OrganizationId });
        }

        private bool EducationAdministratorExists(Guid id)
        {
          return (_context.EducationAdministrators?.Any(e => e.ParticipantId == id)).GetValueOrDefault();
        }
    }
}
