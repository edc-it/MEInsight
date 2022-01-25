using MEL.Entities;
using MEL.Entities.Core;
using MEL.Entities.Identity;
using MEL.Entities.Programs;
using MEL.Entities.Reference;
using MEL.Entities.TLM;
using MEL.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MEL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Identity
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        //Entities.Core
        public DbSet<MEL.Entities.Core.Organization> Organizations { get; set; }
        public DbSet<MEL.Entities.Core.School> Schools { get; set; }
        public DbSet<MEL.Entities.Core.Partner> Partners { get; set; }
        public DbSet<MEL.Entities.Core.SchoolPeriod> SchoolPeriods { get; set; }
        public DbSet<MEL.Entities.Core.SchoolEnrollment> SchoolEnrollments { get; set; }
        public DbSet<MEL.Entities.Core.SchoolClassroom> SchoolClassrooms { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<MEL.Entities.Core.Teacher> Teachers { get; set; }
        public DbSet<MEL.Entities.Core.EducationAdministrator> EducationAdministrators { get; set; }


        //Reference
        public DbSet<MEL.Entities.Reference.RefOrganizationType> OrganizationTypes { get; set; }
        public DbSet<MEL.Entities.Reference.RefSchoolType> SchoolTypes { get; set; }
        public DbSet<MEL.Entities.Reference.RefSchoolLocation> SchoolLocations { get; set; }
        public DbSet<MEL.Entities.Reference.RefSchoolLanguage> SchoolLanguages { get; set; }
        public DbSet<MEL.Entities.Reference.RefSchoolAdministrationType> SchoolAdministrationTypes { get; set; }
        public DbSet<MEL.Entities.Reference.RefSchoolCluster> SchoolClusters { get; set; }
        public DbSet<MEL.Entities.Reference.RefSchoolStatus> SchoolStatus { get; set; }
        
        public DbSet<MEL.Entities.Reference.RefPartnerType> PartnerTypes { get; set; }
        public DbSet<MEL.Entities.Reference.RefPartnerSector> PartnerSectors { get; set; }

        public DbSet<MEL.Entities.Reference.RefParticipantType> ParticipantTypes { get; set; }
        public DbSet<MEL.Entities.Reference.RefParticipantCohort> ParticipantCohorts { get; set; }
        public DbSet<MEL.Entities.Reference.RefSex> Sex { get; set; }
        public DbSet<MEL.Entities.Reference.RefDisabilityType> DisabilityTypes { get; set; }

        public DbSet<MEL.Entities.Reference.RefStudentType> StudentTypes { get; set; }
        public DbSet<MEL.Entities.Reference.RefStudentSpecialization> StudentSpecializations { get; set; }
        public DbSet<MEL.Entities.Reference.RefStudentYearOfStudy> StudentYearOfStudies { get; set; }
        
        public DbSet<MEL.Entities.Reference.RefTeacherType> TeacherTypes { get; set; }
        public DbSet<MEL.Entities.Reference.RefTeacherPosition> TeacherPositions { get; set; }
        public DbSet<MEL.Entities.Reference.RefTeacherEmploymentType> TeacherEmploymentTypes { get; set; }

        public DbSet<MEL.Entities.Reference.RefEducationAdministratorType> EducationAdministratorTypes { get; set; }
        public DbSet<MEL.Entities.Reference.RefEducationAdministratorPosition> EducationAdministratorPositions { get; set; }
        public DbSet<MEL.Entities.Reference.RefEducationAdministratorOffice> EducationAdministratorOffices { get; set; }

        public DbSet<MEL.Entities.Reference.RefGradeLevel> GradeLevels { get; set; }

        public DbSet<MEL.Entities.Reference.RefEnrollmentStatus> EnrollmentStatus { get; set; }
        public DbSet<MEL.Entities.Reference.RefEvaluationStatus> EvaluationStatus { get; set; }
        public DbSet<MEL.Entities.Reference.RefAssessmentType> AssessmentTypes { get; set; }

        public DbSet<MEL.Entities.Reference.RefProgramType> ProgramTypes { get; set; }
        public DbSet<MEL.Entities.Reference.RefProgramDeliveryType> ProgramDeliveryTypes { get; set; }

        public DbSet<MEL.Entities.Reference.RefAttendanceUnit> AttendanceUnits { get; set; }
        
        public DbSet<MEL.Entities.Reference.RefTLMDistributionStatus> TLMDistributionStatus { get; set; }
        public DbSet<MEL.Entities.Reference.RefTLMLanguage> TLMLanguages { get; set; }
        public DbSet<MEL.Entities.Reference.RefTLMMaterialSet> TLMMaterialSets { get; set; }
        public DbSet<MEL.Entities.Reference.RefTLMSubject> TLMSubjects { get; set; }
        public DbSet<MEL.Entities.Reference.RefTLMGroup> TLMGroups { get; set; }
        public DbSet<MEL.Entities.Reference.RefTLMMaterialType> TLMMaterialTypes { get; set; }
        
        //Reference.Location
        public DbSet<MEL.Entities.Reference.RefLocation> Locations { get; set; }
        public DbSet<MEL.Entities.Reference.RefLocationType> LocationTypes { get; set; }

        //TLM Book distribution
        public DbSet<MEL.Entities.TLM.TLMDistribution> TLMDistributions { get; set; }
        public DbSet<MEL.Entities.TLM.TLMDistributionDetail> TLMDistributionDetails { get; set; }
        public DbSet<MEL.Entities.TLM.TLMDistributionPeriod> TLMDistributionPeriods { get; set; }
        public DbSet<MEL.Entities.TLM.TLMMaterial> TLMMaterials { get; set; }

        //Training
        public DbSet<MEL.Entities.Programs.Program> Programs { get; set; }
        public DbSet<MEL.Entities.Programs.ProgramAssessment> ProgramAssessments { get; set; }
        public DbSet<MEL.Entities.Programs.Group> Groups { get; set; }
        public DbSet<MEL.Entities.Programs.GroupEnrollment> GroupEnrollments { get; set; }
        public DbSet<MEL.Entities.Programs.GroupEvaluation> GroupEvaluations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            //Entities.Core
            builder.Entity<Organization>().HasQueryFilter(p => !p.IsDeleted).ToTable("Organization");
            builder.Entity<School>().HasQueryFilter(p => !p.IsDeleted).ToTable("School");
            builder.Entity<Partner>().HasQueryFilter(p => !p.IsDeleted).ToTable("Partner");
            builder.Entity<SchoolPeriod>().HasQueryFilter(p => !p.IsDeleted).ToTable("SchoolPeriod");
            builder.Entity<SchoolEnrollment>().HasQueryFilter(p => !p.IsDeleted).ToTable("SchoolEnrollment");
            builder.Entity<SchoolClassroom>().HasQueryFilter(p => !p.IsDeleted).ToTable("SchoolClassroom");
            builder.Entity<Participant>().HasQueryFilter(p => !p.IsDeleted).ToTable("Participant");
            builder.Entity<Student>().HasQueryFilter(p => !p.IsDeleted).ToTable("Student");
            builder.Entity<Teacher>().HasQueryFilter(p => !p.IsDeleted).ToTable("Teacher");
            builder.Entity<EducationAdministrator>().HasQueryFilter(p => !p.IsDeleted).ToTable("EducationAdministrator");

            //Entities.Reference
            builder.Entity<RefOrganizationType>().ToTable("RefOrganizationType");
            builder.Entity<RefSchoolType>().ToTable("RefSchoolType");
            builder.Entity<RefSchoolLocation>().ToTable("RefSchoolLocation");
            builder.Entity<RefSchoolLanguage>().ToTable("RefSchoolLanguage");
            builder.Entity<RefSchoolAdministrationType>().ToTable("RefSchoolAdministrationType");
            builder.Entity<RefSchoolCluster>().ToTable("RefSchoolCluster");
            builder.Entity<RefSchoolStatus>().ToTable("RefSchoolStatus");

            builder.Entity<RefPartnerType>().ToTable("RefPartnerType");
            builder.Entity<RefPartnerSector>().ToTable("RefPartnerSector");

            builder.Entity<RefParticipantType>().ToTable("RefParticipantType");
            builder.Entity<RefParticipantCohort>().ToTable("RefParticipantCohort");
            builder.Entity<RefSex>().ToTable("RefSex");
            builder.Entity<RefDisabilityType>().ToTable("RefDisabilityType");

            builder.Entity<RefStudentType>().ToTable("RefStudentType");
            builder.Entity<RefStudentSpecialization>().ToTable("RefStudentSpecialization");
            builder.Entity<RefStudentYearOfStudy>().ToTable("RefStudentYearOfStudy");

            builder.Entity<RefTeacherType>().ToTable("RefTeacherType");
            builder.Entity<RefTeacherPosition>().ToTable("RefTeacherPosition");
            builder.Entity<RefTeacherEmploymentType>().ToTable("RefTeacherEmploymentType");

            builder.Entity<RefEducationAdministratorType>().ToTable("RefEducationAdministratorType");
            builder.Entity<RefEducationAdministratorPosition>().ToTable("RefEducationAdministratorPosition");
            builder.Entity<RefEducationAdministratorOffice>().ToTable("RefEducationAdministratorOffice");

            builder.Entity<RefGradeLevel>().ToTable("RefGradeLevel");
            builder.Entity<RefEnrollmentStatus>().ToTable("RefEnrollmentStatus");
            builder.Entity<RefEvaluationStatus>().ToTable("RefEvaluationStatus");
            builder.Entity<RefAssessmentType>().ToTable("RefAssessmentType");

            builder.Entity<RefProgramType>().ToTable("RefProgramType");
            builder.Entity<RefProgramDeliveryType>().ToTable("RefProgramDeliveryType");
            builder.Entity<RefAttendanceUnit>().ToTable("RefAttendanceUnit");

            builder.Entity<RefTLMDistributionStatus>().ToTable("RefTLMDistributionStatus");
            builder.Entity<RefTLMLanguage>().ToTable("RefTLMLanguage");
            builder.Entity<RefTLMMaterialSet>().ToTable("RefTLMMaterialSet");
            builder.Entity<RefTLMSubject>().ToTable("RefTLMSubject");
            builder.Entity<RefTLMGroup>().ToTable("RefTLMGroup");
            builder.Entity<RefTLMMaterialType>().ToTable("RefTLMMaterialType");

            builder.Entity<RefLocation>().ToTable("RefLocation");
            builder.Entity<RefLocationType>().ToTable("RefLocationType");

            //Entities.TLM
            builder.Entity<TLMDistribution>().HasQueryFilter(p => !p.IsDeleted).ToTable("TLMDistribution");

            builder.Entity<TLMDistribution>()
                .HasOne(m => m.OrganizationsFrom)
                .WithMany(t => t.TLMDistributionsFrom)
                .HasForeignKey(m => m.OrganizationIdFrom);

            builder.Entity<TLMDistribution>()
                .HasOne(m => m.OrganizationsTo)
                .WithMany(t => t.TLMDistributionsTo)
                .HasForeignKey(m => m.OrganizationIdTo);

            builder.Entity<TLMDistributionDetail>().HasQueryFilter(p => !p.IsDeleted).ToTable("TLMDistributionDetail");
            builder.Entity<TLMDistributionPeriod>().HasQueryFilter(p => !p.IsDeleted).ToTable("TLMDistributionPeriod");
            builder.Entity<TLMMaterial>().HasQueryFilter(p => !p.IsDeleted).ToTable("TLMMaterial");

            //Entities.Training
            builder.Entity<Program>().HasQueryFilter(p => !p.IsDeleted).ToTable("Program");
            builder.Entity<ProgramAssessment>().HasQueryFilter(p => !p.IsDeleted).ToTable("ProgramAssessment");
            builder.Entity<Group>().HasQueryFilter(p => !p.IsDeleted).ToTable("Group");
            builder.Entity<GroupEnrollment>().HasQueryFilter(p => !p.IsDeleted).ToTable("GroupEnrollment");
            builder.Entity<GroupEnrollment>().HasIndex(t => t.ParticipantId);

            builder.Entity<GroupEvaluation>().HasQueryFilter(p => !p.IsDeleted).ToTable("GroupEvaluation");
            builder.Entity<GroupEvaluation>().HasIndex(t => t.GroupEnrollmentId);

            // Disables Cascade.Delete
            List<IMutableEntityType> identiyEntities = new List<IMutableEntityType>
            {
                builder.Model.FindEntityType(typeof(ApplicationUser)),
                builder.Model.FindEntityType(typeof(ApplicationRole)),
                builder.Model.FindEntityType(typeof(IdentityUserRole<string>)),
                builder.Model.FindEntityType(typeof(IdentityUserClaim<string>)),
                builder.Model.FindEntityType(typeof(IdentityUserLogin<string>)),
                builder.Model.FindEntityType(typeof(IdentityUserToken<string>)),
                builder.Model.FindEntityType(typeof(IdentityRoleClaim<string>))
            };

            var cascadeFKs = builder.Model.GetEntityTypes()
                .Except(identiyEntities)
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Override SaveChanges - enables Soft-Delete
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {

            string userName = null;
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            }

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["IsDeleted"] = false;
                            entry.CurrentValues["CreatedDate"] = DateTimeOffset.UtcNow;
                            entry.CurrentValues["CreatedBy"] = userName;
                            break;

                        case EntityState.Modified:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = false;
                            entry.CurrentValues["CreatedDate"] = entry.GetDatabaseValues().GetValue<object>("CreatedDate");
                            entry.CurrentValues["CreatedBy"] = entry.GetDatabaseValues().GetValue<object>("CreatedBy");
                            entry.CurrentValues["ModifiedDate"] = DateTimeOffset.UtcNow;
                            entry.CurrentValues["ModifiedBy"] = userName;

                            break;

                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                            entry.CurrentValues["CreatedDate"] = entry.GetDatabaseValues().GetValue<object>("CreatedDate");
                            entry.CurrentValues["CreatedBy"] = entry.GetDatabaseValues().GetValue<object>("CreatedBy");
                            entry.CurrentValues["ModifiedDate"] = entry.GetDatabaseValues().GetValue<object>("ModifiedDate");
                            entry.CurrentValues["ModifiedBy"] = entry.GetDatabaseValues().GetValue<object>("ModifiedBy");
                            entry.CurrentValues["DeletedDate"] = DateTimeOffset.UtcNow;
                            entry.CurrentValues["DeletedBy"] = userName;
                            break;
                    }
                }
            }
        }
    }
}
