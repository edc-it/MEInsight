using MEL.Entities.Core;
using MEL.Entities.TLM;
using MEL.Entities.Programs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefGradeLevel
    {
        public RefGradeLevel()
        {
            this.SchoolEnrollments = new HashSet<SchoolEnrollment>();
            this.TLMMaterials = new HashSet<TLMMaterial>();
            this.Groups = new HashSet<Group>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Grade Level Id")]
        [Column(Order = 0)]
        public int RefGradeLevelId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Grade Level Code")]
        [Column(Order = 1)]
        public string GradeLevelCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Grade Level")]
        [Column(Order = 2)]
        public string GradeLevel { get; set; }

        [MaxLength(10)]
        [Display(Name = "Grade Id")]
        [Column(Order = 3)]
        public string GradeLevelId { get; set; }

        public virtual ICollection<SchoolEnrollment> SchoolEnrollments { get; set; }
        public virtual ICollection<TLMMaterial> TLMMaterials { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
