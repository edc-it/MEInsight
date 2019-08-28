using MEL.Entities.TLM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefTLMMaterialType
    {
        public RefTLMMaterialType()
        {
            this.TLMMaterials = new HashSet<TLMMaterial>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "TLM Material Type Id")]
        [Column(Order = 0)]
        public int RefTLMMaterialTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "TLM Material Type Code")]
        [Column(Order = 1)]
        public string TLMMaterialTypeCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "TLM Material Type")]
        [Column(Order = 2)]
        public string TLMMaterialType { get; set; }

        public virtual ICollection<TLMMaterial> TLMMaterials { get; set; }
    }
}
