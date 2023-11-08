using MEInsight.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Entities.Reference
{
    [Table("RefParticipantCohort")]
    public class RefParticipantCohort
    {
        public RefParticipantCohort()
        {
            Participants = new HashSet<Participant>();
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
        public string ParticipantCohortCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Participant Cohort")]
        [Column(Order = 2)]
        public string ParticipantCohort { get; set; } = null!;

        public virtual ICollection<Participant> Participants { get; set; }
    }
}
