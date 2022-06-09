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
    public class GroupEvaluationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupEvaluationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: GroupEvaluations
        public async Task<IActionResult> Index(Guid? id)
        {
        
            ViewData["NextController"] = "";
            ViewData["ParentController"] = "GroupEnrollments";
            ViewData["ParentId"] = id;

            // SelectList for "Create New" (ParticipantType) button
            ViewData["ParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType");

            // GroupEvaluations List
            var applicationDbContext = _context.GroupEvaluations
                .Include(g => g.EvaluationStatus)
                .Include(g => g.GroupEnrollments!).ThenInclude(g => g.Participants).ThenInclude(g => g.Sex)
                .Include(g => g.GroupEnrollments!).ThenInclude(g => g.Participants).ThenInclude(g => g.ParticipantTypes)
                .Include(g => g.ProgramAssessments)
                    .Where(g => g.GroupEnrollments.GroupId == id);

            // Group Details
            var group = await _context.Groups
                .Include(g => g.Programs!).ThenInclude(g => g.ProgramAssessments)
                .Include(g => g.Programs!).ThenInclude(g => g.AttendanceUnits)
                .Where(x => x.GroupId == id).FirstOrDefaultAsync();

            if (group != null)
            {
                ViewData["Closed"] = group.Closed;
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attendance
        public async Task<IActionResult> Attendance(Guid? id, Guid? groupEnrollmentId)
        {
            ViewData["ParentId"] = id;

            // Query Group Evaluations by filtered by GroupId
            var applicationDbContext = _context.GroupEvaluations
                .Include(g => g.GroupEnrollments).ThenInclude(g => g.Participants).ThenInclude(g => g.Sex)
                .Include(g => g.GroupEnrollments).ThenInclude(g => g.Participants).ThenInclude(g => g.ParticipantTypes)
                .Include(g => g.GroupEnrollments).ThenInclude(g => g.Groups)
                .Include(g => g.EvaluationStatus)
                .Where(g => g.GroupEnrollments.GroupId == id);

            // If groupEnrollmentId is not null, filter to return one enrollment participant 
            if (groupEnrollmentId != null)
            {
                applicationDbContext = applicationDbContext
                .Where(m => m.GroupEnrollmentId == groupEnrollmentId);
            }

            // Query Group to return Min, Max, and Attendance unit to view
            var group = await _context.Groups
                .Include(t => t.Programs!).ThenInclude(t =>t.AttendanceUnits)
                .Where(t => t.GroupId == id)
                .FirstOrDefaultAsync();

            //ViewData["EnableDuration"] = group.Programs.EnableDuration;
            if (group != null)
            {
                if (group.Programs != null)
                {
                    ViewData["Min"] = group.Programs.Min;
                    ViewData["Max"] = group.Programs.Max;
                    ViewData["AttendanceUnit"] = group.Programs!.AttendanceUnits!.AttendanceUnit;
                    ViewData["TrainingProgramId"] = group.ProgramId;
                }
            }
                        
            ViewData["RefEnrollmentStatusId"] = new SelectList(_context.EnrollmentStatus, "RefEnrollmentStatusId", "EnrollmentStatus");

            return View(await applicationDbContext.OrderBy(x => x.GroupEnrollments.Participants.LastName).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Attendance(List<GroupEnrollment> attendance, Guid? id, Guid? groupEnrollmentId)
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

        // GET: Attendance
        public async Task<IActionResult> Score(Guid? id, Guid? groupEnrollmentId)
        {
            ViewData["ParentId"] = id;

            // Query Group Evaluations by filtered by GroupId
            var applicationDbContext = _context.GroupEvaluations
                .Include(g => g.GroupEnrollments).ThenInclude(g => g.Participants).ThenInclude(g => g.Sex)
                .Include(g => g.GroupEnrollments).ThenInclude(g => g.Participants).ThenInclude(g => g.ParticipantTypes)
                .Include(g => g.GroupEnrollments).ThenInclude(g => g.Groups)
                .Include(g => g.ProgramAssessments!).ThenInclude(g => g.AssessmentTypes)
                .Include(g => g.EvaluationStatus)
                .Where(g => g.GroupEnrollments.GroupId == id);

            // If groupEnrollmentId is not null, filter to return one enrollment participant 
            if (groupEnrollmentId != null)
            {
                applicationDbContext = applicationDbContext
                .Where(m => m.GroupEnrollmentId == groupEnrollmentId);
            }

            // Query Group to return Min, Max, and Attendance unit to view
            var group = await _context.Groups
                .Include(t => t.Programs!).ThenInclude(t => t.ProgramAssessments)
                .Include(t => t.Programs!).ThenInclude(t => t.AttendanceUnits)
                .Where(t => t.GroupId == id)
                .FirstOrDefaultAsync();

            if (group != null)
            {
                ViewData["Min"] = group.Programs!.Min;
                ViewData["Max"] = group.Programs!.Max;
                ViewData["AttendanceUnit"] = group.Programs!.AttendanceUnits!.AttendanceUnit;
                ViewData["ProgramId"] = group.ProgramId;
            }
            
            ViewData["RefEvaluationStatusId"] = new SelectList(_context.EvaluationStatus, "RefEvaluationStatusId", "EvaluationStatus");

            return View(await applicationDbContext.OrderBy(x => x.GroupEnrollments.Participants.LastName).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Score(List<GroupEvaluation> score, Guid? id, Guid? groupEnrollmentId)
        {
            ViewData["ParentId"] = id;

            if (ModelState.IsValid)
            {
                foreach (GroupEvaluation item in score)
                {
                    GroupEvaluation? Exists_GroupEvaluation = await _context.GroupEvaluations.FindAsync(item.GroupEvaluationId);

                    if (Exists_GroupEvaluation != null)
                    {
                        Exists_GroupEvaluation.Score = item.Score;
                        Exists_GroupEvaluation.RefEvaluationStatusId = item.RefEvaluationStatusId;
                        Exists_GroupEvaluation.StatusDate = DateTime.Now;
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

        // GET: GroupEvaluations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEvaluation = await _context.GroupEvaluations
                .Include(g => g.EvaluationStatus)
                .Include(g => g.GroupEnrollments)
                .Include(g => g.ProgramAssessments)
                .FirstOrDefaultAsync(m => m.GroupEvaluationId == id);
            
            if (groupEvaluation == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = groupEvaluation.GroupEnrollments.GroupId;

            return View(groupEvaluation);
        }

        // GET: GroupEvaluations/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = id;
            ViewData["RefEvaluationStatusId"] = new SelectList(_context.EvaluationStatus, "EvaluationStatusId", "EvaluationStatus");
            ViewData["GroupEnrollmentId"] = new SelectList(_context.GroupEnrollments, "GroupEnrollmentId", "GroupEnrollmentId");
            ViewData["ProgramAssessmentId"] = new SelectList(_context.ProgramAssessments, "ProgramAssessmentId", "AssessmentName");
            return View();
        }

        // POST: GroupEvaluations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupEvaluationId,GroupEnrollmentId,EvaluationDate,ProgramAssessmentId,Score,StatusDate,RefEvaluationStatusId")] GroupEvaluation groupEvaluation)
        {
            if (ModelState.IsValid)
            {
                groupEvaluation.GroupEvaluationId = Guid.NewGuid();
                _context.Add(groupEvaluation);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), new { id = groupEvaluation.GroupEnrollments.GroupId });
            }
            ViewData["RefEvaluationStatusId"] = new SelectList(_context.EvaluationStatus, "EvaluationStatusId", "EvaluationStatus", groupEvaluation.RefEvaluationStatusId);
            ViewData["GroupEnrollmentId"] = new SelectList(_context.GroupEnrollments, "GroupEnrollmentId", "GroupEnrollmentId", groupEvaluation.GroupEnrollmentId);
            ViewData["ProgramAssessmentId"] = new SelectList(_context.ProgramAssessments, "ProgramAssessmentId", "AssessmentName", groupEvaluation.ProgramAssessmentId);
            ViewData["ParentId"] = groupEvaluation.GroupEnrollments.GroupId;
            return View(groupEvaluation);
        }

        // GET: GroupEvaluations/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEvaluation = await _context.GroupEvaluations.FindAsync(id);

            if (groupEvaluation == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = groupEvaluation.GroupEnrollments.GroupId;
            ViewData["RefEvaluationStatusId"] = new SelectList(_context.EvaluationStatus, "EvaluationStatusId", "EvaluationStatus", groupEvaluation.RefEvaluationStatusId);
            ViewData["GroupEnrollmentId"] = new SelectList(_context.GroupEnrollments, "GroupEnrollmentId", "GroupEnrollmentId", groupEvaluation.GroupEnrollmentId);
            ViewData["ProgramAssessmentId"] = new SelectList(_context.ProgramAssessments, "ProgramAssessmentId", "AssessmentName", groupEvaluation.ProgramAssessmentId);
            return View(groupEvaluation);
        }

        // POST: GroupEvaluations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GroupEvaluationId,GroupEnrollmentId,EvaluationDate,ProgramAssessmentId,Score,StatusDate,RefEvaluationStatusId")] GroupEvaluation groupEvaluation)
        {
            if (id != groupEvaluation.GroupEvaluationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupEvaluation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupEvaluationExists(groupEvaluation.GroupEvaluationId))
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
                return RedirectToAction(nameof(Index), new { id = groupEvaluation.GroupEnrollments.GroupId });
            }
            ViewData["RefEvaluationStatusId"] = new SelectList(_context.EvaluationStatus, "EvaluationStatusId", "EvaluationStatus", groupEvaluation.RefEvaluationStatusId);
            ViewData["GroupEnrollmentId"] = new SelectList(_context.GroupEnrollments, "GroupEnrollmentId", "GroupEnrollmentId", groupEvaluation.GroupEnrollmentId);
            ViewData["ProgramAssessmentId"] = new SelectList(_context.ProgramAssessments, "ProgramAssessmentId", "AssessmentName", groupEvaluation.ProgramAssessmentId);
            ViewData["ParentId"] = groupEvaluation.GroupEnrollments.GroupId;
            return View(groupEvaluation);
        }

        // GET: GroupEvaluations/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEvaluation = await _context.GroupEvaluations
                .Include(g => g.EvaluationStatus)
                .Include(g => g.GroupEnrollments)
                .Include(g => g.ProgramAssessments)
                    .FirstOrDefaultAsync(m => m.GroupEvaluationId == id);

            if (groupEvaluation == null)
            {
                return NotFound();
            }

            int relatedCount = 0;

            //relatedCount += groupEvaluation.ICollection.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            ViewData["ParentId"] = groupEvaluation.GroupEnrollments.GroupId;

            return View(groupEvaluation);
        }

        // POST: GroupEvaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var groupEvaluation = await _context.GroupEvaluations.FindAsync(id);

            if (groupEvaluation != null)
            {
                _context.GroupEvaluations.Remove(groupEvaluation);
                await _context.SaveChangesAsync();

                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD DELETED";
                TempData["message"] = "Record successfully deleted";

                return RedirectToAction(nameof(Index), new { id = groupEvaluation.GroupEnrollments.GroupId });
            }
            else
            {
                return NotFound();
            }

            
        }

        private bool GroupEvaluationExists(Guid id)
        {
            return _context.GroupEvaluations.Any(e => e.GroupEvaluationId == id);
        }
    }
}
