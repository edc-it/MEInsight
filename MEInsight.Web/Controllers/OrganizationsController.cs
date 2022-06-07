using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MEInsight.Entities.Core;
using MEInsight.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MEInsight.Entities.Identity;
using MEInsight.Web.Extensions;

namespace MEInsight.Web.Controllers
{
    ////[Authorize]
    public class OrganizationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrganizationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Organizations
        public async Task<IActionResult> Index(Guid? id)
        {

            ViewData["NextController"] = "Groups";
            ViewData["ParentController"] = "Organizations";
            ViewData["ParentId"] = id;

            //Get logged USER Organization
            Guid? userOrganizationId = (await _userManager.GetUserAsync(HttpContext.User))?.OrganizationId;

            if (userOrganizationId == null)
            {
                return NotFound();
            }

            //Get Logged User Organization
            var organization = await _context.Organizations
                    .SingleOrDefaultAsync(o => o.OrganizationId == userOrganizationId);

            if (organization == null)
            {
                return NotFound();
            }

            //If USER Organization is not an Organization Unit redirect to Groups Controller
            if (organization.IsOrganizationUnit != true)
            {
                return RedirectToAction(nameof(Index), "Groups", new { id = organization.OrganizationId });
            }
            else
            {
                //If id parameter is null, return USER Organization
                if (id == null)
                {
                    var applicationDbContext = _context.Organizations
                        .Include(o => o.Locations)
                        .Include(o => o.OrganizationTypes)
                        .Include(o => o.ParentOrganizations)
                        .Where(o => o.ParentOrganizationId == organization.OrganizationId);

                    ViewData["ParentId"] = organization.OrganizationId;

                    return View(await applicationDbContext.OrderBy(o => o.OrganizationName).ToListAsync());

                }
                // return selected id Organization
                else
                {
                    //Get Organizations that are Organization Units
                    var OUOrganizations = _context.Organizations
                        .Where(x => x.IsOrganizationUnit == true);

                    //Get List of Parents
                    var parents = EnumerableExtensions.ListParents(OUOrganizations, id).ToList();

                    if (!parents.Contains(organization))
                    {
                        //Parent
                        return NotFound();
                    }


                    //Return selected Organization ID Child organizations
                    var applicationDbContext = _context.Organizations
                        .Include(o => o.Locations).ThenInclude(o => o.ParentLocations)
                        .Include(o => o.OrganizationTypes)
                        .Include(o => o.ParentOrganizations)
                        .Where(o => o.ParentOrganizationId == id);

                    return View(await applicationDbContext.OrderBy(o => o.OrganizationName).ToListAsync());

                }
            }
        }

        // GET: Organizations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations
                .Include(o => o.Locations).ThenInclude(o => o.ParentLocations)
                .Include(o => o.OrganizationTypes)
                .Include(o => o.ParentOrganizations)
                .FirstOrDefaultAsync(m => m.OrganizationId == id);

            if (organization == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = organization.ParentOrganizationId;

            return View(organization);
        }

        // GET: Organizations/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
            return View();
        }

        // POST: Organizations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("OrganizationId,RegistrationDate,OrganizationCode,OrganizationName,RefOrganizationTypeId,ParentOrganizationId,Contact,Phone,RefLocationId,Address,Latitude,Longitude,IsOrganizationUnit")] Organization organization,
            string[] RefLocationId
            )
        {
            if (ModelState.IsValid)
            {

                if (RefLocationId.Length > 0)
                {
                    // Gets the last non-null item in RefLocationId array,
                    // the array might have null values for locations left empty
                    // and this assigns the last non-null location to RefLocationId
                    var location =
                        (from r in RefLocationId where !string.IsNullOrEmpty(r) select r)
                        .OrderByDescending(r => r).Count();

                    organization.RefLocationId = RefLocationId[location - 1];
                }

                organization.OrganizationId = Guid.NewGuid();
                _context.Add(organization);

                //TODO: 1 Create a separate view model for schools
                if (organization.RefOrganizationTypeId == 20)
                {
                    School school = new School
                    {
                        OrganizationId = organization.OrganizationId,
                    };
                    _context.Add(school);
                }

                await _context.SaveChangesAsync();

                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";

                //TODO: 2 Create a separate view model for schools
                if (organization.RefOrganizationTypeId == 20)
                {
                    return RedirectToAction(nameof(Edit), "Schools", new { id = organization.OrganizationId });
                }

                return RedirectToAction(nameof(Index), new { id = organization.ParentOrganizationId });
            }

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
            ViewData["ParentOrganizationId"] = new SelectList(_context.Organizations.Where(x => x.IsOrganizationUnit == true), "OrganizationId", "OrganizationName", organization.ParentOrganizationId);

            return View(organization);
        }

        // GET: Organizations/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations
                .Include(m => m.OrganizationTypes)
                .SingleOrDefaultAsync(m => m.OrganizationId == id);

            if (organization == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = organization.ParentOrganizationId;

            var locationTypes = _context.LocationTypes;
            ViewData["RefLocationTypes"] = locationTypes;
            ViewData["RefLocationTypesCount"] = locationTypes.Count();

            ViewData["RefLocationId"] = new SelectList(_context
                .Locations
                .Where(x =>
                    x.LocationTypes.LocationLevel == 1 &&
                    x.ParentLocationId == null)
                .Select(x => new
                {
                    x.RefLocationId,
                    x.LocationName
                }), "RefLocationId", "LocationName");

            //Get Location Parents (only the lower level RefLocationId is saved, 
            //this gets the location parents for the saved location
            var allLocations = _context.Locations;
            var locations = EnumerableExtensions.ListLocations(allLocations, organization.RefLocationId);
            ViewData["RefLocationParents"] = locations.OrderBy(x => x.RefLocationTypeId);

            ViewData["ParentOrganizationId"] = new SelectList(_context.Organizations.Where(x => x.IsOrganizationUnit == true), "OrganizationId", "OrganizationName", organization.ParentOrganizationId);
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType", organization.RefOrganizationTypeId);

            return View(organization);
        }

        // POST: Organizations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrganizationId,RegistrationDate,OrganizationCode,OrganizationName,RefOrganizationTypeId,ParentOrganizationId,Contact,Phone,RefLocationId,Address,Latitude,Longitude,IsOrganizationUnit,IsTenant")] Organization organization, string[] RefLocationId)
        {
            if (id != organization.OrganizationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    // Gets the last non-null item in RefLocationId array,
                    // the array might have null values for locations left empty
                    // and this assigns the last non-null location to RefLocationId
                    if (RefLocationId.Length != 0)
                    {
                        var location =
                        (from r in RefLocationId where !string.IsNullOrEmpty(r) select r)
                        .OrderByDescending(r => r).Count();

                        organization.RefLocationId = RefLocationId[location - 1];
                    }

                    _context.Update(organization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationExists(organization.OrganizationId))
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

                return RedirectToAction(nameof(Index), new { id = organization.ParentOrganizationId });
            }

            ViewData["ParentId"] = organization.ParentOrganizationId;

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

            //parents for location h
            var allLocations = _context.Locations;
            var locations = EnumerableExtensions.ListLocations(allLocations, organization.RefLocationId);
            ViewData["RefLocationParents"] = locations.OrderBy(x => x.RefLocationTypeId);

            ViewData["ParentOrganizationId"] = new SelectList(_context.Organizations.Where(x => x.IsOrganizationUnit == true), "OrganizationId", "OrganizationName", organization.ParentOrganizationId);
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType", organization.RefOrganizationTypeId);

            return View(organization);
        }

        // GET: Organizations/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations
                .Include(o => o.Organizations)
                .Include(o => o.OrganizationTypes)
                .Include(o => o.ParentOrganizations)
                ////.Include(o => o.Schools)
                .Include(o => o.Locations)
                .Include(o => o.Participants)
                .Include(o => o.Groups)
                .Include(o => o.TLMDistributionsFrom)
                .Include(o => o.TLMDistributionsTo)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrganizationId == id);

            if (organization == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
            relatedCount += organization.Participants.Count();
            ////relatedCount += organization.Schools.Count();
            relatedCount += organization.Organizations.Count();
            relatedCount += organization.Groups.Count();
            relatedCount += organization.TLMDistributionsFrom.Count();
            relatedCount += organization.TLMDistributionsTo.Count();
            relatedCount += organization.Users.Count();

            if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            ViewData["ParentId"] = organization.ParentOrganizationId;

            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //TODO: Remove 1:1 School Record
            var organization = await _context.Organizations.FindAsync(id);

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();

            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            //return RedirectToAction(nameof(Index));        
            return RedirectToAction(nameof(Index), new { id = organization.ParentOrganizationId });
        }

        private bool OrganizationExists(Guid id)
        {
            return _context.Organizations.Any(e => e.OrganizationId == id);
        }
    }
}
