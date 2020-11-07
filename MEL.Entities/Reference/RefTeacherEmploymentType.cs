using MEL.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace MEL.Entities.Reference
{
    public class RefTeacherEmploymentType
    {
        public RefTeacherEmploymentType()
        {
            this.Teachers = new HashSet<Teacher>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Employment Type Id")]
        [Column(Order = 0)]
        public int RefTeacherEmploymentTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Employment Type Code")]
        [Column(Order = 1)]
        public string TeacherEmploymentTypeCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Employment Type")]
        [Column(Order = 2)]
        public string TeacherEmploymentType { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
