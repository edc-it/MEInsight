using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MEInsight.Entities.Programs;
using MEInsight.Web.Data;
using MEInsight.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using MEInsight.Web.Models;
using System.Linq.Dynamic.Core;

namespace MEInsight.Web.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupEnrollmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupEnrollmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/GroupEnrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupEnrollment>>> GetGroupEnrollments()
        {
          if (_context.GroupEnrollments == null)
          {
              return NotFound();
          }
            return await _context.GroupEnrollments.ToListAsync();
        }

        // GET: api/GroupEnrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupEnrollment>> GetGroupEnrollment(Guid id)
        {
          if (_context.GroupEnrollments == null)
          {
              return NotFound();
          }
            var groupEnrollment = await _context.GroupEnrollments.FindAsync(id);

            if (groupEnrollment == null)
            {
                return NotFound();
            }

            return groupEnrollment;
        }

        // PUT: api/GroupEnrollments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupEnrollment(Guid id, GroupEnrollment groupEnrollment)
        {
            if (id != groupEnrollment.GroupEnrollmentId)
            {
                return BadRequest();
            }

            _context.Entry(groupEnrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupEnrollmentExists(id))
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

        // POST: api/GroupEnrollments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupEnrollment>> PostGroupEnrollment(GroupEnrollment groupEnrollment)
        {
          if (_context.GroupEnrollments == null)
          {
              return Problem("Entity set 'ApplicationDbContext.GroupEnrollments'  is null.");
          }
            _context.GroupEnrollments.Add(groupEnrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupEnrollment", new { id = groupEnrollment.GroupEnrollmentId }, groupEnrollment);
        }

        // DELETE: api/GroupEnrollments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupEnrollment(Guid id)
        {
            if (_context.GroupEnrollments == null)
            {
                return NotFound();
            }
            var groupEnrollment = await _context.GroupEnrollments.FindAsync(id);
            if (groupEnrollment == null)
            {
                return NotFound();
            }

            _context.GroupEnrollments.Remove(groupEnrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Paginated")]
        // POST: api/GroupEnrollments/Paginated
        // Used by datatables.net
        public async Task<ActionResult<IEnumerable<GroupEnrollment>>> GetGroupEnrollmentsPaginated()
        {

            var id = Request.Form["id"].FirstOrDefault();

            if (id == null)
            {
                return NotFound();
            }

            Guid groupId = new(id);

            if (_context.GroupEnrollments == null)
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

                var query = _context.GroupEnrollments
                    .Where(x => x.GroupId == groupId)
                    .Include(x => x.Participants!).ThenInclude(x => x.Sex)
                    .Include(x => x.Participants!.ParticipantTypes)
                    .Include(x => x.Groups!)
                    .ThenInclude(x => x.Programs!)
                    .ThenInclude(x => x.AttendanceUnits!)
                    .Include(p => p.EnrollmentStatus)
                    .Select(x => new GroupEnrollmentsDto
                    {
                        GroupEnrollmentId = x.GroupEnrollmentId,
                        GroupId = x.GroupId,
                        ParticipantId = x.ParticipantId,
                        Name = string.Concat(x.Participants!.LastName, ", ", x.Participants!.FirstName, x.Participants!.MiddleName == null ? "" : " " + x.Participants!.MiddleName).ToUpper(),
                        FirstName = x.Participants!.FirstName,
                        MiddleName = x.Participants!.MiddleName,
                        LastName = x.Participants!.LastName,
                        ParticipantCode = x.Participants!.ParticipantCode,
                        RefSexId = x.Participants!.Sex!.RefSexId,
                        Sex = x.Participants!.Sex!.Sex,
                        Attendance = x.Attendance != null ? x.Attendance.ToString() + " " + x.Groups!.Programs!.AttendanceUnits!.AttendanceUnit : null,
                        RefAttendanceUnitId = x.Groups!.Programs!.RefAttendanceUnitId,
                        AttendanceUnit =  x.Groups!.Programs!.AttendanceUnits!.AttendanceUnit,
                        RefEnrollmentStatusId = x.RefEnrollmentStatusId,
                        EnrollmentStatus = x.EnrollmentStatus!.EnrollmentStatus
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
                    query = query
                        .Where(x =>
                            x.FirstName!.ToLower().Contains(searchValue.ToLower()) ||
                            x.MiddleName!.ToLower().Contains(searchValue.ToLower()) ||
                            x.LastName!.ToLower().Contains(searchValue.ToLower()) ||
                            x.ParticipantCode!.ToLower().Contains(searchValue.ToLower())
                            );
                }

                //Return JSON Data 
                var jsonData = new
                {
                    draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal,
                    data = await query.ToListAsync()
                };

                return Ok(jsonData);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private bool GroupEnrollmentExists(Guid id)
        {
            return (_context.GroupEnrollments?.Any(e => e.GroupEnrollmentId == id)).GetValueOrDefault();
        }
    }
}
