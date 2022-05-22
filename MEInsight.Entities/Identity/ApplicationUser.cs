using MEInsight.Entities.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEInsight.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Display(Name = "Organization")]
        public Guid? OrganizationId { get; set; }

        [MaxLength(70)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [MaxLength(70)]
        [Display(Name = "Surname/Last name")]
        public string? LastName { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [MaxLength(50)]
        [ScaffoldColumn(false)]
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        [ScaffoldColumn(false)]
        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Modified By")]
        public DateTime? ModifiedDate { get; set; }

        [MaxLength(50)]
        [ScaffoldColumn(false)]
        [Display(Name = "Deleted By")]
        public string? DeletedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Deleted By")]
        public DateTime? DeletedDate { get; set; }

        [ForeignKey("OrganizationId")]
        [Display(Name = "Organization")]
        public virtual Organization? Organizations { get; set; }
    }
}
