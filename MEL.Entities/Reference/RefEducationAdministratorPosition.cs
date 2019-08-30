using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Core;

namespace MEL.Entities.Reference
{
    public class RefEducationAdministratorPosition
    {
        public RefEducationAdministratorPosition()
        {
            this.EducationAdministrators = new HashSet<EducationAdministrator>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Position Id")]
        [Column(Order = 0)]
        public int RefEducationAdministratorPositionId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Position Code")]
        [Column(Order = 1)]
        public string EducationAdministratorPositionCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Position")]
        [Column(Order = 2)]
        public string EducationAdministratorPosition { get; set; }

        public virtual ICollection<EducationAdministrator> EducationAdministrators { get; set; }
    }
}
