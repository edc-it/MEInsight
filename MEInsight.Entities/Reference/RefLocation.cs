using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MEInsight.Entities.Reference
{
    [Table("RefLocation")]
    public class RefLocation
    {
        public RefLocation()
        {
            Locations = new HashSet<RefLocation>();
            Organizations = new HashSet<Organization>();
            Participants = new HashSet<Participant>();
            SchoolClusters = new HashSet<RefSchoolCluster>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Location Id")]
        [Remote(action: "VerifyLocationCode", areaName: "", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "LocationCodeInitialValue")]
        [Column(Order = 0)]
        public string RefLocationId { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(255)]
        [Display(Name = "Location")]
        [Column(Order = 1)]
        public string? LocationName { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Location Type")]
        [Column(Order = 2)]
        public int RefLocationTypeId { get; set; }
        
        [Display(Name = "Parent Location")]
        [Column(Order = 3)]
        public string? ParentLocationId { get; set; }

        [Display(Name = "Latitude")]
        [Column(Order = 4)]
        public double? Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Column(Order = 5)]
        public double? Longitude { get; set; }

        // Navigation properties
        [ForeignKey("RefLocationTypeId")]
        [Display(Name = "Location Type")]
        public virtual RefLocationType? LocationTypes { get; set; } = null!;

        [ForeignKey("ParentLocationId")]
        [Display(Name = "Parent Location")]
        public virtual RefLocation? ParentLocations { get; set; }

        //TODO
        // Add LocationReference

        public virtual ICollection<RefLocation> Locations { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<RefSchoolCluster> SchoolClusters { get; set; }
    }
}
