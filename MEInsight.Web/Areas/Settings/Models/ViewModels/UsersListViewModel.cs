using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MEInsight.Web.Areas.Settings.Models.ViewModels
{
    public class UsersListViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Organization")]
        public Guid? OrganizationId { get; set; }

        [Display(Name = "Organization")]
        public string Organization { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Active")]
        public int? Active { get; set; }

        [Display(Name = "Access Failed Count")]
        public int? AccessFailedCount { get; set; }

        [Display(Name = "Lockout Enabled")]
        public bool? LockoutEnabled { get; set; }

        [Display(Name = "Lockout End")]
        public DateTimeOffset? LockoutEnd { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
