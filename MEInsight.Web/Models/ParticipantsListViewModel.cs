using MEInsight.Entities.Core;
using MEInsight.Entities.Programs;
using MEInsight.Entities.Reference;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MEInsight.Web.Models
{
    public class ParticipantsListViewModel
    {
        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "ParticipantId")]
        public Guid? ParticipantId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Registration Date")]
        //[DataType(DataType.Date)]
        public string? RegistrationDate { get; set; }

        [Display(Name = "Organization")]
        public Guid? OrganizationId { get; set; }

        [Display(Name = "Organization Name")]
        public string? OrganizationName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Participant Type")]
        public int? RefParticipantTypeId { get; set; }

        [Display(Name = "Participant Type")]
        public string? ParticipantType { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Participant Code")]
        [Remote(action: "VerifyParticipantCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "ParticipantCodeInitialValue")]
        public string? ParticipantCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(35)]
        [Display(Name = "First name")]
        public string? FirstName { get; set; }

        [MaxLength(35)]
        [Display(Name = "Middle name")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(35)]
        [Display(Name = "Surname/Last name")]
        public string? LastName { get; set; }

        
        [Display(Name = "Name")]
        public string? NameFirst { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Name")]
        public string? NameId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Sex")]
        public int? RefSexId { get; set; }

        [Display(Name = "Sex")]
        public string? Sex { get; set; }

        [Display(Name = "Sex")]
        public string? SexId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Age")]
        public int? Age { get; set; }

        [Display(Name = "Has Disability?")]
        public bool? Disability { get; set; }

        [Display(Name = "Disability Type")]
        public int? RefStudentDisabilityTypeId { get; set; }

        [Display(Name = "Disability Type")]
        public string? DisabilityType { get; set; }

        [MaxLength(20)]
        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [MaxLength(20)]
        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }

        [MaxLength(320)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [MaxLength(200)]
        [Display(Name = "Facebook")]
        public string? Facebook { get; set; }

        [MaxLength(200)]
        [Display(Name = "Instant Messenger")]
        public string? InstantMessenger { get; set; }

        [Display(Name = "Location")]
        public string? RefLocationId { get; set; }

        [Display(Name = "Location")]
        public string? Location { get; set; }

        [MaxLength(384)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        // Student properties
        [MaxLength(100)]
        [Display(Name = "Student Code")]
        [Remote(action: "VerifyStudentCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "StudentCodeInitialValue")]
        public string? StudentCode { get; set; }

        [Display(Name = "Student Type")]
        public int? RefStudentTypeId { get; set; }

        [Display(Name = "Student Type")]
        public string? StudentType { get; set; }

        [Display(Name = "Student Specialization")]
        public int? RefStudentSpecializationId { get; set; }

        [Display(Name = "Student Specialization")]
        public string? StudentSpecialization { get; set; }

        [Display(Name = "Student Year of Study")]
        public int? RefStudentYearOfStudyId { get; set; }

        [Display(Name = "Student Year of Study")]
        public string? StudentYearOfStudy { get; set; }

        [MaxLength(255)]
        [Display(Name = "Parent/Guardian")]
        public string? ParentGuardian { get; set; }

        

        // Teacher
        [Display(Name = "Teacher Type")]
        public int? RefTeacherTypeId { get; set; }

        [Display(Name = "Teacher Type")]
        public string? TeacherType { get; set; }

        [Display(Name = "Position")]
        public int? RefTeacherPositionId { get; set; }

        [Display(Name = "Position")]
        public string? TeacherPosition { get; set; }

        [Display(Name = "Employment Type")]
        public int? RefTeacherEmploymentTypeId { get; set; }

        [Display(Name = "Employment Type")]
        public string? TeacherEmploymentType { get; set; }

        [MaxLength(50)]
        [Display(Name = "Grades taught?")]
        public string? GradeLevels { get; set; }

        //Education Administrator
        [Display(Name = "Administrator Type")]
        public int? RefEducationAdministratorTypeId { get; set; }

        [Display(Name = "Administrator Type")]
        public string? EducationAdministratorType { get; set; }

        [Display(Name = "Position")]
        public int? RefEducationAdministratorPositionId { get; set; }

        [Display(Name = "Position")]
        public string? EducationAdministratorPosition { get; set; }

        [Display(Name = "Office")]
        public int? RefEducationAdministratorOfficeId { get; set; }

        [Display(Name = "Office")]
        public string? EducationAdministratorOffice { get; set; }

        public string? Position { get; set; }

        // Base Entity
        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Created Date")]
        public DateTimeOffset? CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Modified Date")]
        public DateTimeOffset? ModifiedDate { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Deleted By")]
        public string? DeletedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Deleted Date")]
        public DateTimeOffset? DeletedDate { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Is Deleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "Enrollment Count")]
        public int? EnrollmentCount { get; set; }

        //public Guid? GroupId { get; set; }
        //public string GroupName { get; set; }
        
    }
}
