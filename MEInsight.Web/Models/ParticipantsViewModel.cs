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
    public class ParticipantsViewModel
    {
        public ParticipantsViewModel()
        {
            this.GroupEnrollments = new HashSet<GroupEnrollment>();
            this.Groups = new HashSet<Group>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "ParticipantId")]
        public Guid ParticipantId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Organization")]
        public Guid? OrganizationId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Participant Type")]
        public int RefParticipantTypeId { get; set; }

        [Display(Name = "Participant Cohort")]
        public int? RefParticipantCohortId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(25)]
        [Display(Name = "Participant Code")]
        [Remote(action: "VerifyParticipantCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "ParticipantCodeInitialValue")]
        public string ParticipantCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(35)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [MaxLength(35)]
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(35)]
        [Display(Name = "Surname/Last name")]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Name")]
        public string NameFirst
        {
            get
            {
                return String.Concat(FirstName, " ", MiddleName ?? "", " ", LastName);
            }
        }

        [NotMapped]
        [Display(Name = "Name")]
        public string Name
        {
            get
            {
                return String.Concat(LastName, ", ", FirstName, MiddleName == null ? "" : " " + MiddleName);
            }
        }

        [NotMapped]
        [Display(Name = "Name")]
        public string NameId
        {
            get
            {
                return String.Concat(LastName, ", ", FirstName, MiddleName == null ? "" : " " + MiddleName, " (", ParticipantCode, ")");
                //return String.Concat(FirstName, " ", MiddleName ?? "", " ", LastName, " (", PersonCode, ")");
            }
        }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Sex")]
        public int RefSexId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Age")]
        public int? Age { get; set; }

        [Display(Name = "Has Disability?")]
        public bool? Disability { get; set; }

        [Display(Name = "Disability Type")]
        public int? RefDisabilityTypeId { get; set; }

        [MaxLength(20)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [MaxLength(20)]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [MaxLength(320)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(200)]
        [Display(Name = "Facebook")]
        public string Facebook { get; set; }

        [MaxLength(200)]
        [Display(Name = "Instant Messenger")]
        public string InstantMessenger { get; set; }

        [Display(Name = "Location")]
        public string RefLocationId { get; set; }

        [MaxLength(384)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string Address { get; set; }


        // Student properties
        [MaxLength(100)]
        [Display(Name = "Student Code")]
        [Remote(action: "VerifyStudentCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "StudentCodeInitialValue")]
        public string StudentCode { get; set; }

        [Display(Name = "Student Type")]
        public int? RefStudentTypeId { get; set; }

        [Display(Name = "Student Specialization")]
        public int? RefStudentSpecializationId { get; set; }

        [Display(Name = "Student Year of Study")]
        public int? RefStudentYearOfStudyId { get; set; }

        [MaxLength(255)]
        [Display(Name = "Parent/Guardian")]
        public string ParentGuardian { get; set; }


        // Teacher
        [Display(Name = "Teacher Type")]
        public int? RefTeacherTypeId { get; set; }

        [Display(Name = "Position")]
        public int? RefTeacherPositionId { get; set; }

        [Display(Name = "Employment Type")]
        public int? RefTeacherEmploymentTypeId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Grades taught?")]
        public string GradeLevels { get; set; }

        //Education Administrator
        [Display(Name = "Administrator Type")]
        public int? RefEducationAdministratorTypeId { get; set; }

        [Display(Name = "Position")]
        public int? RefEducationAdministratorPositionId { get; set; }

        [Display(Name = "Office")]
        public int? RefEducationAdministratorOfficeId { get; set; }

        // Base Entity
        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Created Date")]
        public DateTimeOffset? CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Modified Date")]
        public DateTimeOffset? ModifiedDate { get; set; }

        [ScaffoldColumn(false)]
        [MaxLength(50)]
        [Display(Name = "Deleted By")]
        public string DeletedBy { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Deleted Date")]
        public DateTimeOffset? DeletedDate { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        // Navigation properties
        [ForeignKey("OrganizationId")]
        [Display(Name = "Organization")]
        public virtual Organization Organizations { get; set; }

        // Participant
        [ForeignKey("ParticipantId")]
        public virtual Participant Participants { get; set; }

        [ForeignKey("RefParticipantTypeId")]
        [Display(Name = "Participant Type")]
        public virtual RefParticipantType ParticipantTypes { get; set; }

        [ForeignKey("RefParticipantCohortId")]
        [Display(Name = "Participant Cohort")]
        public virtual RefParticipantCohort ParticipantCohorts { get; set; }

        [ForeignKey("RefSexId")]
        [Display(Name = "Sex")]
        public virtual RefSex Sex { get; set; }

        [ForeignKey("RefDisabilityTypeId")]
        [Display(Name = "Disability Type")]
        public virtual RefDisabilityType DisabilityTypes { get; set; }

        [ForeignKey("RefLocationId")]
        [Display(Name = "Location")]
        public virtual RefLocation Locations { get; set; }

        // Student
        [ForeignKey("RefStudentTypeId")]
        [Display(Name = "Student Type")]
        public virtual RefStudentType StudentTypes { get; set; }

        [ForeignKey("RefStudentSpecializationId")]
        [Display(Name = "Student Specialization")]
        public virtual RefStudentSpecialization StudentSpecializations { get; set; }

        [ForeignKey("RefStudentYearOfStudyId")]
        [Display(Name = "Student Year of Study")]
        public virtual RefStudentYearOfStudy StudentYearOfStudies { get; set; }

        // Teacher
        [ForeignKey("RefTeacherTypeId")]
        [Display(Name = "Teacher Type")]
        public virtual RefTeacherType TeacherTypes { get; set; }

        [ForeignKey("RefTeacherPositionId")]
        [Display(Name = "Position")]
        public virtual RefTeacherPosition TeacherPositions { get; set; }

        [ForeignKey("RefTeacherEmploymentTypeId")]
        [Display(Name = "Employment Type")]
        public virtual RefTeacherEmploymentType TeacherEmploymentTypes { get; set; }

        // Education Administrator
        [ForeignKey("RefEducationAdministratorTypeId")]
        [Display(Name = "Administrator Type")]
        public virtual RefEducationAdministratorType EducationAdministratorTypes { get; set; }

        [ForeignKey("RefEducationAdministratorPositionId")]
        [Display(Name = "Position")]
        public virtual RefEducationAdministratorPosition EducationAdministratorPositions { get; set; }

        [ForeignKey("RefEducationAdministratorOfficeId")]
        [Display(Name = "Office")]
        public virtual RefEducationAdministratorOffice EducationAdministratorOffices { get; set; }

        // Navigation Properties
        // Participant
        public virtual ICollection<GroupEnrollment> GroupEnrollments { get; set; }

        // Teacher
        public virtual ICollection<Group> Groups { get; set; }

    }
}
