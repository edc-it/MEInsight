using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Core;
using MEL.Entities.Reference;

namespace MEL.Entities.Programs
{
    public class GroupEnrollment : BaseEntity
    {
        public GroupEnrollment()
        {
            this.GroupEvaluations = new HashSet<GroupEvaluation>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Group Enrollment")]
        [Column(Order = 1)]
        public Guid GroupEnrollmentId { get; set; }

        [Display(Name = "Group")]
        [Column(Order = 2)]
        public Guid? GroupId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Participant")]
        [Column(Order = 3)]
        public Guid ParticipantId { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        [Column(Order = 5)]
        public DateTime? EnrollmentDate { get; set; }

        [Display(Name = "Total Attendance")]
        [Column(Order = 6)]
        public int? Attendance { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Status Date")]
        [Column(Order = 8)]
        public DateTime? StatusDate { get; set; }

        [Display(Name = "Status")]
        [Column(Order = 9)]
        public int? RefEnrollmentStatusId { get; set; }
        
        [ForeignKey("ParticipantId")]
        [Display(Name = "Participant")]
        public virtual Participant Participants { get; set; }

        [ForeignKey("GroupId")]
        [Display(Name = "Group")]
        public virtual Group Groups { get; set; }

        [ForeignKey("RefEnrollmentStatusId")]
        [Display(Name = "Enrollment Status")]
        public virtual RefEnrollmentStatus EnrollmentStatus { get; set; }

        public virtual ICollection<GroupEvaluation> GroupEvaluations { get; set; }
    }
}
