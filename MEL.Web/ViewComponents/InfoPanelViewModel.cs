using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MEL.Web.ViewComponents
{
    public class InfoPanelViewModel
    {
        public int? CurrentLevel { get; set; }

        public string CurrentController { get; set; }

        [Display(Name = "Program")]
        public string Program { get; set; }

        [Display(Name = "Program Id")]
        public int? ProgramId { get; set; }

        [Display(Name = "Has Assessment?")]
        public bool? HasAssessment { get; set; }

        [Display(Name = "Duration")]
        public int? Max { get; set; }

        [Display(Name = "Min Attendance")]
        public int? Min { get; set; }

        [Display(Name = "Attendance Unit")]
        public string AttendanceUnit { get; set; }

        [Display(Name = "Filename")]
        public string GroupFileName { get; set; }

        [Display(Name = "Teacher/Facilitator")]
        public string GroupTeacher { get; set; }

        public string GroupTeacherType { get; set; }

        [Display(Name = "Closed")]
        public bool? GroupClosed { get; set; }

        [Display(Name = "Grade Level")]
        public string GradeLevel { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Closed Date")]
        public DateTime? GroupClosedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? GroupStartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? GroupEndDate { get; set; }

        [Display(Name = "Group Id")]
        public Guid? GroupId { get; set; }

        [Display(Name = "Group")]
        public string Group { get; set; }

        [Display(Name = "Parent Organization")]
        public Guid? ParentOrganizationId { get; set; }

        [Display(Name = "Parent Organization")]
        public string ParentOrganization { get; set; }

        [Display(Name = "Organization")]
        public Guid? OrganizationId { get; set; }

        [Display(Name = "Organization")]
        public string Organization { get; set; }

        [Display(Name = "Organization Code")]
        public string OrganizationCode { get; set; }

        [Display(Name = "Is Administrator?")]
        public bool? IsOrganizationUnit { get; set; }

        [Display(Name = "Is Tenant?")]
        public bool? IsTenant { get; set; }


        public IEnumerable<OrganizationParent> OrganizationParent { get; set; }
        public IEnumerable<LocationParent> LocationParent { get; set; }
    }

    // ViewModel for GetParentHierarchyAsync
    public class OrganizationParent
    {
        [Display(Name = "Index")]
        public int? Index { get; set; }

        [Display(Name = "Organization")]
        public Guid? OrganizationId { get; set; }

        [Display(Name = "Organization")]
        public string OrganizationName { get; set; }

        [Display(Name = "Parent Organization")]
        public Guid? ParentOrganizationId { get; set; }

        [Display(Name = "Parent")]
        public string Parent { get; set; }

        [Display(Name = "Is Administrator?")]
        public bool? IsOrganizationUnit { get; set; }
    }

    // ViewModel for GetLocationHierarchyAsync
    public class LocationParent
    {
        [Display(Name = "Index")]
        public int? Index { get; set; }

        [Display(Name = "Location")]
        public string RefLocationId { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Display(Name = "Parent Location")]
        public string ParentLocationId { get; set; }

        [Display(Name = "Parent")]
        public string ParentName { get; set; }

        [Display(Name = "Location Type")]
        public int? RefLocationTypeId { get; set; }

        [Display(Name = "Location Type")]
        public string LocationType { get; set; }

        [Display(Name = "Location Level")]
        public int LocationLevel { get; set; }
    }
}
