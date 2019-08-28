using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MEL.Entities
{
    public abstract class BaseEntity
    {
        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Created Date")]
        public DateTimeOffset? CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Modified Date")]
        public DateTimeOffset? ModifiedDate { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Deleted By")]
        public string DeletedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Deleted Date")]
        public DateTimeOffset? DeletedDate { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }
    }
}
