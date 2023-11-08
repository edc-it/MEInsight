using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Reference;
using MEInsight.Entities.Programs;
using Microsoft.EntityFrameworkCore;

namespace MEInsight.Entities.Core
{
    [Table("Teacher")]
    public class Teacher : Participant
    {
        public Teacher()
        {
            Groups = new HashSet<Group>();
        }

        [Display(Name = "Teacher Type")]
        [Column(Order = 1)]
        public int? RefTeacherTypeId { get; set; }

        [Display(Name = "Position")]
        [Column(Order = 2)]
        public int? RefTeacherPositionId { get; set; }
        
        [Display(Name = "Employment Type")]
        [Column(Order = 3)]
        public int? RefTeacherEmploymentTypeId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Grades taught?")]
        [Column(Order = 4)]
        public string? GradeLevels { get; set; }

        [ForeignKey("RefTeacherTypeId")]
        [Display(Name = "Teacher Type")]
        public virtual RefTeacherType? TeacherTypes { get; set; }

        [ForeignKey("RefTeacherPositionId")]
        [Display(Name = "Position")]
        public virtual RefTeacherPosition? TeacherPositions { get; set; }

        [ForeignKey("RefTeacherEmploymentTypeId")]
        [Display(Name = "Employment Type")]
        public virtual RefTeacherEmploymentType? TeacherEmploymentTypes { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
