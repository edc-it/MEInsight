using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Core;

namespace MEL.Entities.Reference
{
    public class RefSchoolAdministrationType
    {
        public RefSchoolAdministrationType()
        {
            this.Schools = new HashSet<School>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Administration Type Id")]
        [Column(Order = 0)]
        public int RefSchoolAdministrationTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Administration Type Code")]
        [Column(Order = 1)]
        public string SchoolAdministrationTypeCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Administration Type")]
        [Column(Order = 2)]
        public string SchoolAdministrationType { get; set; }

        public virtual ICollection<School> Schools { get; set; }
    }
}
