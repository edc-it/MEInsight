using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Core;
using MEInsight.Entities.Programs;

namespace MEInsight.Entities.Reference
{
    [Table("RefOrganizationType")]
    public class RefOrganizationType
    {
        public RefOrganizationType()
        {
            Organizations = new HashSet<Organization>();
            Programs = new HashSet<Program>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Organization Type Id")]
        [Column(Order = 0)]
        public int RefOrganizationTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Organization Type")]
        [Column(Order = 1)]
        public string OrganizationType { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Organization Type Code")]
        [Column(Order = 2)]
        public string OrganizationTypeCode { get; set; } = null!;

        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}
