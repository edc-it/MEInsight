using MEInsight.Entities.TLM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefTLMSubject")]
    public class RefTLMSubject
    {
        public RefTLMSubject()
        {
            TLMMaterials = new HashSet<TLMMaterial>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "TLM Subject Id")]
        [Column(Order = 0)]
        public int RefTLMSubjectId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "TLM Subject Code")]
        [Column(Order = 1)]
        public string TLMSubjectCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "TLM Subject")]
        [Column(Order = 2)]
        public string TLMSubject { get; set; } = null!;

        public virtual ICollection<TLMMaterial> TLMMaterials { get; set; }
    }
}
