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
using MEInsight.Entities.Core;
using MEInsight.Web.Extensions;
using MEInsight.Entities.Programs;
using Microsoft.EntityFrameworkCore.Query;
using MEInsight.Web.Models;

namespace MEL.Web.Controllers
{
    [Authorize]
    public class ParticipantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ParticipantsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Participants
        public async Task<IActionResult> Index()
        {
            
            ViewData["NextController"] = "";
            ViewData["ParentController"] = "";
            ViewData["ParentId"] = "";

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

            // SelectList for "Create New" (ParticipantType) button
            ViewData["ParticipantTypeId"] = new SelectList(_context.ParticipantTypes, "RefParticipantTypeId", "ParticipantType");

            var participant = _context.Participants
            .Include(p => p.Locations)
            .Include(p => p.Organizations)
            .Include(p => p.ParticipantTypes)
            .Include(p => p.Sex);
            return View(await participant.ToListAsync());

        }

        // GET: Participants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participants = await
                              (from participant in _context.Participants
                               .Include(p => p.GroupEnrollments)
                               join student in _context.Students on participant.ParticipantId equals student.ParticipantId into ps
                               from participantStudent in ps.DefaultIfEmpty()
                               join teacher in _context.Teachers on participant.ParticipantId equals teacher.ParticipantId into pt
                               from participantTeacher in pt.DefaultIfEmpty()
                               join educationAdministrator in _context.EducationAdministrators on participant.ParticipantId equals educationAdministrator.ParticipantId into pea
                               from participantEducationAdministrator in pea.DefaultIfEmpty()
                               select new ParticipantsViewModel
                               {
                                   // Participant
                                   ParticipantId = participant.ParticipantId,
                                   RegistrationDate = participant.RegistrationDate,
                                   OrganizationId = participant.OrganizationId,
                                   RefParticipantTypeId = participant.RefParticipantTypeId,
                                   ParticipantTypes = participant.ParticipantTypes, //
                                   ParticipantCohorts = participant.ParticipantCohorts,
                                   ParticipantCode = participant.ParticipantCode,
                                   FirstName = participant.FirstName,
                                   MiddleName = participant.MiddleName,
                                   LastName = participant.LastName,
                                   RefSexId = participant.RefSexId,
                                   Sex = participant.Sex, //
                                   BirthDate = participant.BirthDate,
                                   Age = participant.Age,
                                   Disability = participant.Disability,
                                   RefDisabilityTypeId = participant.RefDisabilityTypeId,
                                   DisabilityTypes = participant.DisabilityTypes, //
                                   Phone = participant.Phone,
                                   Mobile = participant.Mobile,
                                   Email = participant.Email,
                                   Facebook = participant.Facebook,
                                   InstantMessenger = participant.InstantMessenger,
                                   RefLocationId = participant.RefLocationId,
                                   Address = participant.Address,
                                   //Student
                                   StudentCode = participantStudent.StudentCode,
                                   RefStudentTypeId = participantStudent.RefStudentTypeId,
                                   StudentTypes = participantStudent.StudentTypes, //
                                   RefStudentSpecializationId = participantStudent.RefStudentSpecializationId,
                                   StudentSpecializations = participantStudent.StudentSpecializations, //
                                   RefStudentYearOfStudyId = participantStudent.RefStudentYearOfStudyId,
                                   StudentYearOfStudies = participantStudent.StudentYearOfStudies,
                                   ParentGuardian = participantStudent.ParentGuardian,
                                   //Teacher
                                   RefTeacherTypeId = participantTeacher.RefTeacherTypeId,
                                   TeacherTypes = participantTeacher.TeacherTypes,
                                   RefTeacherPositionId = participantTeacher.RefTeacherPositionId,
                                   TeacherPositions = participantTeacher.TeacherPositions,
                                   RefTeacherEmploymentTypeId = participantTeacher.RefTeacherEmploymentTypeId,
                                   TeacherEmploymentTypes = participantTeacher.TeacherEmploymentTypes,
                                   GradeLevels = participantTeacher.GradeLevels,
                                   //Education Administrator
                                   RefEducationAdministratorTypeId = participantEducationAdministrator.RefEducationAdministratorTypeId,
                                   EducationAdministratorTypes = participantEducationAdministrator.EducationAdministratorTypes,
                                   RefEducationAdministratorPositionId = participantEducationAdministrator.RefEducationAdministratorPositionId,
                                   EducationAdministratorPositions = participantEducationAdministrator.EducationAdministratorPositions,
                                   RefEducationAdministratorOfficeId = participantEducationAdministrator.RefEducationAdministratorOfficeId,
                                   EducationAdministratorOffices = participantEducationAdministrator.EducationAdministratorOffices,
                                   //Base Entity
                                   CreatedBy = participant.CreatedBy,
                                   CreatedDate = participant.CreatedDate,
                                   ModifiedBy = participant.ModifiedBy,
                                   ModifiedDate = participant.ModifiedDate

                               }).SingleOrDefaultAsync(p => p.ParticipantId == id);


            if (participants == null)
            {
                return NotFound();
            }

            //ViewData["ParentId"] = participant.ParentId;

            return View(participants);
        }

        /// <summary>
        /// Creates a unique Participant.
        /// Creates a new Group Enrollment record for this Participant if the request originated from Group Enrollments.
        /// Creates a new Group Evaluation record for each Program Assessment associated to this Group/Program
        /// </summary>
        /// <param name="id">OrganizationId</param>
        /// <param name="groupId">
        /// The Group that originated the request (if any)
        /// </param>
        /// <param name="participantTypeId">
        /// The type of Participant to Create
        /// </param>
        /// <returns>A new unique Participant (and Group Enrollment/Evaluation records)</returns>
        // GET: Participants/Create
        [Authorize(Policy = "RequireCreateRole")]
        public async Task<IActionResult> Create(Guid? GroupId, int? RefParticipantTypeId)
        {

            if (RefParticipantTypeId == null)
            {
                return NotFound();
            }

            // If groupId is not null, Create a new participant and enroll to Group Enrollments
            if (GroupId != null)
            {

                var group = await _context.Groups
                    .Include(g => g.Organizations)
                    .Where(g => g.GroupId == GroupId)
                    .FirstOrDefaultAsync();

                if (group != null)
                {
                    // if School return OrganizationId for JSTree to pre-select Organization
                    if (group.Organizations!.RefOrganizationTypeId == 2)
                    {
                        ViewData["OrganizationId"] = group.OrganizationId;
                    }

                    ViewData["GroupId"] = GroupId;
                }
                
            }
            // If groupId is null, Create new Participant only
            else
            {
                // return related organization Ids
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

            // *** Select Lists ***
            // RefLocation
            var locationTypes = _context.LocationTypes;
            ViewData["RefLocationTypes"] = locationTypes;
            ViewData["RefLocationTypesCount"] = locationTypes.Count();

            ViewData["RefLocationId"] = new SelectList(_context.Locations
                .Include(x => x.LocationTypes)
                .Where(x =>
                    x.LocationTypes.LocationLevel == 1 &&
                    x.ParentLocationId == null)
                .Select(x => new
                {
                    x.RefLocationId,
                    x.LocationName
                }), "RefLocationId", "LocationName");

            // Participants - All
            // ViewData["ParentId"] = id;
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex");
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType");
            ViewData["RefParticipantTypeId"] = RefParticipantTypeId;
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort");

            return View();
        }

        // POST: Participants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ParticipantId,RegistrationDate,OrganizationId,RefParticipantTypeId,RefParticipantCohortId,ParticipantCode,FirstName,MiddleName,LastName,RefSexId,BirthDate,Age,Disability,RefDisabilityTypeId,Phone,Mobile,Email,Facebook,InstantMessenger,RefLocationId,Address,AdditionalData")] Participant participant,
            string[] RefLocationId,
            Guid? GroupId
            )
        {
            if (ModelState.IsValid)
            {

                // *** RefLocation ***
                // Gets the last non-null item in RefLocationId array,
                // the array might have null values for locations left empty
                // and this assigns the last non-null location to RefLocationId
                if (RefLocationId.Length > 0)
                {
                    var location =
                        (from r in RefLocationId where !string.IsNullOrEmpty(r) select r)
                        .OrderByDescending(r => r).Count();

                    participant.RefLocationId = RefLocationId[location - 1];
                }

                // Participant
                participant.ParticipantId = Guid.NewGuid();
                _context.Add(participant);

                // *** Create Group Enrollment and/or Group Evaluations ***
                // If Create action route includes GroupId parameter (from GroupEnrollment) 
                // Creates GroupEnrollment record for this Participant
                if (GroupId != null)
                {
                    // Get related Group
                    var group = await _context.Groups
                    .Include(x => x.Programs)
                    .Where(x => x.GroupId == GroupId)
                    .FirstOrDefaultAsync();

                    if (group != null)
                    {
                        // Create GroupEnrollment for this Participant
                        GroupEnrollment? groupEnrollment = new()
                        {
                            GroupEnrollmentId = Guid.NewGuid(),
                            GroupId = group.GroupId,
                            ParticipantId = participant.ParticipantId,
                            RefEnrollmentStatusId = 1, //Status Enrolled
                            EnrollmentDate = participant.RegistrationDate
                        };

                        _context.Add(groupEnrollment);

                        // If the Program selected for this Group has related ProgramAssessments
                        // Creates the GroupEvaluation records for each ProgramAssessment for this Participant
                        if (group.Programs!.HasAssessment == true)
                        {

                            List<ProgramAssessment> programAssessments = new();

                            // Get ProgramAssessments list related to this Group/Program 
                            programAssessments = await _context.ProgramAssessments
                                .Where(x => x.ProgramId == group.ProgramId)
                                .ToListAsync();

                            if (programAssessments != null)
                            {
                                foreach (var assessment in programAssessments)
                                {
                                    GroupEvaluation groupEvaluation = new()
                                    {
                                        GroupEvaluationId = Guid.NewGuid(),
                                        GroupEnrollmentId = groupEnrollment.GroupEnrollmentId,
                                        RefEvaluationStatusId = 1, // Status Enrolled
                                        ProgramAssessmentId = assessment.ProgramAssessmentId,
                                    };

                                    _context.GroupEvaluations.Add(groupEvaluation);
                                }
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();

                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";

                // If Action route includes GroupId redirect to GroupEnrollment
                if (GroupId != null)
                {
                    return RedirectToAction(nameof(Index), "GroupEnrollments", new { id = GroupId });
                }
                // Else redirect to Participants
                else
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            // If Model NotValid return to Create View (Get)
            // If groupId is not null, Create a new participant and enroll to Group Enrollments
            if (GroupId != null)
            {

                var refOrganizationTypeId = await _context.Groups
                    .Where(g => g.GroupId == GroupId)
                    .Select(g => g.Organizations!.RefOrganizationTypeId)
                    .FirstOrDefaultAsync();

                // if School return OrganizationId for JSTree to pre-select Organization
                if (refOrganizationTypeId == 1)
                {
                    ViewData["OrganizationId"] = participant.OrganizationId;
                }

                ViewData["GroupId"] = GroupId;
            }
            // If groupId is null, Create new Participant only
            else
            {
                // return related organization Ids
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

            // *** Select Lists ***
            // RefLocation
            var locationTypes = _context.LocationTypes;
            ViewData["RefLocationTypes"] = locationTypes;
            ViewData["RefLocationTypesCount"] = locationTypes.Count();

            ViewData["RefLocationId"] = new SelectList(_context.Locations
                .Include(x => x.LocationTypes)
                .Where(x =>
                    x.LocationTypes.LocationLevel == 1 &&
                    x.ParentLocationId == null)
                .Select(x => new
                {
                    x.RefLocationId,
                    x.LocationName
                }), "RefLocationId", "LocationName");

            // Participants - All
            ViewData["ParentId"] = participant.OrganizationId;
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", participant.RefSexId);
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", participant.RefDisabilityTypeId);
            ViewData["RefParticipantTypeId"] = participant.RefParticipantTypeId;
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", participant.RefParticipantCohortId);

            return RedirectToAction(nameof(Create), "Participants", new { participant.OrganizationId, GroupId, participant.RefParticipantTypeId });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(List<ParticipantsViewModel> upload, Guid? GroupId)
        {

            if (ModelState.IsValid)
            {
                foreach (ParticipantsViewModel item in upload)
                {
                    Participant participant = new()
                    {
                        ParticipantId = Guid.NewGuid(),
                        OrganizationId = item.OrganizationId,
                        RegistrationDate = item.RegistrationDate,
                        ParticipantCode = item.ParticipantCode,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        MiddleName = item.MiddleName,
                        RefParticipantTypeId = item.RefParticipantTypeId,
                        RefParticipantCohortId = item.RefParticipantCohortId,
                        RefSexId = item.RefSexId,
                        BirthDate = item.BirthDate,
                        Age = item.Age,
                        Disability = item.Disability,
                        RefDisabilityTypeId = item.RefDisabilityTypeId,
                        Phone = item.Phone,
                        Mobile = item.Mobile,
                        Email = item.Email,
                        Facebook = item.Facebook,
                        InstantMessenger = item.InstantMessenger,
                        RefLocationId = item.RefLocationId,
                        Address = item.Address

                    };
                    _context.Add(participant);
                    Console.WriteLine(participant);

                    GroupEnrollment groupEnrollment = new()
                    {
                        GroupEnrollmentId = Guid.NewGuid(),
                        GroupId = GroupId,
                        ParticipantId = participant.ParticipantId,
                        EnrollmentDate = item.EnrollmentDate,
                        Attendance = item.Attendance,
                        RefEnrollmentStatusId = item.RefEnrollmentStatusId
                    };
                    _context.Add(groupEnrollment);
                    Console.WriteLine(groupEnrollment);
                    
                }
                
                await _context.SaveChangesAsync();


                TempData["messageType"] = "success";
                TempData["messageTitle"] = "RECORD CREATED";
                TempData["message"] = "New record successfully created";

                // If Action route includes GroupId redirect to GroupEnrollment
                if (GroupId != null)
                {
                    return RedirectToAction(nameof(Index), "GroupEnrollments", new { id = GroupId });
                }
                // Else redirect to Participants
                else
                {
                    return RedirectToAction(nameof(Index));
                }

            }

            return View();
        }

        // GET: Participants/Edit/5
        [Authorize(Policy = "RequireEditRole")]
        public async Task<IActionResult> Edit(Guid? id, Guid? GroupId)
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

            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            // *** Select Lists ***
            // RefLocation
            var locationTypes = _context.LocationTypes;
            ViewData["RefLocationTypes"] = locationTypes;
            ViewData["RefLocationTypesCount"] = locationTypes.Count();

            ViewData["RefLocationId"] = new SelectList(_context.Locations
                .Include(x => x.LocationTypes)
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

            if (participant.RefLocationId != null)
            {
                var locations = EnumerableExtensions.ListLocations(allLocations, participant.RefLocationId);
                ViewData["RefLocationParents"] = locations.OrderBy(x => x.RefLocationTypeId);
            }

            // Participants - All
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", participant.RefSexId);
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", participant.RefDisabilityTypeId);
            ViewData["RefParticipantTypeId"] = participant.RefParticipantTypeId;
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", participant.RefParticipantCohortId);

            ViewData["GroupId"] = GroupId;

            return View(participant);
        }

        // POST: Participants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            Guid id,
            [Bind("ParticipantId,RegistrationDate,OrganizationId,RefParticipantTypeId,RefParticipantCohortId,ParticipantCode,FirstName,MiddleName,LastName,RefSexId,BirthDate,Age,Disability,RefDisabilityTypeId,Phone,Mobile,Email,Facebook,InstantMessenger,RefLocationId,Address,AdditionalData")] Participant participant,
            string[] RefLocationId,
            Guid? GroupId
            )
        {
            if (id != participant.ParticipantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    // *** RefLocation ***
                    // Gets the last non-null item in RefLocationId array,
                    // the array might have null values for locations left empty
                    // and this assigns the last non-null location to RefLocationId
                    if (RefLocationId.Length > 0)
                    {
                        var location =
                            (from r in RefLocationId where !string.IsNullOrEmpty(r) select r)
                            .OrderByDescending(r => r).Count();

                        participant.RefLocationId = RefLocationId[location - 1];
                    }

                    _context.Update(participant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantExists(participant.ParticipantId))
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

                // If Action route includes GroupId redirect to GroupEnrollment
                if (GroupId != null)
                {
                    return RedirectToAction(nameof(Index), "GroupEnrollments", new { id = GroupId });
                }
                // Else redirect to Participants
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            // If Model NotValid return to Create View (Get)
            // If groupId is not null, Create a new participant and enroll to Group Enrollments
            if (GroupId != null)
            {

                var refOrganizationTypeId = await _context.Groups
                    .Where(g => g.GroupId == GroupId)
                    .Select(g => g.Organizations!.RefOrganizationTypeId)
                    .FirstOrDefaultAsync();

                // if School return OrganizationId for JSTree to pre-select Organization
                if (refOrganizationTypeId == 1)
                {
                    ViewData["OrganizationId"] = participant.OrganizationId;
                }

                ViewData["GroupId"] = GroupId;
            }
            // If groupId is null, Create new Participant only
            else
            {
                // return related organization Ids
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

            // *** Select Lists ***
            // RefLocation
            var locationTypes = _context.LocationTypes;
            ViewData["RefLocationTypes"] = locationTypes;
            ViewData["RefLocationTypesCount"] = locationTypes.Count();

            ViewData["RefLocationId"] = new SelectList(_context.Locations
                .Include(x => x.LocationTypes)
                .Where(x =>
                    x.LocationTypes.LocationLevel == 1 &&
                    x.ParentLocationId == null)
                .Select(x => new
                {
                    x.RefLocationId,
                    x.LocationName
                }), "RefLocationId", "LocationName");

            // Participants - All
            ViewData["ParentId"] = participant.OrganizationId;
            ViewData["RefDisabilityTypeId"] = new SelectList(_context.DisabilityTypes, "RefDisabilityTypeId", "DisabilityType", participant.RefDisabilityTypeId);
            // List of Participant Types - excluding Student, Teacher, and Education Administrators
            int[] excludedParticipantTypesIds = { 1, 2, 3 };
            ViewData["RefParticipantTypeId"] = new SelectList(_context.ParticipantTypes
                .Where(p => !excludedParticipantTypesIds.Contains(p.RefParticipantTypeId)), "RefParticipantTypeId", "ParticipantType", participant.RefParticipantTypeId);
            ViewData["RefParticipantCohortId"] = new SelectList(_context.ParticipantCohorts, "RefParticipantCohortId", "ParticipantCohort", participant.RefParticipantCohortId);
            ViewData["RefSexId"] = new SelectList(_context.Sex, "RefSexId", "Sex", participant.RefSexId);
            
            return View(participant);

        }

        // GET: Participants/Delete/5
        [Authorize(Policy = "RequireDeleteRole")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _context.Participants
                .Include(p => p.Locations)
                .Include(p => p.Organizations)
                .Include(p => p.ParticipantTypes)
                .Include(p => p.ParticipantCohorts)
                .Include(p => p.Sex)
                //.Include(p => p.Students)
                //.Include(p => p.Teachers)
                //.Include(p => p.EducationAdministrators)
                .Include(p => p.GroupEnrollments)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);

            if (participant == null)
            {
                return NotFound();
            }

            int relatedCount = 0;
            relatedCount += participant.GroupEnrollments.Count;

            if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return View(participant);
        }

        // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
            {
                return NotFound();
            }

            // Students
            if (participant.RefParticipantTypeId == 1)
            {
                var student = await _context.Students.FindAsync(id);

                if (student != null)
                {
                    _context.Students.Remove(student);
                }
            }
            // Teachers
            else if (participant.RefParticipantTypeId == 2)
            {
                var teacher = await _context.Teachers.FindAsync(id);

                if (teacher != null)
                {
                    _context.Teachers.Remove(teacher);
                }
            }
            // Education Administrators
            else if (participant.RefParticipantTypeId == 3)
            {
                var educationAdministrator = await _context.EducationAdministrators.FindAsync(id);

                if (educationAdministrator != null)
                {
                    _context.EducationAdministrators.Remove(educationAdministrator);
                }
            }

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();

            TempData["messageType"] = "success";
            TempData["messageTitle"] = "RECORD DELETED";
            TempData["message"] = "Record successfully deleted";

            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantExists(Guid id)
        {
            return _context.Participants.Any(e => e.ParticipantId == id);
        }
    }
}
