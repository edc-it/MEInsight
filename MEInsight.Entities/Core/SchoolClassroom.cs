using MEInsight.Entities.Reference;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Core
{
    [Table("SchoolClassroom")]
    [Index("OrganizationId", Name = "IX_SchoolClassroom_OrganizationId")]
    [Index("RefGradeLevelId", Name = "IX_SchoolClassroom_RefGradeLevelId")]
    public class SchoolClassroom : BaseEntity
    {
        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "School Classroom")]
        [Column(Order = 0)]
        public Guid SchoolClassroomId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        [Column(Order = 1)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Organization")]
        [Column(Order = 2)]
        public Guid OrganizationId { get; set; }

        [Display(Name = "Grade Level")]
        [Column(Order = 3)]
        public int RefGradeLevelId { get; set; }

        [Display(Name = "Classrooms")]
        [Column(Order = 4)]
        public int? Classrooms { get; set; }

        [Display(Name = "Classes")]
        [Column(Order = 5)]
        public int? Classes { get; set; }
        
        //FOREIGN KEYS
        [ForeignKey("OrganizationId")]
        [Display(Name = "School")]
        public virtual School Schools { get; set; } = null!;

        [ForeignKey("RefGradeLevelId")]
        [Display(Name = "Grade Level")]
        public virtual RefGradeLevel GradeLevels { get; set; } = null!;

    }
}
