using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Core;

namespace MEInsight.Entities.Reference
{
    [Table("RefEducationAdministratorType")]
    public class RefEducationAdministratorType
    {
        public RefEducationAdministratorType()
        {
            EducationAdministrators = new HashSet<EducationAdministrator>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Education Administrator Type Id")]
        [Column(Order = 0)]
        public int RefEducationAdministratorTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Education Administrator Type Code")]
        [Column(Order = 1)]
        public string EducationAdministratorTypeCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Education Administrator Type")]
        [Column(Order = 2)]
        public string EducationAdministratorType { get; set; } = null!;

        public virtual ICollection<EducationAdministrator> EducationAdministrators { get; set; }

    }
}
