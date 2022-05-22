using MEInsight.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefEducationAdministratorOffice")]
    public class RefEducationAdministratorOffice
    {
        public RefEducationAdministratorOffice()
        {
            EducationAdministrators = new HashSet<EducationAdministrator>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Office Id")]
        [Column(Order = 0)]
        public int RefEducationAdministratorOfficeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Office Code")]
        [Column(Order = 1)]
        public string EducationAdministratorOfficeCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Office")]
        [Column(Order = 2)]
        public string EducationAdministratorOffice { get; set; } = null!;

        public virtual ICollection<EducationAdministrator> EducationAdministrators { get; set; }
    }
}
