using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Core;

namespace MEL.Entities.Reference
{
    public class RefSchoolStatus
    {
        public RefSchoolStatus()
        {
            this.Schools = new HashSet<School>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "School Status Id")]
        [Column(Order = 0)]
        public int RefSchoolStatusId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "School Status Code")]
        [Column(Order = 1)]
        public string SchoolStatusCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "School Status")]
        [Column(Order = 2)]
        public string SchoolStatus { get; set; }

        public virtual ICollection<School> Schools { get; set; }
    }
}
