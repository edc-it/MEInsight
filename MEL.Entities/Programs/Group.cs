using MEL.Entities.Core;
using MEL.Entities.Reference;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Programs
{
    public class Group : BaseEntity
    {
        public Group()
        {
            this.GroupEnrollments = new HashSet<GroupEnrollment>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Group")]
        [Column(Order = 0)]
        public Guid GroupId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Organization")]
        [Column(Order = 1)]
        public Guid OrganizationId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Program")]
        [Column(Order = 2)]
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(100)]
        [Display(Name = "Group Code")]
        [Remote(action: "VerifyGroupCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "GroupCodeInitialValue")]
        [Column(Order = 3)]
        public string GroupCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(255)]
        [Display(Name = "Group")]
        [Column(Order = 4)]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [Column(Order = 5)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Column(Order = 6)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Teacher/Facilitator")]
        [Column(Order = 7)]
        public Guid? ParticipantId { get; set; }

        [Display(Name = "Grade Level")]
        [Column(Order = 8)]
        public int? RefGradeLevelId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Completion Date")]
        [Column(Order = 9)]
        public DateTime? CompletionDate { get; set; }

        [Display(Name = "Closed")]
        [Column(Order = 10)]
        public bool? Closed { get; set; }

        [MaxLength(50)]
        [Display(Name = "Closed By")]
        [Column(Order = 11)]
        public string ClosedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Closed Date")]
        [Column(Order = 12)]
        public DateTime? ClosedDate { get; set; }

        [Display(Name = "Document")]
        [Column(Order = 13)]
        public string Url { get; set; }

        [Display(Name = "Filename")]
        [Column(Order = 14)]
        public string FileName { get; set; }

        //Navigation Properties
        [ForeignKey("OrganizationId")]
        [Display(Name = "Organization")]
        public virtual Organization Organizations { get; set; }

        [ForeignKey("ProgramId")]
        [Display(Name = "Program")]
        public virtual Program Programs { get; set; }
        
        [ForeignKey("ParticipantId")]
        [Display(Name = "Teacher/Facilitator")]
        public virtual Teacher Teachers { get; set; }

        [ForeignKey("RefGradeLevelId")]
        [Display(Name = "Grade Level")]
        public virtual RefGradeLevel GradeLevels { get; set; }

        public virtual ICollection<GroupEnrollment> GroupEnrollments { get; set; }
    }
}
