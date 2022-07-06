using MEInsight.Entities.Reference;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Core
{
    [Table("SchoolPeriod")]
    public class SchoolPeriod : BaseEntity
    {
        public SchoolPeriod()
        {
            SchoolEnrollments = new HashSet<SchoolEnrollment>();
            SchoolClassrooms = new HashSet<SchoolClassroom>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "School Period")]
        [Column(Order = 0)]
        public int SchoolPeriodId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Period name")]
        [Column(Order = 1)]
        public string PeriodName { get; set; } = null!;

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [Column(Order = 2)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Column(Order = 3)]
        public DateTime? EndDate { get; set; }

        public virtual ICollection<SchoolEnrollment> SchoolEnrollments { get; set; }
        public virtual ICollection<SchoolClassroom> SchoolClassrooms { get; set; }

    }
}
