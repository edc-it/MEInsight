using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Programs;

namespace MEL.Entities.Reference
{
    public class RefProgramType
    {
        public RefProgramType()
        {
            this.Programs = new HashSet<Program>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Program Type Id")]
        public int RefProgramTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Program Type Code")]
        public string ProgramTypeCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Program Type")]
        public string ProgramType { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
    }
}
