using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Core;

namespace MEL.Entities.Reference
{
    public class RefSex
    {
        public RefSex()
        {
            this.Participants = new HashSet<Participant>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Sex Id")]
        [Column(Order = 0)]
        public int RefSexId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(50)]
        [Display(Name = "Sex")]
        [Column(Order = 1)]
        public string Sex { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(1)]
        [Display(Name = "Sex")]
        [Column(Order = 2)]
        public string SexId { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
    }
}
