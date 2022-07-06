using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MEInsight.Entities.Core;
using MEInsight.Web.Data;
using MEInsight.Web.Models;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Identity;
using MEInsight.Entities.Identity;
using MEInsight.Web.Extensions;

namespace MEInsight.Web.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ParticipantsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Participants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipants()
        {
            if (_context.Participants == null)
            {
                return NotFound();
            }
            return await _context.Participants.ToListAsync();
        }

        // GET: api/Participants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(Guid id)
        {
            if (_context.Participants == null)
            {
                return NotFound();
            }
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            return participant;
        }

        // PUT: api/Participants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant(Guid id, Participant participant)
        {
            if (id != participant.ParticipantId)
            {
                return BadRequest();
            }

            _context.Entry(participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Participants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipant(Participant participant)
        {
            if (_context.Participants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Participants'  is null.");
            }
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipant", new { id = participant.ParticipantId }, participant);
        }

        // DELETE: api/Participants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(Guid id)
        {
            if (_context.Participants == null)
            {
                return NotFound();
            }
            var participant = await _context.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Paginated")]
        // POST: api/Participants/Paginated
        // Used by datatables.net
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipantsPaginated()
        {

            if (_context.Participants == null)
            {
                return NotFound();
            }

            try
            {
                // Get Logged User OrganizationId
                Guid? userOrganizacionId = (await _userManager.GetUserAsync(HttpContext.User))?.OrganizationId;

                if (userOrganizacionId == null)
                {
                    return NotFound();
                }

                // Get Logged User Organization
                var organization = await _context.Organizations
                        .SingleOrDefaultAsync(o => o.OrganizationId == userOrganizacionId);

                if (organization == null)
                {
                    return NotFound();
                }
                
                var query = _context.Participants.AsQueryable();

                // Filtered by one OrganizationId
                string? id = Request.Form["id"].FirstOrDefault();

                if (id != null)
                {
                    Guid organizationId = new(id!);
                    query = query
                        .Where(x => x.OrganizationId == organizationId);
                }
                else
                {
                    if (organization.IsOrganizationUnit != true)
                    {
                        query = query
                            .Where(p => p.OrganizationId == organization.OrganizationId);
                    }
                    else
                    {
                        //Get Organization Hierarchy
                        var AllOrganizations = await _context.Organizations
                        .Select(x => new
                        {
                            x.OrganizationId,
                            x.OrganizationName,
                            x.RefOrganizationTypeId,
                            x.ParentOrganizationId,
                            x.IsOrganizationUnit
                        }).ToListAsync();

                        var lookup = AllOrganizations.ToLookup(x => x.ParentOrganizationId);
                        var childOrganizations = lookup[organization.OrganizationId].SelectRecursive(x => lookup[x.OrganizationId]).ToList();

                        var organizationIds = childOrganizations
                                .Select(x => x.OrganizationId)
                                    .ToList();
                        organizationIds.Add(organization.OrganizationId);

                        query = query
                            .Where(p => organizationIds.Contains((Guid)p.OrganizationId!));
                    }

                }

                // Datatables.net server-side POST request
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20 
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                string? searchValue = Request.Form["search[value]"].FirstOrDefault();
                //Paging Size (10, 20, 50,100) 
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    //OrderBy - requires using System.Linq.Dynamic.Core
                    query = query.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                //Paging (-1 == All rows)
                if (pageSize != -1)
                {
                    query = query.Skip(skip).Take(pageSize);
                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    query = query
                        .Where(x =>
                            x.FirstName!.ToLower().Contains(searchValue.ToLower()) ||
                            x.MiddleName!.ToLower().Contains(searchValue.ToLower()) ||
                            x.LastName!.ToLower().Contains(searchValue.ToLower()) ||
                            x.ParticipantCode!.ToLower().Contains(searchValue.ToLower()) ||
                            x.Organizations!.OrganizationName!.ToLower().Contains(searchValue.ToLower())
                            );
                }

                var data = await query
                    .Include(p => p.Locations)
                    .Include(p => p.Organizations)
                    .Include(p => p.ParticipantTypes)
                    .Include(p => p.Sex)
                    .Select(p => new ParticipantsListViewModel()
                    {
                        Name = p.Name,
                        ParticipantCode = p.ParticipantCode,
                        ParticipantType = p.ParticipantTypes!.ParticipantType,
                        Position = "",
                        Sex = p.Sex!.Sex,
                        OrganizationName = p.Organizations!.OrganizationName,
                        RegistrationDate = p.RegistrationDate.ToShortDateString(),
                        Location = p.Locations!.LocationName,
                        ParticipantId = p.ParticipantId
                    })
                    .ToListAsync();
                
                int total = query.Count();

                //Return JSON Data 
                var jsonData = new
                {
                    draw,
                    recordsFiltered = total,
                    recordsTotal = total,
                    data
                };

                return Ok(jsonData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ParticipantExists(Guid id)
        {
            return (_context.Participants?.Any(e => e.ParticipantId == id)).GetValueOrDefault();
        }
    }
}
