using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MEInsight.Entities.Reference;
using Microsoft.EntityFrameworkCore;

namespace MEInsight.Entities.Core
{
    [Table("EducationAdministrator")]
    [Index("RefEducationAdministratorOfficeId", Name = "IX_EducationAdministrator_RefEducationAdministratorOfficeId")]
    [Index("RefEducationAdministratorPositionId", Name = "IX_EducationAdministrator_RefEducationAdministratorPositionId")]
    [Index("RefEducationAdministratorTypeId", Name = "IX_EducationAdministrator_RefEducationAdministratorTypeId")]
    public class EducationAdministrator : Participant
    {

        [Display(Name = "Administrator Type")]
        [Column(Order = 1)]
        public int? RefEducationAdministratorTypeId { get; set; }

        [Display(Name = "Position")]
        [Column(Order = 2)]
        public int? RefEducationAdministratorPositionId { get; set; }

        [Display(Name = "Office")]
        [Column(Order = 3)]
        public int? RefEducationAdministratorOfficeId { get; set; }

        [ForeignKey("RefEducationAdministratorTypeId")]
        [Display(Name = "Administrator Type")]
        public virtual RefEducationAdministratorType? EducationAdministratorTypes { get; set; }

        [ForeignKey("RefEducationAdministratorPositionId")]
        [Display(Name = "Position")]
        public virtual RefEducationAdministratorPosition? EducationAdministratorPositions { get; set; }
                
        [ForeignKey("RefEducationAdministratorOfficeId")]
        [Display(Name = "Office")]
        public virtual RefEducationAdministratorOffice? EducationAdministratorOffices { get; set; }

    }
}
