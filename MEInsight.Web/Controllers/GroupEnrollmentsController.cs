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
using MEInsight.Web.Data;
using MEInsight.Entities.Programs;


namespace MEL.Web.Controllers
{
    [Authorize]
    public class GroupEnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupEnrollmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: GroupEnrollments
        public async Task<IActionResult> Index(Guid? id)
        {
        
            ViewData["ParentController"] = "Groups";
            ViewData["ParentId"] = id;

            // SelectList for "Create New" (ParticipantType) button
            ViewData["ParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType");

            // Group Details
            var group = await _context.Groups
                .Include(g => g.Programs!).ThenInclude(g => g.ProgramAssessments)
                .Include(g => g.Programs!).ThenInclude(g => g.AttendanceUnits)
                .Where(x => x.GroupId == id).FirstOrDefaultAsync();

            if (group == null) return NotFound();

            if (group.Programs!.HasAssessment)
            {
                var programAssessments = group.Programs.ProgramAssessments.Any();

                if (programAssessments)
                {
                    ViewData["HasAssessements"] = true;
                }
            }

            ViewData["Closed"] = group.Closed;
            ViewData["ProgramAttendanceUnit"] = group.Programs.AttendanceUnits!.AttendanceUnit;

            ViewData["OrganizationId"] = group.OrganizationId;

            return View();
        }

        // GET: Attendance
        public async Task<IActionResult> Attendance(Guid? id, Guid? groupEnrollmentId)
        {
            ViewData["ParentId"] = id;

            // Query Group Enrollments by filtered by GroupId
            var applicationDbContext = _context.GroupEnrollments
                .Include(g => g.Participants!).ThenInclude(g => g.Sex)
                .Include(g => g.Participants!.ParticipantTypes)
                .Include(g => g.Groups)
                .Include(g => g.EnrollmentStatus)
                .Where(g => g.GroupId == id);

            // If groupEnrollmentId is not null, filter to return one enrollment participant 
            if (groupEnrollmentId != null)
            {
                applicationDbContext = applicationDbContext
                .Where(m => m.GroupEnrollmentId == groupEnrollmentId);
            }

            // Query Group to return Min, Max, and Attendance unit to view
            var group = await _context.Groups
                .Include(t => t.Programs!).ThenInclude(t => t.AttendanceUnits)
                .Where(t => t.GroupId == id)
                .FirstOrDefaultAsync();

            if (group != null)
            {
                if (group.Programs!.Min == null)
                {
                    ViewData["Min"] = 0;
                }
                else
                {
                    ViewData["Min"] = group.Programs.Min;
                }
                if (group.Programs!.Max == null)
                {
                    ViewData["Max"] = 0;
                }
                else
                {
                    ViewData["Max"] = group.Programs.Max;
                }

                ViewData["AttendanceUnit"] = group.Programs.AttendanceUnits!.AttendanceUnit;
                ViewData["TrainingProgramId"] = group.ProgramId;

            }
            
            ViewData["RefEnrollmentStatusId"] = new SelectList(_context.EnrollmentStatus, "RefEnrollmentStatusId", "EnrollmentStatus");

            return View(await applicationDbContext.OrderBy(x => x.Participants!.LastName).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Attendance(List<GroupEnrollment> attendance, Guid? id)
        {
            ViewData["ParentId"] = id;

            if (ModelState.IsValid)
            {

                foreach (GroupEnrollment item in attendance)
                {
                    GroupEnrollment? Exists_GroupEnrollment = await _context.GroupEnrollments.FindAsync(item.GroupEnrollmentId);

                    if (Exists_GroupEnrollment != null)
                    {
                        Exists_GroupEnrollment.Attendance = item.Attendance;
                        Exists_GroupEnrollment.RefEnrollmentStatusId = item.RefEnrollmentStatusId;
                        Exists_GroupEnrollment.StatusDate = DateTime.Now;
                    }
                }

                await _context.SaveChangesAsync();

                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORDS UPDATED";
                TempData["message"] = "Records updated successfully";

                return RedirectToAction("Index", new { id });
            }

            return View();
        }

        // GET: GroupEnrollments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEnrollment = await _context.GroupEnrollments
                .Include(g => g.EnrollmentStatus)
                .Include(g => g.Groups)
                .Include(g => g.Participants)
                .FirstOrDefaultAsync(m => m.GroupEnrollmentId == id);

            if (groupEnrollment == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["ParentId"] = groupEnrollment.GroupId;
                ViewData["Closed"] = groupEnrollment.Groups!.Closed;

                return View(groupEnrollment);
            }
            
        }

        // GET: GroupEnrollments/Create
        [Authorize(Policy = "RequireCreateRole")]
        public async Task<IActionResult> Create(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

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

            //Get OrganizationId to filter only the Participants from the Group.Organization
            var group = _context.Groups
                .Where(t => t.GroupId == id)
                .FirstOrDefault();

            if (group == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["CurrentOrganizationId"] = group.OrganizationId;
                ViewData["Group"] = group;

                ViewData["GroupStartDate"] = group.StartDate.ToShortDateString();
                ViewData["GroupEndDate"] = group.EndDate?.ToShortDateString();
            }

            ViewData["ParentId"] = id;
            ViewData["RefEnrollmentStatusId"] = new SelectList(_context.EnrollmentStatus, "RefEnrollmentStatusId", "EnrollmentStatus");
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupCode");
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "ParticipantId", "FirstName");


            return View();
        }

        // POST: GroupEnrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid?[] ParticipantId, DateTime? EnrollmentDate, Guid GroupId)
        {
            if (ModelState.IsValid)
            {

                var group = await _context.Groups
                   .Include(x => x.Programs)
                   .Where(x => x.GroupId == GroupId)
                   .FirstOrDefaultAsync();

                if (group == null) 
                { 
                    return NotFound();
                }
                else
                {
                    foreach (var item in ParticipantId)
                    {
                        GroupEnrollment groupEnrollment = new()
                        {
                            GroupEnrollmentId = Guid.NewGuid(),
                            ParticipantId = item!.Value,
                            EnrollmentDate = EnrollmentDate,
                            RefEnrollmentStatusId = 1,
                            GroupId = GroupId,
                        };
                        _context.Add(groupEnrollment);
                    }

                    await _context.SaveChangesAsync();

                    TempData["messageType"] = "success";
                    TempData["messageTitle"] = "RECORD CREATED";
                    TempData["message"] = "New record successfully created";

                    return RedirectToAction(nameof(Index), new { id = group.GroupId });
                }

                
            }
            
            return View();
        }

        // GET: GroupEnrollments/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEnrollment = await _context.GroupEnrollments
                .Include(x => x.Groups)
                .Where(x => x.GroupEnrollmentId == id)
                .FirstOrDefaultAsync();

            if (groupEnrollment == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["ParentId"] = groupEnrollment.GroupId;
                ViewData["RefEnrollmentStatusId"] = new SelectList(_context.EnrollmentStatus, "RefEnrollmentStatusId", "EnrollmentStatus", groupEnrollment.RefEnrollmentStatusId);
                ViewData["GroupId"] = new SelectList(_context.Groups
                    .Where(x => x.OrganizationId == groupEnrollment.Groups!.OrganizationId), "GroupId", "GroupName", groupEnrollment.GroupId);
                return View(groupEnrollment);
            }
            
        }

        // POST: GroupEnrollments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GroupEnrollmentId,GroupId,ParticipantId,EnrollmentDate,Attendance,StatusDate,RefEnrollmentStatusId")] GroupEnrollment groupEnrollment)
        {
            if (id != groupEnrollment.GroupEnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupEnrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupEnrollmentExists(groupEnrollment.GroupEnrollmentId))
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
                return RedirectToAction(nameof(Index), new { id = groupEnrollment.GroupId });
            }
            ViewData["RefEnrollmentStatusId"] = new SelectList(_context.EnrollmentStatus, "RefEnrollmentStatusId", "EnrollmentStatus", groupEnrollment.RefEnrollmentStatusId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupCode", groupEnrollment.GroupId);
            ViewData["ParticipantId"] = new SelectList(_context.Participants, "ParticipantId", "FirstName", groupEnrollment.ParticipantId);
            ViewData["ParentId"] = groupEnrollment.GroupId;
            return View(groupEnrollment);
        }

        // GET: GroupEnrollments/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEnrollment = await _context.GroupEnrollments
                .Include(g => g.EnrollmentStatus)
                .Include(g => g.Groups)
                .Include(g => g.GroupEvaluations)
                .Include(g => g.Participants)
                    .FirstOrDefaultAsync(m => m.GroupEnrollmentId == id);

            if (groupEnrollment == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                    relatedCount += groupEnrollment.GroupEvaluations.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            ViewData["ParentId"] = groupEnrollment.GroupId;

            return View(groupEnrollment);
        }

        // POST: GroupEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var groupEnrollment = await _context.GroupEnrollments.FindAsync(id);

            if (groupEnrollment == null)
            {
                return NotFound();
            }
            
            _context.GroupEnrollments.Remove(groupEnrollment);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            return RedirectToAction(nameof(Index), new { id = groupEnrollment.GroupId });
        }

        private bool GroupEnrollmentExists(Guid id)
        {
            return _context.GroupEnrollments.Any(e => e.GroupEnrollmentId == id);
        }
    }
}
