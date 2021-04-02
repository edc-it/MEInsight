using MEL.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEL.Entities.Reference
{
    public class RefParticipantType
    {
        public RefParticipantType()
        {
            this.Participants = new HashSet<Participant>();
            this.SchoolEnrollments = new HashSet<SchoolEnrollment>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Participant Type Id")]
        [Column(Order = 0)]
        public int RefParticipantTypeId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Participant Type Code")]
        [Column(Order = 1)]
        public string ParticipantTypeCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Participant Type")]
        [Column(Order = 2)]
        public string ParticipantType { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<SchoolEnrollment> SchoolEnrollments { get; set; }

    }
}
