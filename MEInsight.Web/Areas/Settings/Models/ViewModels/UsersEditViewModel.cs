using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MEInsight.Web.Areas.Settings.Models.ViewModels
{
    public class UsersEditViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [MaxLength(70)]
        [Display(Name = "First name")]
        public string? FirstName { get; set; }

        [MaxLength(70)]
        [Display(Name = "Last name")]
        public string? LastName { get; set; }

        [DisplayName("Organization")]
        public Guid? OrganizationId { get; set; }

        [Display(Name = "Active")]
        public int? Active { get; set; }

        [MaxLength(50)]
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Roles")]
        public List<SelectListItem>? Roles { get; set; }

        [Display(Name = "Role")]
        public Guid? RoleId { get; set; }
    }
}
