using MEL.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefSchoolLanguage
    {
        public RefSchoolLanguage()
        {
            this.Schools = new HashSet<School>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "School Language Id")]
        [Column(Order = 0)]
        public int RefSchoolLanguageId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "School Language Code")]
        [Column(Order = 1)]
        public string SchoolLanguageCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "School Language")]
        [Column(Order = 2)]
        public string SchoolLanguage { get; set; }

        public virtual ICollection<School> Schools { get; set; }
    }
}
