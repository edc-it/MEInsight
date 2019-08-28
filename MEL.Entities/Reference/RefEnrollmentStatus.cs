using MEL.Entities.Programs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefEnrollmentStatus
    {
        public RefEnrollmentStatus()
        {
            this.GroupEnrollments = new HashSet<GroupEnrollment>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Enrollment Status Id")]
        [Column(Order = 0)]
        public int EnrollmentStatusId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Enrollment Status Code")]
        [Column(Order = 1)]
        public string EnrollmentStatusCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Enrollment Status")]
        [Column(Order = 2)]
        public string EnrollmentStatus { get; set; }

        public virtual ICollection<GroupEnrollment> GroupEnrollments { get; set; }
    }
}
