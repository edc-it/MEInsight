using MEInsight.Entities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEInsight.Entities.Reference
{
    [Table("RefParticipantDataSource")]
    public class RefParticipantDataSource
    {
        public RefParticipantDataSource()
        {
            ParticipantData = new HashSet<ParticipantData>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Participant Data Source Id")]
        [Column(Order = 0)]
        public int RefParticipantDataSourceId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Participant's Data Source Code")]
        [Column(Order = 1)]
        public string? ParticipantDataSourceCode { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(150)]
        [Display(Name = "Participant's Data Source")]
        [Column(Order = 2)]
        public string? ParticipantDataSource { get; set; } = null!;

        public virtual ICollection<ParticipantData>? ParticipantData { get; set; }

    }
}
