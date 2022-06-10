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

        [ForeignKey("OrganizationId")]
        //[InverseProperty("Users")]
        [Display(Name = "Organization")]
        public virtual Organization? Organizations { get; set; }
    }
}
