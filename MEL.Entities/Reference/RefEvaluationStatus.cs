using MEL.Entities.Programs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefEvaluationStatus
    {
        public RefEvaluationStatus()
        {
            this.GroupEvaluations = new HashSet<GroupEvaluation>();
			this.ProgramAssessments = new HashSet<ProgramAssessment>();
		}

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Evaluation Status Id")]
        [Column(Order = 0)]
        public int EvaluationStatusId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Evaluation Status Code")]
        [Column(Order = 1)]
        public string EvaluationStatusCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Evaluation Status")]
        [Column(Order = 2)]
        public string EvaluationStatus { get; set; }

        public virtual ICollection<GroupEvaluation> GroupEvaluations { get; set; }
		public virtual ICollection<ProgramAssessment> ProgramAssessments { get; set; }
	}
}
