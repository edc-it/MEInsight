using MEInsight.Entities;
using MEInsight.Entities.Core;
using MEInsight.Entities.Identity;
using MEInsight.Entities.Programs;
using MEInsight.Entities.Reference;
using MEInsight.Entities.TLM;
using MEInsight.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

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

        // JSON NoSQL Hybrid Participant
        public DbSet<ParticipantData> ParticipantData { get; set; } = null!;
        public DbSet<RefParticipantDataSource> ParticipantDataSources { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            //Entities.Core
            builder.Entity<Organization>().HasQueryFilter(p => !p.IsDeleted);

            builder.Entity<SchoolPeriod>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<SchoolEnrollment>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<SchoolClassroom>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Participant>().HasQueryFilter(p => !p.IsDeleted);


            //Entities.TLM
            builder.Entity<TLMDistribution>().HasQueryFilter(p => !p.IsDeleted);

            builder.Entity<TLMDistribution>()
                .HasOne(m => m.OrganizationsFrom)
                .WithMany(t => t.TLMDistributionsFrom)
                .HasForeignKey(m => m.OrganizationIdFrom);

            builder.Entity<TLMDistribution>()
                .HasOne(m => m.OrganizationsTo)
                .WithMany(t => t.TLMDistributionsTo)
                .HasForeignKey(m => m.OrganizationIdTo);

            builder.Entity<TLMDistributionDetail>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<TLMDistributionPeriod>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<TLMMaterial>().HasQueryFilter(p => !p.IsDeleted);

            //Entities.Programs
            builder.Entity<MEInsight.Entities.Programs.Program>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<ProgramAssessment>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Group>().HasQueryFilter(p => !p.IsDeleted);

            builder.Entity<GroupEnrollment>(entity =>
            {
                entity.HasQueryFilter(p => !p.IsDeleted);
                entity.HasIndex(t => t.ParticipantId);

                entity.HasOne(d => d.Groups)
                    .WithMany(p => p.GroupEnrollments)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            builder.Entity<GroupEvaluation>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<GroupEvaluation>().HasIndex(t => t.GroupEnrollmentId);

            builder.Entity<ParticipantData>()
                .HasQueryFilter(p => !p.IsDeleted)
                .Property(b => b.ExtendedData).HasColumnName("Data");

            builder.Entity<RefParticipantDataSource>();

            builder.Entity<ApplicationUser>().HasQueryFilter(p => !p.IsDeleted);

            // Seed database
            builder.Seed();

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
                            //Soft delete cascade
                            entry.CurrentValues["IsDeleted"] = true;
                            entry.CurrentValues["CreatedDate"] = entry.GetDatabaseValues()?.GetValue<object>("CreatedDate");
                            entry.CurrentValues["CreatedBy"] = entry.GetDatabaseValues()?.GetValue<object>("CreatedBy");
                            entry.CurrentValues["ModifiedDate"] = entry.GetDatabaseValues()?.GetValue<object>("ModifiedDate");
                            entry.CurrentValues["ModifiedBy"] = entry.GetDatabaseValues()?.GetValue<object>("ModifiedBy");
                            entry.CurrentValues["DeletedDate"] = DateTimeOffset.UtcNow;
                            entry.CurrentValues["DeletedBy"] = userName;
                            foreach (var navigationEntry in entry.Navigations.Where(x => !((IReadOnlyNavigation)x.Metadata).IsOnDependent))
                            {
                                if (navigationEntry is CollectionEntry collectionEntry)
                                {
                                    foreach (var dependentEntry in collectionEntry.CurrentValue!)
                                    {
                                        HandleDependent(Entry(dependentEntry));
                                    }
                                }
                                else
                                {
                                    var dependentEntry = navigationEntry.CurrentValue;
                                    if (dependentEntry != null)
                                    {
                                        HandleDependent(Entry(dependentEntry));
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        private static void HandleDependent(EntityEntry entry)
        {
            entry.CurrentValues["IsDeleted"] = true;
        }
    }
}
