using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEInsight.Web.Data;
using MEInsight.Entities.Identity;
using MEInsight.Web.Models;
using MEInsight.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// NuGet package System.Linq.Dynamic.Core
using System.Linq.Dynamic.Core;
using MEInsight.Entities.Programs;
using Microsoft.AspNetCore.Authorization;
using MEInsight.Entities.TLM;

namespace MEL.Web.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns a JSON response with the list of Locations filtered by ParentLocationId(id).
        /// Used to populate cascading dropdowns for RefLocations and each RefLocationType.Level 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>[{ refLocationId: locationName }] - JSON response with the list of filtered Locations</returns>
        [AcceptVerbs("Post")]
        public IActionResult GetLocations(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model =
                from location in _context.Locations
                where location.ParentLocationId == id
                select new
                {
                    location.RefLocationId,
                    location.LocationName
                };

            if (model == null)
            {
                return NotFound();
            }

            return Json(model);
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> CloseGroup(Guid? id, string CompletionDate)
        {
            if (id == null)
            {
                return Json(new { Status = false });
            }

            DateTime completionDate;
            if (CompletionDate == null)
            {
                return Json(new { Status = false });
            }
            else
            {
                completionDate = DateTime.Parse(CompletionDate);
            }

            Group? group = await _context.Groups.FindAsync(id);

            if (group == null)
            {
                return Json(new { Status = false });
            }

            group.Closed = true;
            group.ClosedDate = DateTime.Now;
            group.ClosedBy = User?.Identity?.Name;
            group.CompletionDate = completionDate;

            _context.Update(group);
            await _context.SaveChangesAsync();

            return Json(new { Status = true });

        }

        [AcceptVerbs("Post")]
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> OpenGroup(Guid? id)
        {
            if (id == null)
            {
                return Json(new { Status = false });
            }

            Group? group = await _context.Groups.FindAsync(id);

            if (group == null)
            {
                return Json(new { Status = false });
            }

            group.Closed = false;
            group.ClosedDate = null;
            group.ClosedBy = null;
            group.CompletionDate = null;

            _context.Update(group);
            await _context.SaveChangesAsync();

            return Json(new { Status = true });

        }

        /// <summary>
        /// Closes a TLM Distribution Period from Editing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CompletionDate"></param>
        /// <returns></returns>
        [AcceptVerbs("Post")]
        public async Task<IActionResult> CloseDistribution(int? id)
        {
            if (id == null)
            {
                return Json(new { Status = false });
            }

            TLMDistributionPeriod? distributionPeriod = await _context.TLMDistributionPeriods.FindAsync(id);

            if (distributionPeriod == null)
            {
                return Json(new { Status = false });
            }

            distributionPeriod.Closed = true;
            distributionPeriod.ClosedDate = DateTime.Now;
            distributionPeriod.ClosedBy = User?.Identity?.Name;

            _context.Update(distributionPeriod);
            await _context.SaveChangesAsync();

            return Json(new { Status = true });

        }

        /// <summary>
        /// Opens a TLM Distribution Period for Editing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AcceptVerbs("Post")]
        [Authorize(Policy = "RequireMELRole")]
        public async Task<IActionResult> OpenDistribution(int? id)
        {
            if (id == null)
            {
                return Json(new { Status = false });
            }

            TLMDistributionPeriod? distributionPeriod = await _context.TLMDistributionPeriods.FindAsync(id);

            if (distributionPeriod == null)
            {
                return Json(new { Status = false });
            }

            distributionPeriod.Closed = false;
            distributionPeriod.ClosedDate = null;
            distributionPeriod.ClosedBy = null;

            _context.Update(distributionPeriod);
            await _context.SaveChangesAsync();

            return Json(new { Status = true });

        }

        /// <summary>
        /// Returns a JSON response with the list of Organizations filtered by ParentLocationId(id)
        /// Formated for use with JStree plugin.
        /// </summary>
        /// <param name="id">Guid OrganizationId</param>
        /// <returns>JSON Array i.e. [{ id : "OrganizationId", text : "OrganizationName", parent: "ParentId", icon: "FontAwesome Icon Class" }]</returns>
        [AcceptVerbs("Get")]
        public IActionResult GetOrganizationTree(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            // Select ALL Organizations
            var AllOrganizations = _context.Organizations
                .Select(x => new
                {
                    Id = x.OrganizationId,
                    Text = x.OrganizationName + " (" + x.OrganizationCode + ")",
                    Parent = x.ParentOrganizationId,
                    Icon = x.OrganizationTypes!.OrganizationType == "School" ? "far fa-building" : "fas fa-building"

                }).ToList();

            //Select the OrganizationParentId for the id parameter (OrganizationId) - this includes the Parent in the hierachical tree
            //var parent = AllOrganizations.Where(x => x.Id == id).Select(x => x.Parent).FirstOrDefault();
            var parent = AllOrganizations.Where(x => x.Id == id).Select(x => x.Id).FirstOrDefault();

            //Select parent organization object
            var top = AllOrganizations.Where(x => x.Id == id).Select(x => new
            {
                x.Id,
                x.Text,
                Parent = "#",
                x.Icon
            }).ToList();

            //Creates a generic Lookup<TKey,TElement>
            var lookup = AllOrganizations.ToLookup(x => x.Parent);

            //Flattens (the lookup) filtering all the children from the selected organization
            var model = lookup[parent].SelectRecursive(x => lookup[x.Id])
                .Select(x => new
                {
                    x.Id,
                    x.Text,
                    Parent = x.Parent == null ? "#" : x.Id == id ? "#" : x.Parent.ToString(),
                    x.Icon
                })
                .ToList();

            if (top == null)
            {
                return NotFound();
            }

            //Add parent organization object to model
            model.AddRange(top!);

            if (model == null)
            {
                return NotFound();
            }

            return Json(model);
        }

        //[AcceptVerbs("Post")]
        public async Task<IActionResult> GetParticipants(Guid? organizationId)
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

            // Get Participants
            var participants =
                              (from participant in _context.Participants
                               .Include(p => p.GroupEnrollments)
                               .Include(p => p.Organizations)
                               .Include(p => p.ParticipantTypes)
                               .Include(p => p.Sex)
                               join student in _context.Students on participant.ParticipantId equals student.ParticipantId into ps
                               from participantStudent in ps.DefaultIfEmpty()
                               join teacher in _context.Teachers on participant.ParticipantId equals teacher.ParticipantId into pt
                               from participantTeacher in pt.DefaultIfEmpty()
                               join educationAdministrator in _context.EducationAdministrators on participant.ParticipantId equals educationAdministrator.ParticipantId into pea
                               from participantEducationAdministrator in pea.DefaultIfEmpty()
                               select new ParticipantsListViewModel
                               {
                                   // Participant
                                   ParticipantId = participant.ParticipantId,
                                   RegistrationDate = participant.RegistrationDate.ToShortDateString(),
                                   OrganizationId = participant.OrganizationId,
                                   OrganizationName = participant.Organizations!.OrganizationName,
                                   ParticipantType = participant.ParticipantTypes!.ParticipantType,
                                   ParticipantCode = participant.ParticipantCode,
                                   Name = participant.Name,
                                   Sex = participant.Sex!.Sex,
                                   BirthDate = participant.BirthDate ?? null,
                                   Age = participant.Age ?? null,
                                   Disability = participant.Disability ?? null,
                                   DisabilityType = participant.DisabilityTypes!.DisabilityType ?? null,
                                   RefLocationId = participant.RefLocationId,
                                   Location = participant.Organizations!.Locations!.LocationTypes.LocationLevel == 4 ? participant.Organizations!.Locations!.ParentLocations!.LocationName + ", " + participant.Organizations!.Locations!.ParentLocations!.ParentLocations!.LocationName
                                                : participant.Organizations!.Locations!.LocationTypes.LocationLevel == 3 ? participant.Organizations!.Locations!.LocationName + ", " + participant.Organizations!.Locations!.ParentLocations!.LocationName
                                                      : participant.Organizations!.Locations!.LocationTypes.LocationLevel == 2 ? participant.Organizations!.Locations!.LocationName
                                                        : "",
                                   //Student
                                   StudentCode = participantStudent.StudentCode ?? null,
                                   StudentType = participantStudent.StudentTypes!.StudentType ?? null, //
                                   StudentSpecialization = participantStudent.StudentSpecializations!.StudentSpecialization ?? null, //
                                   StudentYearOfStudy = participantStudent.StudentYearOfStudies!.StudentYearOfStudy ?? null,

                                   TeacherType = participantTeacher.TeacherTypes!.TeacherType ?? null,
                                   TeacherPosition = participantTeacher.TeacherPositions!.TeacherPosition ?? null,
                                   TeacherEmploymentType = participantTeacher.TeacherEmploymentTypes!.TeacherEmploymentType ?? null,

                                   GradeLevels = participantTeacher.GradeLevels ?? null,
                                   //Education Administrator
                                   EducationAdministratorType = participantEducationAdministrator.EducationAdministratorTypes!.EducationAdministratorType ?? null,
                                   EducationAdministratorPosition = participantEducationAdministrator.EducationAdministratorPositions!.EducationAdministratorPosition ?? null,
                                   EducationAdministratorOffice = participantEducationAdministrator.EducationAdministratorOffices!.EducationAdministratorOffice ?? null,

                                   //Combined
                                   Position = participant.RefParticipantTypeId == 2 ? participantTeacher.TeacherPositions.TeacherPosition
                                        : participant.RefParticipantTypeId == 3 ? participantEducationAdministrator.EducationAdministratorPositions.EducationAdministratorPosition
                                        : null,

                                   //Base Entity
                                   CreatedBy = participant.CreatedBy ?? null,
                                   CreatedDate = participant.CreatedDate ?? null,
                                   ModifiedBy = participant.ModifiedBy ?? null,
                                   ModifiedDate = participant.ModifiedDate ?? null,
                               });

            if (organization.IsOrganizationUnit != true)
            {
                participants = participants
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

                participants = participants
                    .Where(p => organizationIds
                    .Contains((Guid)p.OrganizationId!));
            }

            // DATATABLES POST REQUEST
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
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10, 20, 50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;

            // CUSTOM FILTERS:
            var groupId = Request.Form["groupId"].FirstOrDefault();
            if (!string.IsNullOrEmpty(groupId))
            {
                var enrolledIds = _context.GroupEnrollments
                    .Where(x => x.GroupId == Guid.Parse(groupId))
                    .Select(x => x.ParticipantId).ToList();

                participants = participants
                    .Where(p => enrolledIds.Contains((Guid)p.ParticipantId!));
            }
            else
            {
                // by Selected Organizations Array from jsTree
                string[] selectedOrganizations = Request.Form["selectedOrganizations[]"].ToArray();
                if (selectedOrganizations != null && selectedOrganizations.Length != 0)
                {
                    participants = participants
                        .Where(p => selectedOrganizations.Select(Guid.Parse)
                        .Contains((Guid)p.OrganizationId!));
                }
            }

            //Sorting  
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                //OrderBy - requires Linq.Dynamic.Core
                participants = participants.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            //Search  
            if (!string.IsNullOrEmpty(searchValue))
            {
                participants = participants
                    .Where(m =>
                        m.ParticipantCode!.ToLower().Contains(searchValue.ToLower()) ||
                        m.Name!.ToLower().Contains(searchValue.ToLower()) ||
                        m.Sex!.ToLower().Contains(searchValue.ToLower()) ||
                        m.ParticipantType!.ToLower().Contains(searchValue.ToLower()) ||
                        m.OrganizationName!.ToLower().Contains(searchValue.ToLower())
                    );
            }

            //total number of rows counts   
            recordsTotal = participants.Count();

            //Paging (-1 == All rows)
            if (pageSize != -1)
            {
                participants = participants.Skip(skip).Take(pageSize);
            }

            var data = await participants.ToListAsync();

            //Returning Json Data  
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> GetGroups(Guid? id)
        {
            // GET USER ORGANIZATIONS
            // Logged User OrganizationId
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

            var groups =
                _context.Groups.Include(g => g.Organizations)
                .Select(g => new Group
                {
                    GroupId = g.GroupId,
                    GroupName = g.Organizations!.OrganizationName + " - " + g.GroupName,
                    OrganizationId = g.OrganizationId
                });

            if (organization.IsOrganizationUnit != true)
            {
                groups = groups
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

                groups = groups
                    .Where(p => organizationIds
                    .Contains((Guid)p.OrganizationId));
            }

            // by Selected Organizations Array from jsTree
            string[] selectedOrganizations = Request.Form["selectedOrganizations[]"].ToArray();
            if (selectedOrganizations != null && selectedOrganizations.Length != 0)
            {
                groups = groups
                    .Where(p => selectedOrganizations.Select(Guid.Parse)
                    .Contains((Guid)p.OrganizationId));
            }

            return Json(
                await groups.Select(g => new
                {
                    g.GroupId,
                    g.GroupName
                }).ToListAsync()
                );
        }

        //[AcceptVerbs("Post")]
        public async Task<IActionResult> GetTLMDistributions(int? distributionPeriodId)
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

            // Get Distributions
            var tlmDistributions = _context.TLMDistributions
                .Include(t => t.TLMDistributionStatus)
                .Include(t => t.OrganizationsFrom)
                .Include(t => t.OrganizationsTo)
                .Include(t => t.ParentTLMDistributions)
                .Include(t => t.TLMDistributionPeriods)
                .Where(t => t.TLMDistributionPeriodId == distributionPeriodId)
                .Select(t => new
                {
                    t.TLMDistributionId,
                    t.TrackingCode,
                    OrganizationFrom = t.OrganizationsFrom!.OrganizationName,
                    OrganizationTo = t.OrganizationsTo!.OrganizationName,
                    ShippedDate = t.ShippedDate.HasValue ? t.ShippedDate.Value.ToShortDateString() : null,
                    ReceivedDate = t.ReceivedDate.HasValue ? t.ReceivedDate.Value.ToShortDateString() : null,
                    t.TLMDistributionStatus!.DistributionStatus,
                    t.Url
                });

            // Get Participants
            //if (organization.IsOrganizationUnit != true)
            //{
            //    participants = participants
            //        .Where(p => p.OrganizationId == organization.OrganizationId);
            //}
            //else
            //{
            //    //Get Organization Hierarchy
            //    var AllOrganizations = await _context.Organizations
            //    .Select(x => new
            //    {
            //        x.OrganizationId,
            //        x.OrganizationName,
            //        x.RefOrganizationTypeId,
            //        x.ParentOrganizationId,
            //        x.IsOrganizationUnit
            //    }).ToListAsync();

            //    var lookup = AllOrganizations.ToLookup(x => x.ParentOrganizationId);
            //    var childOrganizations = lookup[organization.OrganizationId].SelectRecursive(x => lookup[x.OrganizationId]).ToList();

            //    var organizationIds = childOrganizations
            //            .Select(x => x.OrganizationId)
            //                .ToList();

            //    participants = participants
            //        .Where(p => organizationIds
            //        .Contains((Guid)p.OrganizationId));
            //}

            // DATATABLES POST REQUEST
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
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10, 20, 50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;

            // CUSTOM FILTERS:

            //Sorting  
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                //OrderBy - requires Linq.Dynamic.Core
                tlmDistributions = tlmDistributions.OrderBy(sortColumn + " " + sortColumnDirection);
            }
            else
            {
                tlmDistributions = tlmDistributions.OrderBy(t => t.OrganizationFrom).ThenBy(t => t.OrganizationTo);
            }

            //Search  
            if (!string.IsNullOrEmpty(searchValue))
            {
                tlmDistributions = tlmDistributions
                    .Where(m =>
                        m.OrganizationFrom.ToLower().Contains(searchValue.ToLower()) ||
                        m.OrganizationTo.ToLower().Contains(searchValue.ToLower()) ||
                        m.TrackingCode!.ToLower().Contains(searchValue.ToLower())
                    );
            }

            //total number of rows counts   
            recordsTotal = tlmDistributions.Count();

            //Paging (-1 == All rows)
            if (pageSize != -1)
            {
                tlmDistributions = tlmDistributions.Skip(skip).Take(pageSize);
            }

            var data = await tlmDistributions.ToListAsync();

            //Returning Json Data  
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
        }
    }
}