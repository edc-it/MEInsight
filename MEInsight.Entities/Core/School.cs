using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Reference;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MEInsight.Entities.Core
{
    [Table("School")]
    [Index("RefSchoolAdministrationTypeId", Name = "IX_School_RefSchoolAdministrationTypeId")]
    [Index("RefSchoolClusterId", Name = "IX_School_RefSchoolClusterId")]
    [Index("RefSchoolLanguageId", Name = "IX_School_RefSchoolLanguageId")]
    [Index("RefSchoolLocationId", Name = "IX_School_RefSchoolLocationId")]
    [Index("RefSchoolStatusId", Name = "IX_School_RefSchoolStatusId")]
    [Index("RefSchoolTypeId", Name = "IX_School_RefSchoolTypeId")]
    public class School : Organization
    {
        public School()
        {
            SchoolEnrollments = new HashSet<SchoolEnrollment>();
            SchoolClassrooms = new HashSet<SchoolClassroom>();
        }

        [StringLength(100)]
        [Display(Name = "School Code")]
        [Column(Order = 1)]
        [Remote(action: "VerifySchoolCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "SchoolCodeInitialValue")]
        public string? SchoolCode { get; set; }
        
        [Display(Name = "School Type")]
        [Column(Order = 2)]
        public int? RefSchoolTypeId { get; set; }

        [Display(Name = "School Location")]
        [Column(Order = 3)]
        public int? RefSchoolLocationId { get; set; }

        [Display(Name = "School Language")]
        [Column(Order = 4)]
        public int? RefSchoolLanguageId { get; set; }

        [Display(Name = "School Administration Type")]
        [Column(Order = 5)]
        public int? RefSchoolAdministrationTypeId { get; set; }

        [Display(Name = "Partner")]
        [Column(Order = 6)]
        public Guid? PartnerId { get; set; }

        [Display(Name = "Is Cluster Center?")]
        [Column(Order = 7)]
        public bool? IsClusterCenter { get; set; }

        [Display(Name = "School Cluster")]
        [Column(Order = 8)]
        public int? RefSchoolClusterId { get; set; }

        [Display(Name = "School Status")]
        [Column(Order = 9)]
        public int? RefSchoolStatusId { get; set; }

        [StringLength(150)]
        [Display(Name = "HeadTeacher")]
        [Column(Order = 10)]
        public string? HeadTeacher { get; set; }

        [ForeignKey("RefSchoolTypeId")]
        [Display(Name = "School Type")]
        public virtual RefSchoolType? SchoolTypes { get; set; }

        [ForeignKey("RefSchoolLocationId")]
        [Display(Name = "School Location")]
        public virtual RefSchoolLocation? SchoolLocations { get; set; }

        [ForeignKey("RefSchoolLanguageId")]
        [Display(Name = "School Language")]
        public virtual RefSchoolLanguage? SchoolLanguages { get; set; }

        [ForeignKey("RefSchoolAdministrationTypeId")]
        [Display(Name = "School Administration Type")]
        public virtual RefSchoolAdministrationType? SchoolAdministrationTypes { get; set; }

        [ForeignKey("RefSchoolClusterId")]
        [Display(Name = "School Cluster")]
        public virtual RefSchoolCluster? SchoolClusters { get; set; }

        //[ForeignKey("PartnerId")]
        //[InverseProperty("Schools")]
        //[Display(Name = "Partner")]
        //public virtual Partner? Partners { get; set; }

        [ForeignKey("RefSchoolStatusId")]
        [Display(Name = "Status")]
        public virtual RefSchoolStatus? SchoolStatus { get; set; }

        public virtual ICollection<SchoolEnrollment> SchoolEnrollments { get; set; }
        
        public virtual ICollection<SchoolClassroom> SchoolClassrooms { get; set; }

    }
}
