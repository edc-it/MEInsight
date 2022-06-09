using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MEInsight.Entities.Reference;

namespace MEInsight.Entities.Programs
{
    [Table("GroupEvaluation")]
    [Index("GroupEnrollmentId", Name = "IX_GroupEvaluation_GroupEnrollmentId")]
    [Index("ProgramAssessmentId", Name = "IX_GroupEvaluation_ProgramAssessmentId")]
    [Index("RefEvaluationStatusId", Name = "IX_GroupEvaluation_RefEvaluationStatusId")]
    public class GroupEvaluation : BaseEntity
    {
        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Evaluation Id")]
        [Column(Order = 0)]
        public Guid GroupEvaluationId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Enrollment")]
        [Column(Order = 1)]
        public Guid GroupEnrollmentId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Evaluation Date")]
        [Column(Order = 2)]
        public DateTime? EvaluationDate { get; set; }

        [Display(Name = "Program Assessment")]
        [Column(Order = 3)]
        public int? ProgramAssessmentId { get; set; }

        [Display(Name = "Score")]
        [Column(Order = 4)]
        public int? Score { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Status Date")]
        [Column(Order = 5)]
        public DateTime? StatusDate { get; set; }

        [Display(Name = "Status")]
        [Column(Order = 6)]
        public int? RefEvaluationStatusId { get; set; }

        // Navigation properties
        [ForeignKey("GroupEnrollmentId")]
        [InverseProperty("GroupEvaluations")]
        [Display(Name = "Enrollment")]
        public virtual GroupEnrollment GroupEnrollments { get; set; } = null!;

        [ForeignKey("ProgramAssessmentId")]
        [InverseProperty("GroupEvaluations")]
        [Display(Name = "Program Assessment")]
        public virtual ProgramAssessment? ProgramAssessments { get; set; }

        [ForeignKey("RefEvaluationStatusId")]
        [InverseProperty("GroupEvaluations")]
        [Display(Name = "Evaluation Status")]
        public virtual RefEvaluationStatus? EvaluationStatus { get; set; }
    }
}
