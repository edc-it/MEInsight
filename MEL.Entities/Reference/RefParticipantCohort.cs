using MEL.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefParticipantCohort
    {
        public RefParticipantCohort()
        {
            this.Participants = new HashSet<Participant>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Participant Cohort Id")]
        [Column(Order = 0)]
        public int RefParticipantCohortId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Participant Cohort Code")]
        [Column(Order = 1)]
        public string ParticipantCohortCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Participant Cohort")]
        [Column(Order = 2)]
        public string ParticipantCohort { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
    }
}
