using MEInsight.Entities.Core;
using MEInsight.Entities.Identity;
using MEInsight.Entities.Programs;
using MEInsight.Entities.Reference;
using MEInsight.Entities.TLM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Web.Models
{
    public class OrganizationPartnerViewModel
    {
        public OrganizationPartnerViewModel()
        {
            this.Participants = new HashSet<Participant>();
            this.Schools = new HashSet<School>();
            this.Organizations = new HashSet<Organization>();
            this.Groups = new HashSet<Group>();
            this.Users = new HashSet<ApplicationUser>();
            
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
        [MaxLength(25)]
        [Display(Name = "Organization Code")]
        [Remote(action: "VerifyOrganizationCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "InitialValue")]
        [Column(Order = 2)]
        public string OrganizationCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(255)]
        [Display(Name = "Organization")]
        [Column(Order = 3)]
        public string OrganizationName { get; set; }

        // Foreign Key
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Organization Type")]
        [Column(Order = 4)]
        public int RefOrganizationTypeId { get; set; }

        // Foreign Key
        [Display(Name = "Parent Organization")]
        [Column(Order = 5)]
        public Guid? ParentOrganizationId { get; set; }

        [MaxLength(150)]
        [Column(Order = 6)]
        [Display(Name = "Contact")]
        public string Contact { get; set; }

        [MaxLength(20)]
        [Column(Order = 7)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Location")]
        [Column(Order = 8)]
        public string RefLocationId { get; set; }

        [MaxLength(384)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        [Column(Order = 9)]
        public string Address { get; set; }

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

        // Navigation properties
        [ForeignKey("ParentOrganizationId")]
        [Display(Name = "Parent Organization")]
        public virtual Organization ParentOrganization { get; set; }

        [ForeignKey("RefOrganizationTypeId")]
        [Display(Name = "Organization Type")]
        public virtual RefOrganizationType OrganizationTypes { get; set; }

        [ForeignKey("RefLocationId")]
        [Display(Name = "Location")]
        public virtual RefLocation Locations { get; set; }

        //Partner
        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(50)]
        [Display(Name = "Partner Code")]
        [Column(Order = 1)]
        [Remote(action: "VerifyPartnerCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "InitialValue")]
        public string PartnerCode { get; set; }

        [Display(Name = "Partner Type")]
        [Column(Order = 2)]
        public int? RefPartnerTypeId { get; set; }

        [Display(Name = "Partner Sector")]
        [Column(Order = 3)]
        public int? RefPartnerSectorId { get; set; }

        // Maps to Partner.Contact
        [MaxLength(150)]
        [Display(Name = "Contact")]
        [Column(Order = 4)]
        public string PartnerContact { get; set; }

        // Navigation properties
        [ForeignKey("RefPartnerTypeId")]
        [Display(Name = "Partner Type")]
        public virtual RefPartnerType PartnerTypes { get; set; }

        [ForeignKey("RefPartnerSectorId")]
        [Display(Name = "Partner Sector")]
        public virtual RefPartnerSector PartnerSectors { get; set; }
        public virtual ICollection<School> Schools { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        
    }
}
