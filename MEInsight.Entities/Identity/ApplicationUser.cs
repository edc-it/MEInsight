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

        [StringLength(70)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [StringLength(70)]
        [Display(Name = "Surname/Last name")]
        public string? LastName { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Created Date")]
        public DateTimeOffset? CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Modified Date")]
        public DateTimeOffset? ModifiedDate { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Deleted By")]
        public string? DeletedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Deleted Date")]
        public DateTimeOffset? DeletedDate { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [ForeignKey("OrganizationId")]
        [Display(Name = "Organization")]
        public virtual Organization? Organizations { get; set; }
    }
}
