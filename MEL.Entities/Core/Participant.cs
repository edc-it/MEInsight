﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MEL.Entities.Reference;
using MEL.Entities.Programs;

namespace MEL.Entities.Core
{
    public class Participant : BaseEntity
    {
        public Participant()
        {
            this.Students = new HashSet<Student>();
            this.Teachers = new HashSet<Teacher>();
            this.EducationAdministrators = new HashSet<EducationAdministrator>();
            this.GroupEnrollments = new HashSet<GroupEnrollment>();
        }

        [Key]
        [Required(ErrorMessage = "The {0} field is required.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ParticipantId")]
        [Column(Order = 0)]
        public Guid ParticipantId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        [Column(Order = 1)]
        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Organization")]
        [Column(Order = 2)]
        public Guid? OrganizationId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Participant Type")]
        [Column(Order = 3)]
        public int RefParticipantTypeId { get; set; }

        [Display(Name = "Participant Type")]
        [Column(Order = 4)]
        public int? RefParticipantCohortId { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(100)]
        [Display(Name = "Participant Code")]
        [Column(Order = 5)]
        [Remote(action: "VerifyParticipantCode", controller: "RemoteValidations", HttpMethod = "POST", ErrorMessage = "This Code already exists.", AdditionalFields = "ParticipantCodeInitialValue")]
        public string ParticipantCode { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(35)]
        [Display(Name = "First name")]
        [Column(Order = 6)]
        public string FirstName { get; set; }

        [MaxLength(35)]
        [Display(Name = "Middle name")]
        [Column(Order = 7)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [MaxLength(35)]
        [Display(Name = "Surname/Last name")]
        [Column(Order = 8)]
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
            }
        }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Sex")]
        [Column(Order = 9)]
        public int RefSexId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [Column(Order = 10)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Age")]
        [Column(Order = 11)]
        public int? Age { get; set; }

        [Display(Name = "Has Disability?")]
        [Column(Order = 12)]
        public bool? Disability { get; set; }

        [Display(Name = "Disability Type")]
        [Column(Order = 13)]
        public int? RefDisabilityTypeId { get; set; }

        [MaxLength(20)]
        [Display(Name = "Phone")]
        [Column(Order = 14)]
        public string Phone { get; set; }

        [MaxLength(20)]
        [Display(Name = "Mobile")]
        [Column(Order = 15)]
        public string Mobile { get; set; }

        [MaxLength(320)]
        [Display(Name = "Email")]
        [Column(Order = 16)]
        public string Email { get; set; }

        [MaxLength(200)]
        [Display(Name = "Facebook")]
        [Column(Order = 17)]
        public string Facebook { get; set; }

        [MaxLength(200)]
        [Display(Name = "Instant Messenger")]
        [Column(Order = 18)]
        public string InstantMessenger { get; set; }

        [Display(Name = "Location")]
        [Column(Order = 19)]
        public string RefLocationId { get; set; }

        [MaxLength(384)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        [Column(Order = 20)]
        public string Address { get; set; }

        // Navigation properties
        [ForeignKey("OrganizationId")]
        [Display(Name = "Organization")]
        public virtual Organization Organizations { get; set; }

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

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<EducationAdministrator> EducationAdministrators { get; set; }
        public virtual ICollection<GroupEnrollment> GroupEnrollments { get; set; }

    }
}
