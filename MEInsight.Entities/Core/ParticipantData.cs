using MEInsight.Entities.Reference;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MEInsight.Entities.Core
{
    [Table("ParticipantData")]
    public class ParticipantData : BaseEntity
    {

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ParticipantDataId")]
        [Column(Order = 0)]
        public Guid ParticipantDataId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        [Column(Order = 1)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "ParticipantId")]
        [Column(Order = 2)]
        public Guid? ParticipantId { get; set; }

        [StringLength(100)]
        [Display(Name = "Participant Code")]
        [Column(Order = 3)]
        public string? ParticipantCode { get; set; }

        [Column(Order = 4)]
        public int? RefParticipantDataSourceId { get; set; }

        [IgnoreDataMember]
        public string? ExtendedData { get; set; }

        [Display(Name = "JSON Object data")]
        [NotMapped]
        [Column(Order = 5)]
        public object? Data
        {
            get { return (ExtendedData == null) ? null : JsonSerializer.Deserialize(ExtendedData, typeof(object)); }
            set { ExtendedData = JsonSerializer.Serialize(value); }
        }

        [ForeignKey("ParticipantId")]
        [Display(Name = "Participant")]
        public virtual Participant? Participants { get; set; }

        [ForeignKey("RefParticipantDataSourceId")]
        [Display(Name = "Participant Data Source")]
        public virtual RefParticipantDataSource? ParticipantDataSources { get; set; }

    }
}
