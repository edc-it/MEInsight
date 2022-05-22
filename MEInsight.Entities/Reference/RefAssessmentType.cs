﻿using MEInsight.Entities.Programs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefAssessmentType")]
    public class RefAssessmentType
    {
        public RefAssessmentType()
        {
            ProgramAssessments = new HashSet<ProgramAssessment>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Assessment Type Id")]
        [Column(Order = 0)]
        public int RefAssessmentTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Assessment Type Code")]
        [Column(Order = 1)]
        public string AssessmentTypeCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Assessment Type")]
        [Column(Order = 2)]
        public string AssessmentType { get; set; } = null!;

        public virtual ICollection<ProgramAssessment> ProgramAssessments { get; set; }
    }
}
