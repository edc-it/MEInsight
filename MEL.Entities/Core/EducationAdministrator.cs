using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Reference;

namespace MEL.Entities.Core
{
    public class EducationAdministrator : BaseEntity
    {

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Participant")]
        [Column(Order = 0)]
        public Guid ParticipantId { get; set; }

        [Display(Name = "Administrator Type")]
        [Column(Order = 1)]
        public int? RefEducationAdministratorTypeId { get; set; }

        [Display(Name = "Position")]
        [Column(Order = 2)]
        public int? RefEducationAdministratorPositionId { get; set; }

        [Display(Name = "Office")]
        [Column(Order = 3)]
        public int? RefEducationAdministratorOfficeId { get; set; }

        // Navigation properties
        [ForeignKey("ParticipantId")]
        public virtual Participant Participants { get; set; }

        [ForeignKey("RefEducationAdministratorTypeId")]
        [Display(Name = "Administrator Type")]
        public virtual RefEducationAdministratorType EducationAdministratorTypes { get; set; }

        [ForeignKey("RefEducationAdministratorPositionId")]
        [Display(Name = "Position")]
        public virtual RefEducationAdministratorPosition EducationAdministratorPositions { get; set; }
                
        [ForeignKey("RefEducationAdministratorOfficeId")]
        [Display(Name = "Office")]
        public virtual RefEducationAdministratorOffice EducationAdministratorOffices { get; set; }

    }
}
