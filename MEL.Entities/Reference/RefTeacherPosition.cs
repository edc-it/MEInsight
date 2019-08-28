using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Core;

namespace MEL.Entities.Reference
{
    public class RefTeacherPosition
    {
        public RefTeacherPosition()
        {
            this.Teachers = new HashSet<Teacher>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Position Id")]
        [Column(Order = 0)]
        public int RefTeacherPositionId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Position Code")]
        [Column(Order = 1)]
        public string TeacherPositionCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Position")]
        [Column(Order = 2)]
        public string TeacherPosition { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
