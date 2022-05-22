using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Core;

namespace MEInsight.Entities.Reference
{
    [Table("RefTeacherType")]
    public class RefTeacherType
    {
        public RefTeacherType()
        {
            Teachers = new HashSet<Teacher>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Teacher Type Id")]
        [Column(Order = 0)]
        public int RefTeacherTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Teacher Type Code")]
        [Column(Order = 1)]
        public string TeacherTypeCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Teacher Type")]
        [Column(Order = 2)]
        public string TeacherType { get; set; } = null!;

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
