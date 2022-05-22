using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MEInsight.Entities.Reference;
using MEInsight.Entities.Programs;
using MEInsight.Entities.TLM;
using MEInsight.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MEInsight.Entities.Core
{
    [Table("Organization")]
    [Index("ParentOrganizationId", Name = "IX_Organization_ParentOrganizationId")]
    [Index("RefLocationId", Name = "IX_Organization_RefLocationId")]
    [Index("RefOrganizationTypeId", Name = "IX_Organization_RefOrganizationTypeId")]
    public class Organization : BaseEntity
    {

        public Organization()
        {
            Participants = new HashSet<Participant>();
            Organizations = new HashSet<Organization>();
            Groups = new HashSet<Group>();
            TLMDistributionsFrom = new HashSet<TLMDistribution>();
            TLMDistributionsTo = new HashSet<TLMDistribution>();
            Users = new HashSet<ApplicationUser>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Organization")]
        [Column(Order = 0)]
        public Guid OrganizationId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        [Column(Order = 1)]
        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(100)]
        [Display(Name = "Organization Code")]
        [Remote(action: "VerifyOrganizationCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "OrganizationCodeInitialValue")]
        [Column(Order = 2)]
        public string OrganizationCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(255)]
        [Display(Name = "Organization")]
        [Column(Order = 3)]
        public string OrganizationName { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Organization Type")]
        [Column(Order = 4)]
        public int RefOrganizationTypeId { get; set; }

        [Display(Name = "Parent Organization")]
        [Column(Order = 5)]
        public Guid? ParentOrganizationId { get; set; }

        [MaxLength(150)]
        [Column(Order = 6)]
        [Display(Name = "Contact")]
        public string? Contact { get; set; }

        [MaxLength(25)]
        [Column(Order = 7)]
        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [Display(Name = "Location")]
        [Column(Order = 8)]
        public string? RefLocationId { get; set; }

        [MaxLength(384)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        [Column(Order = 9)]
        public string? Address { get; set; }

        [Display(Name = "Latitude")]
        [Column(Order = 10)]
        public double? Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Column(Order = 11)]
        public double? Longitude { get; set; }

        [Display(Name = "Is Organization Unit?")]
        [Column(Order = 12)]
        public bool IsOrganizationUnit { get; set; } = false;

        [ScaffoldColumn(false)]
        [Display(Name = "Is Tenant?")]
        [Column(Order = 13)]
        public bool? IsTenant { get; set; }

        [ForeignKey("ParentOrganizationId")]
        [Display(Name = "Parent Organization")]
        public virtual Organization? ParentOrganization { get; set; }

        [ForeignKey("RefOrganizationTypeId")]
        [Display(Name = "Organization Type")]
        public virtual RefOrganizationType? OrganizationTypes { get; set; } = null!;

        [ForeignKey("RefLocationId")]
        [Display(Name = "Location")]
        public virtual RefLocation? Locations { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

        [InverseProperty("OrganizationsFrom")]
        public virtual ICollection<TLMDistribution> TLMDistributionsFrom { get; set; }
        
        [InverseProperty("OrganizationsTo")]
        public virtual ICollection<TLMDistribution> TLMDistributionsTo { get; set; }

    }
}
