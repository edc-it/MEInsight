using MEInsight.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefDisabilityType")]
    public class RefDisabilityType
    {
        public RefDisabilityType()
        {
            Participants = new HashSet<Participant>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Disability Type Id")]
        [Column(Order = 0)]
        public int RefDisabilityTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Disability Type Code")]
        [Column(Order = 1)]
        public string DisabilityTypeCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Disability Type")]
        [Column(Order = 2)]
        public string DisabilityType { get; set; } = null!;

        public virtual ICollection<Participant> Participants { get; set; }
    }
}
