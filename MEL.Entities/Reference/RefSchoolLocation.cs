using MEL.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefSchoolLocation
    {
        public RefSchoolLocation()
        {
            this.Schools = new HashSet<School>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "School Location Id")]
        [Column(Order = 0)]
        public int RefSchoolLocationId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "School Location Code")]
        [Column(Order = 1)]
        public string SchoolLocationCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "School Location")]
        [Column(Order = 2)]
        public string SchoolLocation { get; set; }

        public virtual ICollection<School> Schools { get; set; }
    }
}
