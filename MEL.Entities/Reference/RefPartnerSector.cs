using MEL.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefPartnerSector
    {
        public RefPartnerSector()
        {
            this.Partners = new HashSet<Partner>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Sector Id")]
        [Column(Order = 0)]
        public int RefPartnerSectorId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Sector Code")]
        [Column(Order = 1)]
        public string PartnerSectorCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Sector")]
        [Column(Order = 2)]
        public string PartnerSector { get; set; }

        public virtual ICollection<Partner> Partners { get; set; }
    }
}
