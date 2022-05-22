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
    [Index("RefTeacherEmploymentTypeId", Name = "IX_Teacher_RefTeacherEmploymentTypeId")]
    [Index("RefTeacherPositionId", Name = "IX_Teacher_RefTeacherPositionId")]
    [Index("RefTeacherTypeId", Name = "IX_Teacher_RefTeacherTypeId")]
    public class Teacher : Participant
    {
        public Teacher()
        {
            Groups = new HashSet<Group>();
        }

        //[Key]
        //[Required(ErrorMessage = "The {0} field is required.")]
        //[Display(Name = "Participant")]
        //[Column(Order = 0)]
        //public Guid ParticipantId { get; set; }

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

        ////Navigation properties
        //[ForeignKey("ParticipantId")]
        //public virtual Participant Participants { get; set; } = null!;

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
