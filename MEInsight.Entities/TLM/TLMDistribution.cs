using MEInsight.Entities.Core;
using MEInsight.Entities.Reference;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.TLM
{
    [Table("TLMDistribution")]
    [Index("OrganizationIdFrom", Name = "IX_TLMDistribution_OrganizationIdFrom")]
    [Index("OrganizationIdTo", Name = "IX_TLMDistribution_OrganizationIdTo")]
    //[Index("ParentTlmdistributionId", Name = "IX_TLMDistribution_ParentTLMDistributionId")]
    //[Index("RefTlmdistributionStatusId", Name = "IX_TLMDistribution_RefTLMDistributionStatusId")]
    //[Index("TlmdistributionPeriodId", Name = "IX_TLMDistribution_TLMDistributionPeriodId")]
    public class TLMDistribution : BaseEntity
    {
        public TLMDistribution()
        {
            TLMDistributions = new HashSet<TLMDistribution>();
            TLMDistributionDetails = new HashSet<TLMDistributionDetail>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        [Display(Name = "TLM Distribution")]
        public Guid TLMDistributionId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Date)]
        [Column(Order = 1)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Distribution Period")]
        [Column(Order = 2)]
        public int TLMDistributionPeriodId { get; set; }

        [Display(Name = "Organization From")]
        [Column(Order = 3)]
        public Guid? OrganizationIdFrom { get; set; }

        [Display(Name = "Organization To")]
        [Column(Order = 4)]
        public Guid? OrganizationIdTo { get; set; }

        [DataType(DataType.Date)]
        [Column(Order = 5)]
        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }

        [DataType(DataType.Date)]
        [Column(Order = 6)]
        [Display(Name = "Received Date")]
        public DateTime? ReceivedDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Received By")]
        [Column(Order = 7)]
        public string? ReceivedBy { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(100)]
        [Display(Name = "Tracking Code")]
        [Remote(action: "VerifyTrackingCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "TrackingCodeInitialValue")]
        [Column(Order = 8)]
        public string? TrackingCode { get; set; } = null!;

        [Display(Name = "Distribution Status")]
        [Column(Order = 9)]
        public int? RefTLMDistributionStatusId { get; set; }

        [Display(Name = "Document")]
        [Column(Order = 10)]
        public string? Url { get; set; }

        [Display(Name = "Filename")]
        [Column(Order = 11)]
        public string? FileName { get; set; }

        [Display(Name = "Distribution Parent")]
        [Column(Order = 12)]
        public Guid? ParentTLMDistributionId { get; set; }

        //Navigation Properties
        [ForeignKey("OrganizationIdFrom")]
        [InverseProperty("TLMDistributionsFrom")]
        [Display(Name = "Organization From")]
        public virtual Organization? OrganizationsFrom { get; set; }

        [ForeignKey("OrganizationIdTo")]
        [InverseProperty("TLMDistributionsTo")]
        [Display(Name = "Organization To")]
        public virtual Organization? OrganizationsTo { get; set; }

        [ForeignKey("TLMDistributionPeriodId")]
        [Display(Name = "Distribution Period")]
        public virtual TLMDistributionPeriod TLMDistributionPeriods { get; set; } = null!;

        [ForeignKey("RefTLMDistributionStatusId")]
        [Display(Name = "Distribution Status")]
        public virtual RefTLMDistributionStatus? TLMDistributionStatus { get; set; }


        [ForeignKey("ParentTLMDistributionId")]
        [Display(Name = "Distribution Parent")]
        public virtual TLMDistribution? ParentTLMDistributions { get; set; }

        public virtual ICollection<TLMDistribution> TLMDistributions { get; set; }

        [Display(Name = "Distribution Details")]
        public virtual ICollection<TLMDistributionDetail> TLMDistributionDetails { get; set; }
    }
}
