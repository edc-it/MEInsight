using MEInsight.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace MEInsight.Web.ViewComponents
{
    public class InfoPanelViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public InfoPanelViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(dynamic id, string controller, string view)
        {
            var items = await GetItemsAsync(id, controller, view);
            return View(items);
        }

        private async Task<InfoPanelViewModel?> GetItemsAsync(dynamic id, string? controller, string view)
        {

            if (id == null)
            {
                return null;
            }

            InfoPanelViewModel model = new();
            //int modelIntId;
            Guid modelGuidId;

            switch (controller)
            {
                case "Organizations":

                    //Convert dynamic to required type on query
                    modelGuidId = new Guid(id);

                    //Return model query
                    var organization = await _context.Organizations
                        .Where(x => x.OrganizationId == modelGuidId)
                        .FirstOrDefaultAsync();

                    //Get Parent Organization hierarchy
                    var organizationParents = await GetParentHierarchyAsync(modelGuidId);

                    //Get Parent Location hierarchy
                    var organizationLocations = await GetLocationHierarchyAsync(organization!.RefLocationId!);

                    model = new InfoPanelViewModel
                    {
                        CurrentLevel = 0,
                        CurrentController = "Organizations",
                        OrganizationId = organization.OrganizationId,
                        Organization = organization.OrganizationName,
                        OrganizationCode = organization.OrganizationCode,
                        IsOrganizationUnit = organization.IsOrganizationUnit,
                        IsTenant = organization.IsTenant,
                        OrganizationParent = (IEnumerable<OrganizationParent>)organizationParents,
                        LocationParent = (IEnumerable<LocationParent>)organizationLocations
                    };

                    break;

                case "Groups":

                    //Convert dynamic to required type on query
                    modelGuidId = new Guid(id);

                    //Return model query
                    var group = await _context.Organizations
                        .Where(x => x.OrganizationId == modelGuidId)
                        .FirstOrDefaultAsync();

                    //Get Parent Organization hierarchy
                    var groupParents = await GetParentHierarchyAsync(modelGuidId);

                    //Get Parent Location hierarchy
                    var groupLocations = await GetLocationHierarchyAsync(group!.RefLocationId!);

                    model = new InfoPanelViewModel
                    {
                        CurrentLevel = 1,
                        CurrentController = "Groups",
                        OrganizationId = group.OrganizationId,
                        Organization = group.OrganizationName,
                        OrganizationCode = group.OrganizationCode,
                        IsOrganizationUnit = group.IsOrganizationUnit,
                        OrganizationParent = (IEnumerable<OrganizationParent>)groupParents,
                        LocationParent = (IEnumerable<LocationParent>)groupLocations
                    };

                    break;

                case "GroupEnrollments":

                    //Convert dynamic to required type on query
                    modelGuidId = new Guid(id);

                    //Return model query
                    var groupEnrollment = await _context.Groups
                        .Include(x => x.Organizations)
                        .Include(x => x.Programs!).ThenInclude(x => x.AttendanceUnits)
                        .Include(x => x.Teachers!)
                        ////TODO ////.ThenInclude(x => x.Participants)
                        .ThenInclude(x => x.ParticipantTypes)
                        .Include(x => x.GradeLevels)
                        .Where(x => x.GroupId == modelGuidId)
                        .FirstOrDefaultAsync();

                    if (groupEnrollment != null)
                    {
                        //Get Parent Organization hierarchy
                        var groupEnrollmentParents = await GetParentHierarchyAsync(groupEnrollment.Organizations!.OrganizationId);

                        //Get Parent Location hierarchy
                        var groupEnrollmentLocations = await GetLocationHierarchyAsync(groupEnrollment.Organizations!.RefLocationId!);

                        model = new InfoPanelViewModel
                        {
                            CurrentLevel = 2,
                            CurrentController = "GroupEnrollments",
                            OrganizationId = groupEnrollment.Organizations.OrganizationId,
                            Organization = groupEnrollment.Organizations.OrganizationName,
                            OrganizationCode = groupEnrollment.Organizations.OrganizationCode,
                            IsOrganizationUnit = groupEnrollment.Organizations.IsOrganizationUnit,
                            OrganizationParent = (IEnumerable<OrganizationParent>)groupEnrollmentParents,
                            LocationParent = (IEnumerable<LocationParent>)groupEnrollmentLocations,
                            GroupFileName = groupEnrollment.FileName,
                            ProgramId = groupEnrollment.ProgramId,
                            Program = groupEnrollment.Programs!.ProgramName,
                            GroupId = groupEnrollment.GroupId,
                            Group = groupEnrollment.GroupName,
                            GroupClosed = groupEnrollment.Closed,
                            GroupClosedDate = groupEnrollment.ClosedDate,
                            GroupStartDate = groupEnrollment.StartDate,
                            GroupEndDate = groupEnrollment.EndDate,
                            Max = groupEnrollment.Programs.Max,
                            Min = groupEnrollment.Programs.Min,
                            AttendanceUnit = groupEnrollment.Programs.AttendanceUnits!.AttendanceUnit,
                            ////TODO ////GroupTeacher = groupEnrollment.Teachers?.Participants?.NameId ?? null,
                            ////TODO ////GroupTeacherType = groupEnrollment.Teachers?.Participants?.ParticipantTypes?.ParticipantType ?? null,
                            GradeLevel = groupEnrollment.GradeLevels?.GradeLevel ?? null
                        };
                        
                    }
                    break;

                default:
                    break;
            }

            return model;


        }

        #region Helpers

        private async Task<IEnumerable> GetParentHierarchyAsync(Guid modelGuidId)
        {
            // Get Organization Hierarchy
            // List ALL Administrator Organizations and include current OrganizationId
            var adminOrganizations = await _context.Organizations
                .Include(x => x.ParentOrganizations)
                .Where(x =>
                    x.IsOrganizationUnit == true ||
                    x.OrganizationId == modelGuidId
                    )
                .Select(x => new OrganizationParent
                {
                    Index = 1,
                    OrganizationId = x.OrganizationId,
                    OrganizationName = x.OrganizationName,
                    ParentOrganizationId = x.ParentOrganizationId,
                    Parent = x.ParentOrganizations!.OrganizationName,
                    IsOrganizationUnit = x.IsOrganizationUnit
                })
                .ToListAsync();

            //Get Organization Hierarchy of current OrganizationId
            var parents = ListParents(adminOrganizations, modelGuidId);

            //Instatiate for adding index to "parents" and Sort Descending
            IEnumerable<OrganizationParent> sortedParents = new List<OrganizationParent>();

            //Add index and Sort descending to "parents"
            sortedParents = parents
                .Select((x, index) => new OrganizationParent
                {
                    Index = index,
                    OrganizationId = x.OrganizationId,
                    OrganizationName = x.OrganizationName,
                    ParentOrganizationId = x.ParentOrganizationId,
                    Parent = x.Parent,
                    IsOrganizationUnit = x.IsOrganizationUnit
                })
                .OrderByDescending(x => x.Index)
                .ToList();

            return sortedParents.ToList();

        }

        public static IEnumerable<OrganizationParent> ListParents(IEnumerable<OrganizationParent> list, Guid? ID)
        {
            var current = list.Where(n => n.OrganizationId == ID).FirstOrDefault();
            if (current == null)
                return Enumerable.Empty<OrganizationParent>();
            return Enumerable.Concat(new[] { current }, ListParents(list, current.ParentOrganizationId));
        }

        private async Task<IEnumerable> GetLocationHierarchyAsync(string refLocationId)
        {
            var allLocations = await _context.Locations
                .Include(x => x.LocationTypes)
                .Include(x => x.ParentLocations)
                .Select(x => new LocationParent
                {
                    Index = 1,
                    RefLocationId = x.RefLocationId,
                    LocationName = x.LocationName,
                    ParentLocationId = x.ParentLocationId,
                    ParentName = x.ParentLocations!.LocationName,
                    RefLocationTypeId = x.RefLocationTypeId,
                    LocationType = x.LocationTypes.LocationType,
                    LocationLevel = x.LocationTypes.LocationLevel
                })
                .ToListAsync();

            var locations = ListLocations(allLocations, refLocationId);

            IEnumerable<LocationParent> sortedLocations = new List<LocationParent>();

            //Add index and Sort descending to "parents"
            sortedLocations = locations
                .Select((x, index) => new LocationParent
                {
                    Index = index,
                    RefLocationId = x.RefLocationId,
                    LocationName = x.LocationName,
                    ParentLocationId = x.ParentLocationId,
                    ParentName = x.ParentName,
                    RefLocationTypeId = x.RefLocationTypeId,
                    LocationType = x.LocationType,
                    LocationLevel = x.LocationLevel
                })
                .OrderByDescending(x => x.Index)
                .ToList();

            return sortedLocations.ToList();

        }

        public static IEnumerable<LocationParent> ListLocations(IEnumerable<LocationParent> list, string? id)
        {
            var current = list.Where(n => n.RefLocationId == id).FirstOrDefault();

            if (current == null) return Enumerable.Empty<LocationParent>();

            return Enumerable.Concat(new[] { current }, ListLocations(list, current.ParentLocationId));
        }

        #endregion
    }
}
