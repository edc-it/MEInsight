using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.TLM
{
    [Table("TLMDistributionPeriod")]
    public class TLMDistributionPeriod : BaseEntity
    {
        public TLMDistributionPeriod()
        {
            this.TLMDistributions = new HashSet<TLMDistribution>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Distribution Period")]
        [Column(Order = 0)]
        public int TLMDistributionPeriodId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Period name")]
        [Column(Order = 1)]
        public string PeriodName { get; set; } = null!;

        [DataType(DataType.Date)]
        [Column(Order = 2)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Column(Order = 3)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Closed")]
        [Column(Order = 4)]
        public bool? Closed { get; set; }

        [MaxLength(50)]
        [Display(Name = "Closed By")]
        [Column(Order = 5)]
        public string? ClosedBy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Closed Date")]
        [Column(Order = 6)]
        public DateTime? ClosedDate { get; set; }

        // Navigation properties
        public virtual ICollection<TLMDistribution> TLMDistributions { get; set; }
    }
}
