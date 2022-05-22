using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Reference;
using Microsoft.EntityFrameworkCore;

namespace MEInsight.Entities.Programs
{
    [Table("Program")]
    [Index("RefAttendanceUnitId", Name = "IX_Program_RefAttendanceUnitId")]
    [Index("RefOrganizationTypeId", Name = "IX_Program_RefOrganizationTypeId")]
    [Index("RefProgramDeliveryTypeId", Name = "IX_Program_RefProgramDeliveryTypeId")]
    [Index("RefProgramTypeId", Name = "IX_Program_RefProgramTypeId")]
    public class Program : BaseEntity
    {
        public Program()
        {
            Groups = new HashSet<Group>();
            ProgramAssessments = new HashSet<ProgramAssessment>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Program")]
        [Column(Order = 0)]
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Program")]
        [Column(Order = 1)]
        public string ProgramName { get; set; } = null!;

        //[Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Program Type")]
        [Column(Order = 2)]
        public int? RefProgramTypeId { get; set; }

        [Display(Name = "Program Delivery Type")]
        [Column(Order = 3)]
        public int? RefProgramDeliveryTypeId { get; set; }

        [MaxLength(255)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [Column(Order = 4)]
        public string? Description { get; set; }
        
        [Display(Name = "Duration")]
        [Column(Order = 5)]
        public int? Max { get; set; }

        [Display(Name = "Min Attendance")]
        [Column(Order = 6)]
        public int? Min { get; set; }

        //[Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Attendance unit")]
        [Column(Order = 7)]
        public int? RefAttendanceUnitId { get; set; }

        [Display(Name = "Has Assessment?")]
        [Column(Order = 8)]
        public bool HasAssessment { get; set; } = false;

        [Display(Name = "Display as Marks?")]
        [Column(Order = 9)]
        public bool DisplayMarks { get; set; } = false;

        [Display(Name = "Organization Type")]
        [Column(Order = 10)]
        public int? RefOrganizationTypeId { get; set; }

        // Navigation properties
        [ForeignKey("RefProgramTypeId")]
        [Display(Name = "Program Type")]
        public virtual RefProgramType? ProgramTypes { get; set; }

        [ForeignKey("RefProgramDeliveryTypeId")]
        [Display(Name = "Program Delivery Type")]
        public virtual RefProgramDeliveryType? ProgramDeliveryTypes { get; set; }

        [ForeignKey("RefAttendanceUnitId")]
        [Display(Name = "Attendance unit")]
        public virtual RefAttendanceUnit? AttendanceUnits { get; set; }

        [ForeignKey("RefOrganizationTypeId")]
        [Display(Name = "Organization Type")]
        public virtual RefOrganizationType? OrganizationTypes { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<ProgramAssessment> ProgramAssessments { get; set; }
    }
}
