using MEInsight.Entities.Core;
using MEInsight.Entities.Identity;
using MEInsight.Entities.Reference;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MEInsight.Web.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            /* *********************************************************************** */
            /*                  IMPORTANT - CHANGE ADMIN CREDENTIALS                   */
            /*                                                                         */
            /* *********************************************************************** */

            string adminUserName = "admin@meinsight.org";
            string adminPassword = "ME!Insight4Data";
            string adminOrganizationName = "Management Organization";
            string adminOrganizationCode = "MO01";

            Guid adminUserId = new("b6b0c340-9964-4ac8-a3ba-401a71544e78");
            Guid adminRoleId = new("460dd3ac-42f4-417d-9ed5-58eb2659601d");
            Guid readRoleId = new("7f398148-6a9c-4ccf-9e34-7a45499b952c");
            Guid createRoleId = new("f6c162a2-378d-4953-b099-e8e07bf362e8");
            Guid editRoleId = new("7a22b248-da2b-44ce-8e10-30aa2b43fae1");
            Guid deleteRoleId = new("1d8a40e6-c44c-4b32-9271-6d5c248abc33");
            Guid melOfficerRoleId = new("73c1fe42-1822-4674-aaeb-50acc84a1dfb");
            Guid melRoleId = new("16522b51-3d81-446e-ac98-a7a49cc93b12");
            Guid adminOrganizationId = new("ac69be75-b9bd-44cf-a6a4-fb5b5e328079");

            builder.Entity<RefOrganizationType>()
                .HasData(
                    new RefOrganizationType() { RefOrganizationTypeId = 1, OrganizationTypeCode = "OU", OrganizationType = "Organization Unit" },
                    new RefOrganizationType() { RefOrganizationTypeId = 2, OrganizationTypeCode = "School", OrganizationType = "School" }
                );

            builder.Entity<Organization>()
                .HasData(
                    new Organization
                    {
                        OrganizationId = adminOrganizationId,
                        RefOrganizationTypeId = 1,
                        OrganizationName = adminOrganizationName,
                        OrganizationCode = adminOrganizationCode,
                        CreatedBy = adminUserName,
                        CreatedDate = DateTime.UtcNow,
                        RegistrationDate = DateTime.UtcNow,
                        IsTenant = true,
                        IsOrganizationUnit = true
                    }
                );

            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = adminUserName,
                Email = adminUserName,
                NormalizedUserName = adminUserName.ToUpper(),
                ConcurrencyStamp = adminUserId.ToString(),
                SecurityStamp = adminUserId.ToString(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                OrganizationId = adminOrganizationId
            };

            PasswordHasher<ApplicationUser> password = new();
            adminUser.PasswordHash = password.HashPassword(adminUser, adminPassword);

            builder.Entity<ApplicationUser>().HasData(adminUser);

            builder.Entity<ApplicationRole>()
                .HasData(
                    new ApplicationRole() { Id = adminRoleId, Name = "Administrator", Description = "Administrator Role" },
                    new ApplicationRole() { Id = readRoleId, Name = "Read", Description = "Read only Role" },
                    new ApplicationRole() { Id = createRoleId, Name = "Create", Description = "Create only Role" },
                    new ApplicationRole() { Id = editRoleId, Name = "Edit", Description = "Create and Edit Role" },
                    new ApplicationRole() { Id = deleteRoleId, Name = "Delete", Description = "Create, Edit, and Delete Role" },
                    new ApplicationRole() { Id = melOfficerRoleId, Name = "MELOfficer", Description = "Monitoring, Evaluation and Learning Officer Role" },
                    new ApplicationRole() { Id = melRoleId, Name = "MEL", Description = "Monitoring, Evaluation and Learning Role" }
                );

            builder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.HasData(
                    new IdentityUserRole<Guid>
                    {
                        RoleId = adminRoleId,
                        UserId = adminUserId,
                    });
                entity.HasKey(x => new { x.RoleId, x.UserId });
            });

            builder.Entity<RefParticipantType>()
                .HasData(
                    new RefParticipantType { RefParticipantTypeId = 1, ParticipantTypeCode = "ST", ParticipantType = "Student" },
                    new RefParticipantType { RefParticipantTypeId = 2, ParticipantTypeCode = "TE", ParticipantType = "Teacher" },
                    new RefParticipantType { RefParticipantTypeId = 3, ParticipantTypeCode = "EA", ParticipantType = "Education Administrator" }
                );

            builder.Entity<RefSex>()
                .HasData(
                    new RefSex { RefSexId = 1, SexId = "M", Sex = "Male" },
                    new RefSex { RefSexId = 2, SexId = "F", Sex = "Female" }
                );

            builder.Entity<RefEnrollmentStatus>()
                .HasData(
                    new RefEnrollmentStatus { RefEnrollmentStatusId = 1, EnrollmentStatusCode = "E", EnrollmentStatus = "Enrolled" },
                    new RefEnrollmentStatus { RefEnrollmentStatusId = 2, EnrollmentStatusCode = "C", EnrollmentStatus = "Completed" },
                    new RefEnrollmentStatus { RefEnrollmentStatusId = 3, EnrollmentStatusCode = "D", EnrollmentStatus = "Dropped out" }
                );

            builder.Entity<RefEvaluationStatus>()
                .HasData(
                    new RefEvaluationStatus { RefEvaluationStatusId = 1, EvaluationStatusCode = "E", EvaluationStatus = "Enrolled" },
                    new RefEvaluationStatus { RefEvaluationStatusId = 2, EvaluationStatusCode = "C", EvaluationStatus = "Completed" }
                );

            builder.Entity<RefAttendanceUnit>()
                .HasData(
                    new RefAttendanceUnit { RefAttendanceUnitId = 1, AttendanceUnitCode = "Hour", AttendanceUnit = "Hours", AttendanceUnitId = "h" },
                    new RefAttendanceUnit { RefAttendanceUnitId = 2, AttendanceUnitCode = "Day", AttendanceUnit = "Days", AttendanceUnitId = "d" }
                );

            builder.Entity<RefProgramType>()
                .HasData(
                    new RefProgramType { RefProgramTypeId = 1, ProgramTypeCode = "D01", ProgramType = "Default Program Type" }
                );

            builder.Entity<MEInsight.Entities.Programs.Program>()
                .HasData(
                    new MEInsight.Entities.Programs.Program { ProgramId = 1, RefProgramTypeId = 1, RefAttendanceUnitId = 1, ProgramName = "Default Program" }
                );
        }
    }
}
