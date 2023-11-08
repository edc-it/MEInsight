using MEInsight.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefStudentSpecialization")]
    public class RefStudentSpecialization
    {
        public RefStudentSpecialization()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Specialization Id")]
        [Column(Order = 0)]
        public int RefStudentSpecializationId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Specialization Code")]
        [Column(Order = 1)]
        public string StudentSpecializationCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(50)]
        [Display(Name = "Specialization")]
        [Column(Order = 2)]
        public string StudentSpecialization { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}
