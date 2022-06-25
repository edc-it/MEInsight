using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MEInsight.Entities.Reference;

namespace MEInsight.Entities.Core
{
    [Table("Partner")]
    public class Partner : Organization
    {

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100)]
        [Display(Name = "Partner Code")]
        [Column(Order = 1)]
        [Remote(action: "VerifyPartnerCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "PartnerCodeInitialValue")]
        public string PartnerCode { get; set; } = null!;

        [Display(Name = "Partner Type")]
        [Column(Order = 2)]
        public int? RefPartnerTypeId { get; set; }

        [Display(Name = "Partner Sector")]
        [Column(Order = 3)]
        public int? RefPartnerSectorId { get; set; }

        //[MaxLength(150)]
        //[Display(Name = "Contact")]
        //[Column(Order = 4)]
        //public string? Contact { get; set; }

        // Navigation properties
        //[ForeignKey("OrganizationId")]
        //[Display(Name = "Organization")]
        //public virtual Organization Organizations { get; set; } = null!;

        [ForeignKey("RefPartnerTypeId")]
        [Display(Name = "Partner Type")]
        public virtual RefPartnerType? PartnerTypes { get; set; }

        [ForeignKey("RefPartnerSectorId")]
        [Display(Name = "Partner Sector")]
        public virtual RefPartnerSector? PartnerSectors { get; set; }

    }
}
