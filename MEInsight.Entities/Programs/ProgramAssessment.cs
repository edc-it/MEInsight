using MEInsight.Entities.Reference;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Programs
{
    [Table("ProgramAssessment")]
    public class ProgramAssessment : BaseEntity
    {
        public ProgramAssessment()
        {
            GroupEvaluations = new HashSet<GroupEvaluation>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Program Assessment Id")]
        [Column(Order = 0)]
        public int ProgramAssessmentId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Program")]
        [Column(Order = 1)]
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Assessment")]
        [Column(Order = 2)]
        public string AssessmentName { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Assessment Type")]
        [Column(Order = 3)]
        public int? RefAssessmentTypeId { get; set; }

        [MaxLength(255)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [Column(Order = 4)]
        public string? Description { get; set; }

        [Display(Name = "Track Attendance")]
        [Column(Order = 5)]
        public bool TrackAttendance { get; set; }

        [Display(Name = "Duration")]
        [Column(Order = 6)]
        public int? Max { get; set; }

        [Display(Name = "Min Attendance")]
        [Column(Order = 7)]
        public int? Min { get; set; }
        
        [Display(Name = "Attendance unit")]
        [Column(Order = 8)]
        public int? RefAttendanceUnitId { get; set; }

        [Display(Name = "Maximum Score")]
        [Column(Order = 9)]
        public int? MaximumScore { get; set; }

        [Display(Name = "Minimum Score")]
        [Column(Order = 10)]
        public int? MinimumScore { get; set; }

        [Display(Name = "Completion Score")]
        [Column(Order = 11)]
        public int? CompletionScore { get; set; }

        [Display(Name = "Completion Status")]
        [Column(Order = 12)]
        public int? RefEvaluationStatusId { get; set; }

        // Navigation properties
        [ForeignKey("ProgramId")]
        [Display(Name = "Program")]
        public virtual Program Programs { get; set; } = null!;

        [ForeignKey("RefAssessmentTypeId")]
        [Display(Name = "Assessment Type")]
        public virtual RefAssessmentType AssessmentTypes { get; set; } = null!;

        [ForeignKey("RefAttendanceUnitId")]
        [Display(Name = "Attendance unit")]
        public virtual RefAttendanceUnit? AttendanceUnits { get; set; }

        [ForeignKey("RefEvaluationStatusId")]
        [Display(Name = "Completion Status")]
        public virtual RefEvaluationStatus? EvaluationStatus { get; set; }
        
        public virtual ICollection<GroupEvaluation> GroupEvaluations { get; set; }

    }
}
