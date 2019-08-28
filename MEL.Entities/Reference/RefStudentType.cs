using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Core;

namespace MEL.Entities.Reference
{
    public class RefStudentType
    {
        public RefStudentType()
        {
            this.Students = new HashSet<Student>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Student Type Id")]
        [Column(Order = 0)]
        public int RefStudentTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Student Type Code")]
        [Column(Order = 1)]
        public string StudentTypeCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Student Type")]
        [Column(Order = 2)]
        public string StudentType { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
