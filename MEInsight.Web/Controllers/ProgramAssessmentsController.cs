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
    public class ProgramAssessmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgramAssessmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ProgramAssessments
        public async Task<IActionResult> Index(int? id)
        {
        
            ViewData["NextController"] = "";
            ViewData["ParentController"] = "Programs";
            ViewData["ParentId"] = id;
            
            var applicationDbContext = _context.ProgramAssessments
                .Include(p => p.AssessmentTypes)
                .Include(p => p.AttendanceUnits)
                .Include(p => p.EvaluationStatus)
                .Include(p => p.Programs)
                .Where(p => p.ProgramId == id);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProgramAssessments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programAssessment = await _context.ProgramAssessments
                .Include(p => p.AssessmentTypes)
                .Include(p => p.AttendanceUnits)
                .Include(p => p.EvaluationStatus)
                .Include(p => p.Programs)
                .FirstOrDefaultAsync(m => m.ProgramAssessmentId == id);
            
            if (programAssessment == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = programAssessment.ProgramId;

            return View(programAssessment);
        }

        // GET: ProgramAssessments/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = id;
            ViewData["RefAssessmentTypeId"] = new SelectList(_context.AssessmentTypes, "RefAssessmentTypeId", "AssessmentType");
            ViewData["RefAttendanceUnitId"] = new SelectList(_context.AttendanceUnits, "RefAttendanceUnitId", "AttendanceUnit");
            ViewData["RefEvaluationStatusId"] = new SelectList(_context.EvaluationStatus, "EvaluationStatusId", "EvaluationStatus");
            ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "ProgramName");
            return View();
        }

        // POST: ProgramAssessments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramAssessmentId,ProgramId,AssessmentName,RefAssessmentTypeId,Description,Max,Min,RefAttendanceUnitId,MaximumScore,MinimumScore,CompletionScore,RefEvaluationStatusId")] ProgramAssessment programAssessment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programAssessment);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), new { id = programAssessment.ProgramId });
            }
            ViewData["RefAssessmentTypeId"] = new SelectList(_context.AssessmentTypes, "RefAssessmentTypeId", "AssessmentType", programAssessment.RefAssessmentTypeId);
            ViewData["RefAttendanceUnitId"] = new SelectList(_context.AttendanceUnits, "RefAttendanceUnitId", "AttendanceUnit", programAssessment.RefAttendanceUnitId);
            ViewData["RefEvaluationStatusId"] = new SelectList(_context.EvaluationStatus, "EvaluationStatusId", "EvaluationStatus", programAssessment.RefEvaluationStatusId);
            ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "ProgramName", programAssessment.ProgramId);
            ViewData["ParentId"] = programAssessment.ProgramId;
            return View(programAssessment);
        }

        // GET: ProgramAssessments/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programAssessment = await _context.ProgramAssessments.FindAsync(id);

            if (programAssessment == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = programAssessment.ProgramId;
            ViewData["RefAssessmentTypeId"] = new SelectList(_context.AssessmentTypes, "RefAssessmentTypeId", "AssessmentType", programAssessment.RefAssessmentTypeId);
            ViewData["RefAttendanceUnitId"] = new SelectList(_context.AttendanceUnits, "RefAttendanceUnitId", "AttendanceUnit", programAssessment.RefAttendanceUnitId);
            ViewData["RefEvaluationStatusId"] = new SelectList(_context.EvaluationStatus, "EvaluationStatusId", "EvaluationStatus", programAssessment.RefEvaluationStatusId);
            ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "ProgramName", programAssessment.ProgramId);
            return View(programAssessment);
        }

        // POST: ProgramAssessments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgramAssessmentId,ProgramId,AssessmentName,RefAssessmentTypeId,Description,Max,Min,RefAttendanceUnitId,MaximumScore,MinimumScore,CompletionScore,RefEvaluationStatusId")] ProgramAssessment programAssessment)
        {
            if (id != programAssessment.ProgramAssessmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programAssessment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramAssessmentExists(programAssessment.ProgramAssessmentId))
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
                return RedirectToAction(nameof(Index), new { id = programAssessment.ProgramId });
            }
            ViewData["RefAssessmentTypeId"] = new SelectList(_context.AssessmentTypes, "RefAssessmentTypeId", "AssessmentType", programAssessment.RefAssessmentTypeId);
            ViewData["RefAttendanceUnitId"] = new SelectList(_context.AttendanceUnits, "RefAttendanceUnitId", "AttendanceUnit", programAssessment.RefAttendanceUnitId);
            ViewData["RefEvaluationStatusId"] = new SelectList(_context.EvaluationStatus, "EvaluationStatusId", "EvaluationStatus", programAssessment.RefEvaluationStatusId);
            ViewData["ProgramId"] = new SelectList(_context.Programs, "ProgramId", "ProgramName", programAssessment.ProgramId);
            ViewData["ParentId"] = programAssessment.ProgramId;
            return View(programAssessment);
        }

        // GET: ProgramAssessments/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programAssessment = await _context.ProgramAssessments
                .Include(p => p.AssessmentTypes)
                .Include(p => p.AttendanceUnits)
                .Include(p => p.EvaluationStatus)
                .Include(p => p.Programs)
                .Include(p => p.GroupEvaluations)
                    .FirstOrDefaultAsync(m => m.ProgramAssessmentId == id);

            if (programAssessment == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                    relatedCount += programAssessment.GroupEvaluations.Count;

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            ViewData["ParentId"] = programAssessment.ProgramId;

            return View(programAssessment);
        }

        // POST: ProgramAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programAssessment = await _context.ProgramAssessments.FindAsync(id);

            if (programAssessment != null)
            {
                _context.ProgramAssessments.Remove(programAssessment);
            }
            
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            //return RedirectToAction(nameof(Index));        
            return RedirectToAction(nameof(Index), new { id = programAssessment!.ProgramId });
        }

        private bool ProgramAssessmentExists(int id)
        {
            return _context.ProgramAssessments.Any(e => e.ProgramAssessmentId == id);
        }
    }
}
