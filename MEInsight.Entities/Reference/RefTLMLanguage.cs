using MEInsight.Entities.TLM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefTLMLanguage")]
    public class RefTLMLanguage
    {
        public RefTLMLanguage()
        {
            TLMMaterials = new HashSet<TLMMaterial>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "TLM Language Id")]
        [Column(Order = 0)]
        public int RefTLMLanguageId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "TLM Language Code")]
        [Column(Order = 1)]
        public string TLMLanguageCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "TLM Language")]
        [Column(Order = 2)]
        public string TLMLanguage { get; set; } = null!;

        public virtual ICollection<TLMMaterial> TLMMaterials { get; set; }
    }
}
