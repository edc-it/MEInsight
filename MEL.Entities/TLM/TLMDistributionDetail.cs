using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.TLM
{
    public class TLMDistributionDetail : BaseEntity
    {
        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        [Display(Name = "TLM Distribution Detail")]
        public Guid TLMDistributionDetailId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Date)]
        [Column(Order = 1)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "TLM Distribution")]
        [Column(Order = 2)]
        public Guid? TLMDistributionId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "TLM Material")]
        [Column(Order = 3)]
        public int TLMMaterialId { get; set; }

        [Display(Name = "Quantity Shipped")]
        [Column(Order = 4)]
        public int QuantityShipped { get; set; }

        [Display(Name = "Quantity Received")]
        [Column(Order = 5)]
        public int? QuantityReceived { get; set; }

        [MaxLength(255)]
        [Display(Name = "Comment")]
        [Column(Order = 6)]
        public string Comment { get; set; }

        // Navigation properties
        [ForeignKey("TLMDistributionId")]
        [Display(Name = "TLM Distribution")]
        public virtual TLMDistribution TLMDistributions { get; set; }

        [ForeignKey("TLMMaterialId")]
        [Display(Name = "TLM Material")]
        public virtual TLMMaterial TLMMaterials { get; set; }

    }
}
