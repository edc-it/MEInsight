using MEInsight.Entities.Core;
using MEInsight.Entities.Programs;
using MEInsight.Entities.Reference;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEInsight.Web.Models
{
    public class GroupEnrollmentsDto
    {
        [Key]
        [Display(Name = "Group Enrollment")]
        public Guid GroupEnrollmentId { get; set; }

        [Display(Name = "Group")]
        public Guid? GroupId { get; set; }

        [Display(Name = "Participant")]
        public Guid? ParticipantId { get; set; }

        [Display(Name = "First name")]
        public string? FirstName { get; set; } = null!;

        [Display(Name = "Middle name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Surname/Last name")]
        public string? LastName { get; set; } = null!;

        [Display(Name = "Name")]
        public string? Name { get; set; } = null!;

        [Display(Name = "Sex")]
        public int? RefSexId { get; set; }

        [Display(Name = "Sex")]
        public string? Sex { get; set; } = null!;

        [Display(Name = "Participant Code")]
        public string? ParticipantCode { get; set; } = null!;

        [Display(Name = "Total Attendance")]
        public string? Attendance { get; set; }

        [Display(Name = "Attendance Unit Id")]
        public int? RefAttendanceUnitId { get; set; }

        [Display(Name = "Attendance Unit")]
        public string? AttendanceUnit { get; set; } = null!;

        [Display(Name = "Status")]
        public int? RefEnrollmentStatusId { get; set; }

        [Display(Name = "Enrollment Status")]
        public string? EnrollmentStatus { get; set; } = null!;


    }
}
