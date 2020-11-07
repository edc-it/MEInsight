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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MEL.Web.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _environment;

        public GroupsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        // GET: Groups
        public async Task<IActionResult> Index(Guid? id)
        {
        
            ViewData["NextController"] = "GroupEnrollments";
            ViewData["ParentController"] = "Organizations";
            ViewData["ParentId"] = id;

            var applicationDbContext = _context.Groups
                .Include(m => m.GradeLevels)
                .Include(m => m.Organizations)
                .Include(m => m.Programs)
                .Include(m => m.Teachers)
                .Where(m => m.OrganizationId == id);
            
            return View(await applicationDbContext.OrderBy(m => m.GroupName).ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups
                .Include(m => m.GradeLevels)
                .Include(m => m.Organizations)
                .Include(m => m.Programs)
                .Include(m => m.Teachers).ThenInclude(m => m.Participants)
                .FirstOrDefaultAsync(m => m.GroupId == id);
            
            if (group == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = group.OrganizationId;

            return View(group);
        }

        // GET: TrainingGroups/GroupDetails/5
        public async Task<IActionResult> GroupDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups
                .Include(t => t.Organizations)
                .Include(t => t.Teachers).ThenInclude(t => t.Participants)
                .Include(t => t.GradeLevels)
                .Include(t => t.Programs)
                .SingleOrDefaultAsync(m => m.GroupId == id);

            if (group == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = group.OrganizationId;

            return View(group);
        }

        // GET: Groups/Create
        [Authorize(Policy = "RequireCreateRole")]
        public IActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = id;
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel");
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode");
            
            //Filter Teachers by this OrganizationId
            ViewData["ParticipantId"] = 
                new SelectList(_context.Teachers
                .Include(x => x.Participants)
                //.Where(x => x.Participants.OrganizationId == id)
                .Select(x => new {
                    x.ParticipantId,
                    x.Participants.NameId
                }), "ParticipantId", "NameId");

            // Get Organization Types
            var refOrganizationTypeId = _context.Organizations
                .Where(x => x.OrganizationId == id)
                .Select(x => x.RefOrganizationTypeId)
                .SingleOrDefault();

            // Get Programs
            var programs = _context.Programs;

            // If the Program related to this Organization Type is set,
            // returns the list of Programs only for this Organization Type, else returns all Programs
            if (programs.Any(x => x.RefOrganizationTypeId == refOrganizationTypeId))
            {
                ViewData["ProgramId"] = new SelectList(programs.Where(x => x.RefOrganizationTypeId == refOrganizationTypeId), "ProgramId", "ProgramName");
            }
            else
            {
                ViewData["ProgramId"] = new SelectList(programs.Where(x => x.RefOrganizationTypeId == null), "ProgramId", "ProgramName");
            }

            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,OrganizationId,ProgramId,GroupCode,GroupName,StartDate,EndDate,ParticipantId,RefGradeLevelId,CompletionDate,Closed,ClosedBy,ClosedDate,Url,FileName")] Group group)
        {
            if (ModelState.IsValid)
            {
                group.GroupId = Guid.NewGuid();
                _context.Add(group);

                await _context.SaveChangesAsync();
                
                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";
                
                return RedirectToAction(nameof(Index), new { id = group.OrganizationId });
            }
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel", group.RefGradeLevelId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", group.OrganizationId);
            ViewData["ParentId"] = group.OrganizationId;

            //Filter Teachers by this OrganizationId
            ViewData["ParticipantId"] =
                new SelectList(_context.Teachers
                .Include(x => x.Participants)
                //.Where(x => x.Participants.OrganizationId == group.OrganizationId)
                .Select(x => new {
                    x.ParticipantId,
                    x.Participants.NameId
                }), "ParticipantId", "NameId", group.ParticipantId);

            // Get Organization Types
            var refOrganizationTypeId = _context.Organizations
                .Where(x => x.OrganizationId == group.OrganizationId)
                .Select(x => x.RefOrganizationTypeId)
                .SingleOrDefault();

            // Get Programs
            var programs = _context.Programs;

            // If the Program related to this Organization Type is set,
            // returns the list of Programs only for this Organization Type, else returns all Programs
            if (programs.Any(x => x.RefOrganizationTypeId == refOrganizationTypeId))
            {
                ViewData["ProgramId"] = new SelectList(programs.Where(x => x.RefOrganizationTypeId == refOrganizationTypeId), "ProgramId", "ProgramName", group.ProgramId);
            }
            else
            {
                ViewData["ProgramId"] = new SelectList(programs.Where(x => x.RefOrganizationTypeId == null), "ProgramId", "ProgramName", group.ProgramId);
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = group.OrganizationId;
            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel", group.RefGradeLevelId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", group.OrganizationId);

            //Filter Teachers by this OrganizationId
            ViewData["ParticipantId"] = 
                new SelectList(_context.Teachers
                .Include(x => x.Participants)
                //.Where(x => x.Participants.OrganizationId == group.OrganizationId)
                .Select(x => new {
                    x.ParticipantId,
                    x.Participants.NameId
                }), "ParticipantId", "NameId", group.ParticipantId);

            // Get Organization Types
            var refOrganizationTypeId = _context.Organizations
                .Where(x => x.OrganizationId == group.OrganizationId)
                .Select(x => x.RefOrganizationTypeId)
                .SingleOrDefault();

            // Get Programs
            var programs = _context.Programs;

            // If the Program related to this Organization Type is set,
            // returns the list of Programs only for this Organization Type, else returns all Programs
            if (programs.Any(x => x.RefOrganizationTypeId == refOrganizationTypeId))
            {
                ViewData["ProgramId"] = new SelectList(programs.Where(x => x.RefOrganizationTypeId == refOrganizationTypeId), "ProgramId", "ProgramName", group.ProgramId);
            }
            else
            {
                ViewData["ProgramId"] = new SelectList(programs.Where(x => x.RefOrganizationTypeId == null), "ProgramId", "ProgramName", group.ProgramId);
            }

            return View(group);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GroupId,OrganizationId,ProgramId,GroupCode,GroupName,StartDate,EndDate,ParticipantId,RefGradeLevelId,CompletionDate,Closed,ClosedBy,ClosedDate,Url,FileName")] Group group)
        {
            if (id != group.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(group.GroupId))
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

                return RedirectToAction(nameof(Index), new { id = group.OrganizationId });
            }

            ViewData["RefGradeLevelId"] = new SelectList(_context.GradeLevels, "RefGradeLevelId", "GradeLevel", group.RefGradeLevelId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationCode", group.OrganizationId);
            ViewData["ParentId"] = group.OrganizationId;

            //Filter Teachers by this OrganizationId
            ViewData["ParticipantId"] =
                new SelectList(_context.Teachers
                .Include(x => x.Participants)
                //.Where(x => x.Participants.OrganizationId == group.OrganizationId)
                .Select(x => new {
                    x.ParticipantId,
                    x.Participants.NameId
                }), "ParticipantId", "NameId", group.ParticipantId);

            // Get Organization Types
            var refOrganizationTypeId = _context.Organizations
                .Where(x => x.OrganizationId == id)
                .Select(x => x.RefOrganizationTypeId)
                .SingleOrDefault();

            // Get Programs
            var programs = _context.Programs;

            // If the Program related to this Organization Type is set,
            // returns the list of Programs only for this Organization Type, else returns all Programs
            if (programs.Any(x => x.RefOrganizationTypeId == refOrganizationTypeId))
            {
                ViewData["ProgramId"] = new SelectList(programs.Where(x => x.RefOrganizationTypeId == refOrganizationTypeId), "ProgramId", "ProgramName", group.ProgramId);
            }
            else
            {
                ViewData["ProgramId"] = new SelectList(programs.Where(x => x.RefOrganizationTypeId == null), "ProgramId", "ProgramName", group.ProgramId);
            }

            return View(group);
        }

        // GET: Groups/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups
                .Include(m => m.GradeLevels)
                .Include(m => m.Organizations)
                .Include(m => m.Programs)
                .Include(m => m.Teachers)
                .Include(m => m.GroupEnrollments)
                    .FirstOrDefaultAsync(m => m.GroupId == id);

            if (group == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
                    relatedCount += group.GroupEnrollments.Count();

            if(relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;
            ViewData["ParentId"] = group.OrganizationId;

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var group = await _context.Groups.FindAsync(id);

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        
            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            //return RedirectToAction(nameof(Index));        
            return RedirectToAction(nameof(Index), new { id = group.OrganizationId });
        }

        /// <summary>
        /// Uploads a document related to the Group
        /// i.e. a Scanned PDF with the Group training attendance form
        /// </summary>
        /// <param name="file">File to upload</param>
        /// <param name="id">Group Id</param>
        /// <returns></returns>
        [HttpPost, ActionName("Upload")]
        public async Task<IActionResult> Upload(IFormFile file, Guid? id)
        {
            if (file == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            Group group = await _context.Groups.FindAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            long size = file.Length;

            // Get folder path URL = ~/wwwroot/uploads
            var filePath = Path.Combine(_environment.WebRootPath, "uploads");

            if (size > 0)
            {
                // Limit file uploads to 50M
                if (size > 52428800)
                {
                    TempData["messageType"] = "error";
                    TempData["messageTitle"] = "FILE NOT UPLOADED";
                    TempData["message"] = "File size exceeded the maximum size permitted (50MB)";
                    return RedirectToAction("Details", "Groups", new { id });
                }

                // Limit file types to .jpg, .jpeg, .png, .doc, and .pdf only
                if (!CheckFileType(file.FileName))
                {
                    TempData["messageType"] = "error";
                    TempData["messageTitle"] = "FILE NOT UPLOADED";
                    TempData["message"] = "File type not valid. Upload PDF, Documents, or Images";
                    return RedirectToAction("Details", "Groups", new { id });
                }

                // Current file extension
                string extension = Path.GetExtension(file.FileName);

                // Set Filename format (i.e. Document-20190815141805-{id}{.pdf})
                var fileName = "Document" + DateTime.Now.ToString("-yyyyMMddHHmmss-") + id + extension;

                // Upload file to server
                using (var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Build host URL and /uploads/ path
                var request = HttpContext.Request;
                var uriBuilder = new UriBuilder
                {
                    Host = request.Host.Host,
                    Scheme = request.Scheme,
                    Path = "uploads/" + fileName
                };

                if (request.Host.Port.HasValue)
                    uriBuilder.Port = request.Host.Port.Value;

                var url = uriBuilder.ToString();

                // Update database with FileName and URL
                try
                {
                    group.FileName = fileName;
                    group.Url = url;
                    _context.Update(group);

                    TempData["messageType"] = "success";
                    TempData["messageTitle"] = "RECORDS UPDATED";
                    TempData["message"] = "Records updated successfully";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(group.GroupId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = group.GroupId });
            }

            TempData["messageType"] = "error";
            TempData["messageTitle"] = "FILE NOT UPLOADED";
            TempData["message"] = "A file could not be uploaded error";

            return RedirectToAction("Details", "Groups", new { id });
        }

        private bool GroupExists(Guid id)
        {
            return _context.Groups.Any(e => e.GroupId == id);
        }

        #region Helpers
        //Allowed file upload types
        bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".png":
                    return true;
                case ".doc":
                    return true;
                case ".docx":
                    return true;
                case ".pdf":
                    return true;
                default:
                    return false;
            }
        }
        #endregion
    }
}
