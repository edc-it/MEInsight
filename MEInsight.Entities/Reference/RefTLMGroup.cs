using MEInsight.Entities.TLM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefTLMGroup")]
    public class RefTLMGroup
    {
        public RefTLMGroup()
        {
            TLMMaterials = new HashSet<TLMMaterial>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "TLM Group Id")]
        [Column(Order = 0)]
        public int RefTLMGroupId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "TLM Group Code")]
        [Column(Order = 1)]
        public string TLMGroupCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "TLM Group")]
        [Column(Order = 2)]
        public string TLMGroup { get; set; } = null!;

        public virtual ICollection<TLMMaterial> TLMMaterials { get; set; }
    }
}
