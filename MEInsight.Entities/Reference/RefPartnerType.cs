using MEInsight.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace MEInsight.Entities.Reference
{
    [Table("RefPartnerType")]
    public class RefPartnerType
    {
        public RefPartnerType()
        {
            Partners = new HashSet<Partner>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Partner Type Id")]
        [Column(Order = 0)]
        public int RefPartnerTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Partner Type Code")]
        [Column(Order = 1)]
        public string PartnerTypeCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Partner Type")]
        [Column(Order = 2)]
        public string PartnerType { get; set; } = null!;

        public virtual ICollection<Partner> Partners { get; set; }
    }
}
