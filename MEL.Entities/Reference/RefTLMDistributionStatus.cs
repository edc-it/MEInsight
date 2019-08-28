using MEL.Entities.TLM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefTLMDistributionStatus
    {
        public RefTLMDistributionStatus()
        {
            this.TLMDistributions = new HashSet<TLMDistribution>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Distribution Status Id")]
        [Column(Order = 0)]
        public int RefTLMDistributionStatusId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Distribution Status Code")]
        [Column(Order = 1)]
        public string TLMDistributionStatusCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(50)]
        [Display(Name = "Distribution Status")]
        [Column(Order = 2)]
        public string DistributionStatus { get; set; }

        public virtual ICollection<TLMDistribution> TLMDistributions { get; set; }
    }
}
