using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MEInsight.Entities.Core;
using MEInsight.Web.Data;

namespace MEInsight.Web.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schools
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Schools.Include(s => s.Locations).Include(s => s.OrganizationTypes).Include(s => s.ParentOrganizations).Include(s => s.SchoolAdministrationTypes).Include(s => s.SchoolClusters).Include(s => s.SchoolLanguages).Include(s => s.SchoolLocations).Include(s => s.SchoolStatus).Include(s => s.SchoolTypes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Schools/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Schools == null)
            {
                return NotFound();
            }

            var school = await _context.Schools
                .Include(s => s.Locations)
                .Include(s => s.OrganizationTypes)
                .Include(s => s.ParentOrganizations)
                .Include(s => s.SchoolAdministrationTypes)
                .Include(s => s.SchoolClusters)
                .Include(s => s.SchoolLanguages)
                .Include(s => s.SchoolLocations)
                .Include(s => s.SchoolStatus)
                .Include(s => s.SchoolTypes)
                .FirstOrDefaultAsync(m => m.OrganizationId == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // GET: Schools/Create
        public IActionResult Create(Guid? id)
        {

            //if (id == null)
            //{
            //    return NotFound();
            //}

            ViewData["ParentId"] = id;

            var locationTypes = _context.LocationTypes;
            ViewData["RefLocationTypes"] = locationTypes;
            ViewData["RefLocationTypesCount"] = locationTypes.Count();

            ViewData["RefLocationId"] = new SelectList(_context
                .Locations
                .Include(x => x.LocationTypes)
                .Where(x =>
                    x.LocationTypes.LocationLevel == 1 &&
                    x.ParentLocationId == null)
                .Select(x => new
                {
                    x.RefLocationId,
                    x.LocationName
                }), "RefLocationId", "LocationName");


            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType");
            ViewData["ParentOrganizationId"] = new SelectList(_context.Organizations.Where(x => x.IsOrganizationUnit == true), "OrganizationId", "OrganizationName", id);
            
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId");
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType");
            ViewData["ParentOrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode");
            ViewData["RefSchoolAdministrationTypeId"] = new SelectList(_context.SchoolAdministrationTypes, "RefSchoolAdministrationTypeId", "SchoolAdministrationType");
            ViewData["RefSchoolClusterId"] = new SelectList(_context.SchoolClusters, "RefSchoolClusterId", "SchoolCluster");
            ViewData["RefSchoolLanguageId"] = new SelectList(_context.SchoolLanguages, "RefSchoolLanguageId", "SchoolLanguage");
            ViewData["RefSchoolLocationId"] = new SelectList(_context.SchoolLocations, "RefSchoolLocationId", "SchoolLocation");
            ViewData["RefSchoolStatusId"] = new SelectList(_context.SchoolStatus, "RefSchoolStatusId", "SchoolStatus");
            ViewData["RefSchoolTypeId"] = new SelectList(_context.SchoolTypes, "RefSchoolTypeId", "SchoolType");
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolCode,RefSchoolTypeId,RefSchoolLocationId,RefSchoolLanguageId,RefSchoolAdministrationTypeId,PartnerId,IsClusterCenter,RefSchoolClusterId,RefSchoolStatusId,HeadTeacher,OrganizationId,RegistrationDate,OrganizationCode,OrganizationName,RefOrganizationTypeId,ParentOrganizationId,Contact,Phone,RefLocationId,Address,Latitude,Longitude,IsOrganizationUnit")] School school)
        {
            if (ModelState.IsValid)
            {
                school.OrganizationId = Guid.NewGuid();
                _context.Add(school);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", school.RefLocationId);
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType", school.RefOrganizationTypeId);
            ViewData["ParentOrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", school.ParentOrganizationId);
            ViewData["RefSchoolAdministrationTypeId"] = new SelectList(_context.SchoolAdministrationTypes, "RefSchoolAdministrationTypeId", "SchoolAdministrationType", school.RefSchoolAdministrationTypeId);
            ViewData["RefSchoolClusterId"] = new SelectList(_context.SchoolClusters, "RefSchoolClusterId", "SchoolCluster", school.RefSchoolClusterId);
            ViewData["RefSchoolLanguageId"] = new SelectList(_context.SchoolLanguages, "RefSchoolLanguageId", "SchoolLanguage", school.RefSchoolLanguageId);
            ViewData["RefSchoolLocationId"] = new SelectList(_context.SchoolLocations, "RefSchoolLocationId", "SchoolLocation", school.RefSchoolLocationId);
            ViewData["RefSchoolStatusId"] = new SelectList(_context.SchoolStatus, "RefSchoolStatusId", "SchoolStatus", school.RefSchoolStatusId);
            ViewData["RefSchoolTypeId"] = new SelectList(_context.SchoolTypes, "RefSchoolTypeId", "SchoolType", school.RefSchoolTypeId);
            return View(school);
        }

        // GET: Schools/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Schools == null)
            {
                return NotFound();
            }

            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", school.RefLocationId);
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType", school.RefOrganizationTypeId);
            ViewData["ParentOrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", school.ParentOrganizationId);
            ViewData["RefSchoolAdministrationTypeId"] = new SelectList(_context.SchoolAdministrationTypes, "RefSchoolAdministrationTypeId", "SchoolAdministrationType", school.RefSchoolAdministrationTypeId);
            ViewData["RefSchoolClusterId"] = new SelectList(_context.SchoolClusters, "RefSchoolClusterId", "SchoolCluster", school.RefSchoolClusterId);
            ViewData["RefSchoolLanguageId"] = new SelectList(_context.SchoolLanguages, "RefSchoolLanguageId", "SchoolLanguage", school.RefSchoolLanguageId);
            ViewData["RefSchoolLocationId"] = new SelectList(_context.SchoolLocations, "RefSchoolLocationId", "SchoolLocation", school.RefSchoolLocationId);
            ViewData["RefSchoolStatusId"] = new SelectList(_context.SchoolStatus, "RefSchoolStatusId", "SchoolStatus", school.RefSchoolStatusId);
            ViewData["RefSchoolTypeId"] = new SelectList(_context.SchoolTypes, "RefSchoolTypeId", "SchoolType", school.RefSchoolTypeId);
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SchoolCode,RefSchoolTypeId,RefSchoolLocationId,RefSchoolLanguageId,RefSchoolAdministrationTypeId,PartnerId,IsClusterCenter,RefSchoolClusterId,RefSchoolStatusId,HeadTeacher,OrganizationId,RegistrationDate,OrganizationCode,OrganizationName,RefOrganizationTypeId,ParentOrganizationId,Contact,Phone,RefLocationId,Address,Latitude,Longitude,IsOrganizationUnit")] School school)
        {
            if (id != school.OrganizationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(school);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(school.OrganizationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RefLocationId"] = new SelectList(_context.Locations, "RefLocationId", "RefLocationId", school.RefLocationId);
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType", school.RefOrganizationTypeId);
            ViewData["ParentOrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", school.ParentOrganizationId);
            ViewData["RefSchoolAdministrationTypeId"] = new SelectList(_context.SchoolAdministrationTypes, "RefSchoolAdministrationTypeId", "SchoolAdministrationType", school.RefSchoolAdministrationTypeId);
            ViewData["RefSchoolClusterId"] = new SelectList(_context.SchoolClusters, "RefSchoolClusterId", "SchoolCluster", school.RefSchoolClusterId);
            ViewData["RefSchoolLanguageId"] = new SelectList(_context.SchoolLanguages, "RefSchoolLanguageId", "SchoolLanguage", school.RefSchoolLanguageId);
            ViewData["RefSchoolLocationId"] = new SelectList(_context.SchoolLocations, "RefSchoolLocationId", "SchoolLocation", school.RefSchoolLocationId);
            ViewData["RefSchoolStatusId"] = new SelectList(_context.SchoolStatus, "RefSchoolStatusId", "SchoolStatus", school.RefSchoolStatusId);
            ViewData["RefSchoolTypeId"] = new SelectList(_context.SchoolTypes, "RefSchoolTypeId", "SchoolType", school.RefSchoolTypeId);
            return View(school);
        }

        // GET: Schools/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Schools == null)
            {
                return NotFound();
            }

            var school = await _context.Schools
                .Include(s => s.Locations)
                .Include(s => s.OrganizationTypes)
                .Include(s => s.ParentOrganizations)
                .Include(s => s.SchoolAdministrationTypes)
                .Include(s => s.SchoolClusters)
                .Include(s => s.SchoolLanguages)
                .Include(s => s.SchoolLocations)
                .Include(s => s.SchoolStatus)
                .Include(s => s.SchoolTypes)
                .FirstOrDefaultAsync(m => m.OrganizationId == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Schools == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Schools'  is null.");
            }
            var school = await _context.Schools.FindAsync(id);
            if (school != null)
            {
                _context.Schools.Remove(school);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolExists(Guid id)
        {
          return (_context.Schools?.Any(e => e.OrganizationId == id)).GetValueOrDefault();
        }
    }
}
