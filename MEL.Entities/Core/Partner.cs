using MEL.Entities.Reference;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Core
{
    public class Partner : BaseEntity
    {
        public Partner()
        {
            this.Schools = new HashSet<School>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Organization")]
        [Column(Order = 0)]
        public Guid OrganizationId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(100)]
        [Display(Name = "Partner Code")]
        [Column(Order = 1)]
        [Remote(action: "VerifyPartnerCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "PartnerCodeInitialValue")]
        public string PartnerCode { get; set; }

        [Display(Name = "Partner Type")]
        [Column(Order = 2)]
        public int? RefPartnerTypeId { get; set; }

        [Display(Name = "Partner Sector")]
        [Column(Order = 3)]
        public int? RefPartnerSectorId { get; set; }

        [MaxLength(150)]
        [Display(Name = "Contact")]
        [Column(Order = 4)]
        public string Contact { get; set; }

        // Navigation properties
        [ForeignKey("OrganizationId")]
        [Display(Name = "Organization")]
        public virtual Organization Organizations { get; set; }

        [ForeignKey("RefPartnerTypeId")]
        [Display(Name = "Partner Type")]
        public virtual RefPartnerType PartnerTypes { get; set; }

        [ForeignKey("RefPartnerSectorId")]
        [Display(Name = "Partner Sector")]
        public virtual RefPartnerSector PartnerSectors { get; set; }

        public virtual ICollection<School> Schools { get; set; }

    }
}
