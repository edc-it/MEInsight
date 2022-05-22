using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEInsight.Entities.Reference;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MEInsight.Entities.Core
{
    [Table("Student")]
    [Index("RefStudentSpecializationId", Name = "IX_Student_RefStudentSpecializationId")]
    [Index("RefStudentTypeId", Name = "IX_Student_RefStudentTypeId")]
    [Index("RefStudentYearOfStudyId", Name = "IX_Student_RefStudentYearOfStudyId")]
    public class Student : Participant
    {
        //[Key]
        //[Required(ErrorMessage = "The {0} field is required.")]
        //[Display(Name = "Participant")]
        //[Column(Order = 0)]
        //public Guid ParticipantId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Student Code")]
        [Column(Order = 1)]
        public string? StudentCode { get; set; }

        [Display(Name = "Student Type")]
        [Column(Order = 2)]
        public int? RefStudentTypeId { get; set; }

        [Display(Name = "Student Specialization")]
        [Column(Order = 3)]
        public int? RefStudentSpecializationId { get; set; }

        [MaxLength(255)]
        [Display(Name = "Parent/Guardian")]
        [Column(Order = 4)]
        public string? ParentGuardian { get; set; }

        [Display(Name = "Year of Study")]
        [Column(Order = 5)]
        public int? RefStudentYearOfStudyId { get; set; }

        // Navigation Properties
        [ForeignKey("RefStudentTypeId")]
        [Display(Name = "Student Type")]
        public virtual RefStudentType? StudentTypes { get; set; }

        [ForeignKey("RefStudentSpecializationId")]
        [Display(Name = "Student Specialization")]
        public virtual RefStudentSpecialization? StudentSpecializations { get; set; }

        [ForeignKey("RefStudentYearOfStudyId")]
        [Display(Name = "Student Year of Study")]
        public virtual RefStudentYearOfStudy? StudentYearOfStudies { get; set; }



        //[ForeignKey("ParticipantId")]
        //public virtual Participant Participants { get; set; } = null!;

    }
}
