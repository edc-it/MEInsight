using MEInsight.Entities.Programs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefProgramDeliveryType")]
    public class RefProgramDeliveryType
    {
        public RefProgramDeliveryType()
        {
            Programs = new HashSet<Program>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Program Delivery Type Id")]
        public int RefProgramDeliveryTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Program Delivery Type Code")]
        public string ProgramDeliveryTypeCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Program Delivery Type")]
        public string ProgramDeliveryType { get; set; } = null!;

        public virtual ICollection<Program> Programs { get; set; }
    }
}
