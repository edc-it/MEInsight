using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Core;

namespace MEInsight.Entities.Reference
{
    [Table("RefSchoolType")]
    public class RefSchoolType
    {
        public RefSchoolType()
        {
            Schools = new HashSet<School>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "School Type Id")]
        [Column(Order = 0)]
        public int RefSchoolTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "School Type Code")]
        [Column(Order = 1)]
        public string SchoolTypeCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "School Type")]
        [Column(Order = 2)]
        public string SchoolType { get; set; } = null!;

        public virtual ICollection<School> Schools { get; set; }
    }
}
