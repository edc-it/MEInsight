using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Reference;
using MEL.Entities.Programs;

namespace MEL.Entities.Core
{
    public class Teacher : BaseEntity
    {
        public Teacher()
        {
            this.Groups = new HashSet<Group>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Participant")]
        [Column(Order = 0)]
        public Guid ParticipantId { get; set; }

        [Display(Name = "Teacher Type")]
        [Column(Order = 1)]
        public int? RefTeacherTypeId { get; set; }

        [Display(Name = "Position")]
        [Column(Order = 2)]
        public int? RefTeacherPositionId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Grades taught?")]
        [Column(Order = 3)]
        public string GradeLevels { get; set; }

        //Navigation properties
        [ForeignKey("ParticipantId")]
        public virtual Participant Participants { get; set; }

        [ForeignKey("RefTeacherTypeId")]
        [Display(Name = "Teacher Type")]
        public virtual RefTeacherType TeacherTypes { get; set; }

        [ForeignKey("RefTeacherPositionId")]
        [Display(Name = "Position")]
        public virtual RefTeacherPosition TeacherPositions { get; set; }
        
        public virtual ICollection<Group> Groups { get; set; }
    }
}
