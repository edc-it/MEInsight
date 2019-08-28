using MEL.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefStudentDisabilityType
    {
        public RefStudentDisabilityType()
        {
            this.Students = new HashSet<Student>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Disability Type Id")]
        [Column(Order = 0)]
        public int RefStudentDisabilityTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Disability Type Code")]
        [Column(Order = 1)]
        public string DisabilityTypeCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Disability Type")]
        [Column(Order = 2)]
        public string DisabilityType { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
