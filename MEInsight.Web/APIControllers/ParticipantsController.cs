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

                var query = _context.Participants
                    .Include(p => p.DisabilityTypes)
                    .Include(p => p.Locations)
                    .Include(p => p.Organizations)
                    .Include(p => p.ParticipantCohorts)
                    .Include(p => p.ParticipantTypes)
                    .Include(p => p.Sex)
                    .Select(p => new ParticipantsListViewModel
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
                    });

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
                int recordsTotal = 0;


                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    //OrderBy - requires using System.Linq.Dynamic.Core
                    query = query.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                //total number of rows counts
                recordsTotal = query.Count();

                //Paging (-1 == All rows)
                if (pageSize != -1)
                {
                    query = query.Skip(skip).Take(pageSize);
                }

                ////Search
                if (!string.IsNullOrEmpty(searchValue))
                {

                    // TODO: update queries
                    //query = query
                    //    .Where(x => searchValue.ToLower().Contains(x.FirstName.ToLower()));
                    //.Select(x=> x.).Where(m =>
                    //               m.FirstName.ToLower().Contains(searchValue.ToLower())
                    //               || m.MiddleName.ToLower().Contains(searchValue.ToLower())
                    //               || m.LastName.ToLower().Contains(searchValue.ToLower())
                    //               || m.OrganizationName.ToLower().Contains(searchValue.ToLower())
                    //               ).ToListAsync();

                }

                //Return JSON Data 
                var jsonData = new
                {
                    draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal,
                    data = await query.ToListAsync()
                };

                //return Ok(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
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
