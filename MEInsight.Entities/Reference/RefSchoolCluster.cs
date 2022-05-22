using MEInsight.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefSchoolCluster")]
    [Index("RefLocationId", Name = "IX_RefSchoolCluster_RefLocationId")]
    public class RefSchoolCluster
    {
        public RefSchoolCluster()
        {
            Schools = new HashSet<School>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "School Cluster Id")]
        [Column(Order = 0)]
        public int RefSchoolClusterId { get; set; }

        [MaxLength(25)]
        [Display(Name = "School Cluster Code")]
        [Column(Order = 1)]
        public string? SchoolClusterCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "School Cluster")]
        [Column(Order = 2)]
        public string SchoolCluster { get; set; } = null!;

        [Display(Name = "Location")]
        [Column(Order = 3)]
        public string? RefLocationId { get; set; }

        [ForeignKey("RefLocationId")]
        [Display(Name = "Location")]
        public virtual RefLocation? Locations { get; set; }

        public virtual ICollection<School> Schools { get; set; }
    }
}
