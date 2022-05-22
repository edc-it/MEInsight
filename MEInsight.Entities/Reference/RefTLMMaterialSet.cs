using MEInsight.Entities.TLM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefTLMMaterialSet")]
    public class RefTLMMaterialSet
    {
        public RefTLMMaterialSet()
        {
            TLMMaterials = new HashSet<TLMMaterial>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "TLM Material Set Id")]
        [Column(Order = 0)]
        public int RefTLMMaterialSetId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "TLM Material Set Code")]
        [Column(Order = 1)]
        public string TLMMaterialSetCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "TLM Material Set")]
        [Column(Order = 2)]
        public string TLMMaterialSet { get; set; } = null!;

        public virtual ICollection<TLMMaterial> TLMMaterials { get; set; }
    }
}
