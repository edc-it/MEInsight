using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MEL.Entities.Identity;
using MEL.Data;
using MEL.Entities.Programs;


namespace MEL.Web.Controllers
{
    [Authorize]
    public class ProgramsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgramsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Programs
        public async Task<IActionResult> Index()
        {
        
            ViewData["NextController"] = "ProgramAssessments";
            ViewData["ParentController"] = "";
            
            var applicationDbContext = _context.Programs
                .Include(p => p.AttendanceUnits)
                .Include(p => p.OrganizationTypes)
                .Include(p => p.ProgramTypes)
                .Include(p => p.ProgramDeliveryTypes);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Programs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs
                .Include(p => p.AttendanceUnits)
                .Include(p => p.OrganizationTypes)
                .Include(p => p.ProgramTypes)
                .Include(p => p.ProgramDeliveryTypes)
                .FirstOrDefaultAsync(m => m.ProgramId == id);
            
            if (program == null)
            {
                return NotFound();
            }
            
            return View(program);
        }

        // GET: Programs/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create()
        {

            ViewData["RefAttendanceUnitId"] = new SelectList(_context.AttendanceUnits, "RefAttendanceUnitId", "AttendanceUnit");
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType");
            ViewData["RefProgramTypeId"] = new SelectList(_context.ProgramTypes, "RefProgramTypeId", "ProgramType");
            ViewData["RefProgramDeliveryTypeId"] = new SelectList(_context.ProgramDeliveryTypes, "RefProgramDeliveryTypeId", "ProgramDeliveryType");
            return View();
        }

        // POST: Programs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramId,ProgramName,RefProgramTypeId,RefProgramDeliveryTypeId,Description,Max,Min,RefAttendanceUnitId,HasAssessment,DisplayMarks,RefOrganizationTypeId")] MEL.Entities.Programs.Program program)
        {
            if (ModelState.IsValid)
            {
                _context.Add(program);
                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index));

            }
            ViewData["RefAttendanceUnitId"] = new SelectList(_context.AttendanceUnits, "RefAttendanceUnitId", "AttendanceUnit", program.RefAttendanceUnitId);
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType", program.RefOrganizationTypeId);
            ViewData["RefProgramTypeId"] = new SelectList(_context.ProgramTypes, "RefProgramTypeId", "ProgramType", program.RefProgramTypeId);
            ViewData["RefProgramDeliveryTypeId"] = new SelectList(_context.ProgramDeliveryTypes, "RefProgramDeliveryTypeId", "ProgramDeliveryType");

            return View(program);
        }

        // GET: Programs/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs.FindAsync(id);

            if (program == null)
            {
                return NotFound();
            }

            ViewData["RefAttendanceUnitId"] = new SelectList(_context.AttendanceUnits, "RefAttendanceUnitId", "AttendanceUnit", program.RefAttendanceUnitId);
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType", program.RefOrganizationTypeId);
            ViewData["RefProgramTypeId"] = new SelectList(_context.ProgramTypes, "RefProgramTypeId", "ProgramType", program.RefProgramTypeId);
            ViewData["RefProgramDeliveryTypeId"] = new SelectList(_context.ProgramDeliveryTypes, "RefProgramDeliveryTypeId", "ProgramDeliveryType");

            return View(program);
        }

        // POST: Programs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgramId,ProgramName,RefProgramTypeId,RefProgramDeliveryTypeId,Description,Max,Min,RefAttendanceUnitId,HasAssessment,DisplayMarks,RefOrganizationTypeId")] MEL.Entities.Programs.Program program)
        {
            if (id != program.ProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(program);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramExists(program.ProgramId))
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

                return RedirectToAction(nameof(Index));                

            }
            ViewData["RefAttendanceUnitId"] = new SelectList(_context.AttendanceUnits, "RefAttendanceUnitId", "AttendanceUnit", program.RefAttendanceUnitId);
            ViewData["RefOrganizationTypeId"] = new SelectList(_context.OrganizationTypes, "RefOrganizationTypeId", "OrganizationType", program.RefOrganizationTypeId);
            ViewData["RefProgramTypeId"] = new SelectList(_context.ProgramTypes, "RefProgramTypeId", "ProgramType", program.RefProgramTypeId);
            ViewData["RefProgramDeliveryTypeId"] = new SelectList(_context.ProgramDeliveryTypes, "RefProgramDeliveryTypeId", "ProgramDeliveryType");

            return View(program);
        }

        // GET: Programs/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Programs == null) { return NotFound(); }

            var program = await _context.Programs
                .Include(p => p.AttendanceUnits)
                .Include(p => p.OrganizationTypes)
                .Include(p => p.ProgramTypes)
                .Include(p => p.Groups)
                .Include(p => p.ProgramAssessments)
                    .FirstOrDefaultAsync(m => m.ProgramId == id);

            if (program == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
            relatedCount += program.Groups.Count();
            relatedCount += program.ProgramAssessments.Count();

            if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(program);
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Programs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Program'  is null.");
            }
            var program = await _context.Programs.FindAsync(id);

            if (program != null)
            {
                _context.Programs.Remove(program);
            }
                        
            await _context.SaveChangesAsync();

            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            return RedirectToAction(nameof(Index));        

        }

        private bool ProgramExists(int id)
        {
            return _context.Programs.Any(e => e.ProgramId == id);
        }
    }
}
