using MEInsight.Entities;
using MEInsight.Entities.Core;
using MEInsight.Entities.Identity;
using MEInsight.Entities.Programs;
using MEInsight.Entities.Reference;
using MEInsight.Entities.TLM;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MEInsight.Web.Data
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
        public DbSet<ApplicationUser> ApplicationUser { get; set; } = null!;
        public DbSet<ApplicationRole> ApplicationRole { get; set; } = null!;

        //Entities.Core
        public DbSet<Organization> Organizations { get; set; } = null!;
        public DbSet<School> Schools { get; set; } = null!;
        public DbSet<Partner> Partners { get; set; } = null!;
        public DbSet<SchoolPeriod> SchoolPeriods { get; set; } = null!;
        public DbSet<SchoolEnrollment> SchoolEnrollments { get; set; } = null!;
        public DbSet<SchoolClassroom> SchoolClassrooms { get; set; } = null!;
        public DbSet<Participant> Participants { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<EducationAdministrator> EducationAdministrators { get; set; } = null!;

        //Reference
        public DbSet<RefOrganizationType> OrganizationTypes { get; set; } = null!;
        public DbSet<RefSchoolType> SchoolTypes { get; set; } = null!;
        public DbSet<RefSchoolLocation> SchoolLocations { get; set; } = null!;
        public DbSet<RefSchoolLanguage> SchoolLanguages { get; set; } = null!;
        public DbSet<RefSchoolAdministrationType> SchoolAdministrationTypes { get; set; } = null!;
        public DbSet<RefSchoolCluster> SchoolClusters { get; set; } = null!;
        public DbSet<RefSchoolStatus> SchoolStatus { get; set; } = null!;

        public DbSet<RefPartnerType> PartnerTypes { get; set; } = null!;
        public DbSet<RefPartnerSector> PartnerSectors { get; set; } = null!;

        public DbSet<RefParticipantType> ParticipantTypes { get; set; } = null!;
        public DbSet<RefParticipantCohort> ParticipantCohorts { get; set; } = null!;
        public DbSet<RefSex> Sex { get; set; } = null!;
        public DbSet<RefDisabilityType> DisabilityTypes { get; set; } = null!;

        public DbSet<RefStudentType> StudentTypes { get; set; } = null!;
        public DbSet<RefStudentSpecialization> StudentSpecializations { get; set; } = null!;
        public DbSet<RefStudentYearOfStudy> StudentYearOfStudies { get; set; } = null!;

        public DbSet<RefTeacherType> TeacherTypes { get; set; } = null!;
        public DbSet<RefTeacherPosition> TeacherPositions { get; set; } = null!;
        public DbSet<RefTeacherEmploymentType> TeacherEmploymentTypes { get; set; } = null!;

        public DbSet<RefEducationAdministratorType> EducationAdministratorTypes { get; set; } = null!;
        public DbSet<RefEducationAdministratorPosition> EducationAdministratorPositions { get; set; } = null!;
        public DbSet<RefEducationAdministratorOffice> EducationAdministratorOffices { get; set; } = null!;

        public DbSet<RefGradeLevel> GradeLevels { get; set; } = null!;

        public DbSet<RefEnrollmentStatus> EnrollmentStatus { get; set; } = null!;
        public DbSet<RefEvaluationStatus> EvaluationStatus { get; set; } = null!;
        public DbSet<RefAssessmentType> AssessmentTypes { get; set; } = null!;

        public DbSet<RefProgramType> ProgramTypes { get; set; } = null!;
        public DbSet<RefProgramDeliveryType> ProgramDeliveryTypes { get; set; } = null!;

        public DbSet<RefAttendanceUnit> AttendanceUnits { get; set; } = null!;

        public DbSet<RefTLMDistributionStatus> TLMDistributionStatus { get; set; } = null!;
        public DbSet<RefTLMLanguage> TLMLanguages { get; set; } = null!;
        public DbSet<RefTLMMaterialSet> TLMMaterialSets { get; set; } = null!;
        public DbSet<RefTLMSubject> TLMSubjects { get; set; } = null!;
        public DbSet<RefTLMGroup> TLMGroups { get; set; } = null!;
        public DbSet<RefTLMMaterialType> TLMMaterialTypes { get; set; } = null!;

        //Reference.Location
        public DbSet<RefLocation> Locations { get; set; } = null!;
        public DbSet<RefLocationType> LocationTypes { get; set; } = null!;

        //TLM Book distribution
        public DbSet<TLMDistribution> TLMDistributions { get; set; } = null!;
        public DbSet<TLMDistributionDetail> TLMDistributionDetails { get; set; } = null!;
        public DbSet<TLMDistributionPeriod> TLMDistributionPeriods { get; set; } = null!;
        public DbSet<TLMMaterial> TLMMaterials { get; set; } = null!;

        //Program
        public DbSet<MEInsight.Entities.Programs.Program> Programs { get; set; } = null!;
        public DbSet<ProgramAssessment> ProgramAssessments { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<GroupEnrollment> GroupEnrollments { get; set; } = null!;
        public DbSet<GroupEvaluation> GroupEvaluations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            //Entities.Core
            builder.Entity<Organization>().HasQueryFilter(p => !p.IsDeleted).ToTable("Organization");

            builder.Entity<School>().ToTable("School");
            builder.Entity<Partner>().ToTable("Partner");

            builder.Entity<SchoolPeriod>().HasQueryFilter(p => !p.IsDeleted).ToTable("SchoolPeriod");
            builder.Entity<SchoolEnrollment>().HasQueryFilter(p => !p.IsDeleted).ToTable("SchoolEnrollment");
            builder.Entity<SchoolClassroom>().HasQueryFilter(p => !p.IsDeleted).ToTable("SchoolClassroom");
            builder.Entity<Participant>().HasQueryFilter(p => !p.IsDeleted).ToTable("Participant");

            builder.Entity<Student>().ToTable("Student");
            builder.Entity<Teacher>().ToTable("Teacher");
            builder.Entity<EducationAdministrator>().ToTable("EducationAdministrator");
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

            //Entities.Programs
            builder.Entity<MEInsight.Entities.Programs.Program>().HasQueryFilter(p => !p.IsDeleted).ToTable("Program");
            builder.Entity<ProgramAssessment>().HasQueryFilter(p => !p.IsDeleted).ToTable("ProgramAssessment");
            builder.Entity<Group>().HasQueryFilter(p => !p.IsDeleted).ToTable("Group");
            builder.Entity<GroupEnrollment>().HasQueryFilter(p => !p.IsDeleted).ToTable("GroupEnrollment");
            builder.Entity<GroupEnrollment>().HasIndex(t => t.ParticipantId);

            builder.Entity<GroupEvaluation>().HasQueryFilter(p => !p.IsDeleted).ToTable("GroupEvaluation");
            builder.Entity<GroupEvaluation>().HasIndex(t => t.GroupEnrollmentId);

        }
        //Override SaveChanges - enables Soft-Delete
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

            string? userName = null;
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                userName = httpContext.User?.Identity?.Name;
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
                            entry.CurrentValues["CreatedDate"] = entry.GetDatabaseValues()?.GetValue<object>("CreatedDate");
                            entry.CurrentValues["CreatedBy"] = entry.GetDatabaseValues()?.GetValue<object>("CreatedBy");
                            entry.CurrentValues["ModifiedDate"] = DateTimeOffset.UtcNow;
                            entry.CurrentValues["ModifiedBy"] = userName;

                            break;

                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                            entry.CurrentValues["CreatedDate"] = entry.GetDatabaseValues()?.GetValue<object>("CreatedDate");
                            entry.CurrentValues["CreatedBy"] = entry.GetDatabaseValues()?.GetValue<object>("CreatedBy");
                            entry.CurrentValues["ModifiedDate"] = entry.GetDatabaseValues()?.GetValue<object>("ModifiedDate");
                            entry.CurrentValues["ModifiedBy"] = entry.GetDatabaseValues()?.GetValue<object>("ModifiedBy");
                            entry.CurrentValues["DeletedDate"] = DateTimeOffset.UtcNow;
                            entry.CurrentValues["DeletedBy"] = userName;
                            break;
                    }
                }
            }
        }
    }
}
