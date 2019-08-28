using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Reference;
using Microsoft.AspNetCore.Mvc;

namespace MEL.Entities.Core
{
    public class School : BaseEntity
    {
        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Organization")]
        [Column(Order = 0)]
        public Guid OrganizationId { get; set; }

        [MaxLength(100)]
        [Display(Name = "School Code")]
        [Column(Order = 1)]
        [Remote(action: "VerifySchoolCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "SchoolCodeInitialValue")]
        public string SchoolCode { get; set; }
        
        [Display(Name = "School Type")]
        [Column(Order = 2)]
        public int? RefSchoolTypeId { get; set; }

        [Display(Name = "School Location")]
        [Column(Order = 3)]
        public int? RefSchoolLocationId { get; set; }

        [Display(Name = "School Administration Type")]
        [Column(Order = 4)]
        public int? RefSchoolAdministrationTypeId { get; set; }

        [Display(Name = "Partner")]
        [Column(Order = 5)]
        public Guid? PartnerId { get; set; }

        [Display(Name = "Is Cluster Center?")]
        [Column(Order = 6)]
        public bool? IsClusterCenter { get; set; }

        [Display(Name = "School Cluster")]
        [Column(Order = 7)]
        public int? RefSchoolClusterId { get; set; }

        [Display(Name = "School Status")]
        [Column(Order = 8)]
        public int? RefSchoolStatusId { get; set; }

        [MaxLength(150)]
        [Display(Name = "HeadTeacher")]
        [Column(Order = 9)]
        public string HeadTeacher { get; set; }

        // Navigation properties
        [ForeignKey("OrganizationId")]
        [Display(Name = "Organization")]
        public virtual Organization Organizations { get; set; }
        
        [ForeignKey("RefSchoolTypeId")]
        [Display(Name = "School Type")]
        public virtual RefSchoolType SchoolTypes { get; set; }

        [ForeignKey("RefSchoolLocationId")]
        [Display(Name = "School Location")]
        public virtual RefSchoolLocation SchoolLocations { get; set; }

        [ForeignKey("RefSchoolAdministrationTypeId")]
        [Display(Name = "School Administration Type")]
        public virtual RefSchoolAdministrationType SchoolAdministrationTypes { get; set; }

        [ForeignKey("RefSchoolClusterId")]
        [Display(Name = "School Cluster")]
        public virtual RefSchoolCluster SchoolClusters { get; set; }

        [ForeignKey("PartnerId")]
        [Display(Name = "Partner")]
        public virtual Partner Partners { get; set; }

        [ForeignKey("RefSchoolStatusId")]
        [Display(Name = "Status")]
        public virtual RefSchoolStatus SchoolStatus { get; set; }

    }
}
