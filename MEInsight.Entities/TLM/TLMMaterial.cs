using MEInsight.Entities.Reference;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.TLM
{
    [Table("TLMMaterial")]
    public class TLMMaterial : BaseEntity
    {
        public TLMMaterial()
        {
            TLMDistributionDetails = new HashSet<TLMDistributionDetail>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "TLM Material")]
        [Column(Order = 0)]
        public int TLMMaterialId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(100)]
        [Display(Name = "TLM Material Code")]
        [Column(Order = 1)]
        [Remote(action: "VerifyTLMMaterialCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "TLMMaterialCodeInitialValue")]
        public string TLMMaterialCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "TLM Material Name")]
        [Column(Order = 2)]
        public string TLMMaterialName { get; set; } = null!;

        [MaxLength(384)]
        [Display(Name = "Description")]
        [Column(Order = 3)]
        public string? Description { get; set; }

        [Display(Name = "Type")]
        [Column(Order = 4)]
        public int? RefTLMMaterialTypeId { get; set; }

        [Display(Name = "Grade Level")]
        [Column(Order = 5)]
        public int? RefGradeLevelId { get; set; }

        [Display(Name = "Language")]
        [Column(Order = 6)]
        public int? RefTLMLanguageId { get; set; }

        [Display(Name = "Subject")]
        [Column(Order = 7)]
        public int? RefTLMSubjectId { get; set; }

        [Display(Name = "Group")]
        [Column(Order = 8)]
        public int? RefTLMGroupId { get; set; }

        [Display(Name = "Set")]
        [Column(Order = 9)]
        public int? RefTLMMaterialSetId { get; set; }

        [Column(Order = 10)]
        [Display(Name = "Ratio Numerator")]
        public int? RatioNumerator { get; set; }

        [Column(Order = 11)]
        [Display(Name = "Ratio Denominator")]
        public int? RatioDenominator { get; set; }

        [Display(Name = "Document")]
        [Column(Order = 12)]
        public string? Url { get; set; }

        [Display(Name = "Filename")]
        [Column(Order = 13)]
        public string? FileName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Barcode")]
        [Column(Order = 14)]
        public string? Barcode { get; set; }

        // Navigation properties
        [ForeignKey("RefTLMMaterialTypeId")]
        [Display(Name = "Type")]
        public virtual RefTLMMaterialType? TLMMaterialTypes { get; set; }

        [ForeignKey("RefGradeLevelId")]
        [Display(Name = "Grade Level")]
        public virtual RefGradeLevel? GradeLevels { get; set; }

        [ForeignKey("RefTLMLanguageId")]
        [Display(Name = "TLM Language")]
        public virtual RefTLMLanguage? TLMLanguages { get; set; }

        [ForeignKey("RefTLMSubjectId")]
        [Display(Name = "Subject")]
        public virtual RefTLMSubject? TLMSubjects { get; set; }

        [ForeignKey("RefTLMGroupId")]
        [Display(Name = "Group")]
        public virtual RefTLMGroup? TLMGroups { get; set; }

        [ForeignKey("RefTLMMaterialSetId")]
        [Display(Name = "Set")]
        public virtual RefTLMMaterialSet? TLMMaterialSets { get; set; }

        public virtual ICollection<TLMDistributionDetail> TLMDistributionDetails { get; set; }
    }
}
