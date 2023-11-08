using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Core;

namespace MEInsight.Entities.Reference
{
    [Table("RefSchoolStatus")]
    public class RefSchoolStatus
    {
        public RefSchoolStatus()
        {
            Schools = new HashSet<School>();
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
        public string SchoolStatusCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "School Status")]
        [Column(Order = 2)]
        public string SchoolStatus { get; set; } = null!;

        public virtual ICollection<School> Schools { get; set; }
    }
}
