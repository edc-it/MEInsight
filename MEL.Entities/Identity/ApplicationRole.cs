using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MEL.Entities.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        [MaxLength(250)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
