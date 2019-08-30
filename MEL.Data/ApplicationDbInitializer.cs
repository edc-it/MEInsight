using MEL.Entities;
using MEL.Entities.Core;
using MEL.Entities.Identity;
using MEL.Entities.Programs;
using MEL.Entities.Reference;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEL.Data
{
    public class ApplicationDbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
            var _context = serviceProvider.GetService<ApplicationDbContext>();


            /* *********************************************************************** */
            /*                  IMPORTANT - CHANGE ADMIN CREDENTIALS                   */
            /*                                                                         */
            /* *********************************************************************** */
            const string SeedUserName = "admin@meinsight.edc.org";
            string tempAdminPassword = "ME!Insight4Data";


            // Seed database
			// Create Organization Types (required to create the default Organization)
			if (!_context.OrganizationTypes.Any())
			{
				var refOrganizationType = new RefOrganizationType[]
				{
					new RefOrganizationType{ RefOrganizationTypeId = 1, OrganizationTypeCode = "OU", OrganizationType = "Organization Unit" },
					new RefOrganizationType{ RefOrganizationTypeId = 2, OrganizationTypeCode = "School", OrganizationType = "School" }
				};

				foreach (RefOrganizationType item in refOrganizationType)
				{
					_context.OrganizationTypes.Add(item);
				}

                // SET IDENTITY_INSERT requires ALTER TABLE permissions on SQL server
                await _context.Database.OpenConnectionAsync();
                try
                {
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefOrganizationType ON");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefOrganizationType OFF");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }

			// Create Default Organization
			if (!_context.Organizations.Any())
			{
				var organization = new Organization[]
				{
					new Organization {
						OrganizationId = new Guid("9c1f1204-62d7-46a2-b5b0-a94affa54b22"),
						RefOrganizationTypeId = 1,
						OrganizationName = "Management Organization",
						OrganizationCode = "MO01",
						CreatedBy = "admin",
						CreatedDate = DateTime.Now,
						IsTenant = true,
						IsOrganizationUnit = true

					}

				};

				foreach (Organization item in organization)
				{
					_context.Organizations.Add(item);
				}
				await _context.SaveChangesAsync();
			}

            //Add Application Roles
            List<ApplicationRole> applicationRole = new List<ApplicationRole>
            {
                new ApplicationRole() { Name = "Administrator", Description = "DBMS Administrator Role" },
                new ApplicationRole() { Name = "Read", Description = "Read only Role" },
                new ApplicationRole() { Name = "Create", Description = "Create only Role" },
                new ApplicationRole() { Name = "Edit", Description = "Create and Edit Role" },
                new ApplicationRole() { Name = "Delete", Description = "Create, Edit, and Delete Role" },
                new ApplicationRole() { Name = "MELOfficer", Description = "Monitoring, Evaluation and Learning Officer Role" },
                new ApplicationRole() { Name = "MEL", Description = "Monitoring, Evaluation and Learning Role" }
            };

            foreach (var role in applicationRole)
            {
                var roleExists = await roleManager.FindByNameAsync(role.Name);
                if (roleExists == null)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            // Create Administrator User
            var user = await userManager.FindByNameAsync(SeedUserName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = SeedUserName,
                    FirstName = "Admin",
                    LastName = "",
                    OrganizationId = new Guid("9c1f1204-62d7-46a2-b5b0-a94affa54b22"),
                    CreatedDate = DateTime.Now,
                    CreatedBy = "system",
                    Email = SeedUserName,
                };
                await userManager.CreateAsync(user, tempAdminPassword);
            }

            // Add Admin User to Administrator Role
            var isInRole = await userManager.IsInRoleAsync(user, "Administrator");
            if (!isInRole)
            {
                await userManager.AddToRoleAsync(user, "Administrator");
            }

            //ParticipantTypes
            if (!_context.ParticipantTypes.Any())
            {
                var refParticipantType = new RefParticipantType[]
                {
                    new RefParticipantType{ RefParticipantTypeId = 1, ParticipantTypeCode = "ST", ParticipantType = "Student" },
                    new RefParticipantType{ RefParticipantTypeId = 2, ParticipantTypeCode = "TE", ParticipantType = "Teacher" },
                    new RefParticipantType{ RefParticipantTypeId = 3, ParticipantTypeCode = "EA", ParticipantType = "Education Administrator" }
                };

                foreach (RefParticipantType item in refParticipantType)
                {
                    _context.ParticipantTypes.Add(item);
                }

                await _context.Database.OpenConnectionAsync();
                try
                {
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefParticipantType ON");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefParticipantType OFF");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }

            //Sex
            if (!_context.Sex.Any())
            {
                var refSex = new RefSex[]
                {
                    new RefSex{ RefSexId = 1, SexId = "M", Sex = "Male" },
                    new RefSex{ RefSexId = 2, SexId = "F", Sex = "Female" },
                };

                foreach (RefSex item in refSex)
                {
                    _context.Sex.Add(item);
                }
                await _context.SaveChangesAsync();
            }

            //EnrollmentStatus
            if (!_context.EnrollmentStatus.Any())
            {
                var refEnrollmentStatus = new RefEnrollmentStatus[]
                {
                    new RefEnrollmentStatus{ RefEnrollmentStatusId = 1, EnrollmentStatusCode = "E", EnrollmentStatus = "Enrolled" },
                    new RefEnrollmentStatus{ RefEnrollmentStatusId = 2, EnrollmentStatusCode = "C", EnrollmentStatus = "Completed" },
                    new RefEnrollmentStatus{ RefEnrollmentStatusId = 3, EnrollmentStatusCode = "C", EnrollmentStatus = "Dropped out" }
                };

                foreach (RefEnrollmentStatus item in refEnrollmentStatus)
                {
                    _context.EnrollmentStatus.Add(item);
                }

                await _context.Database.OpenConnectionAsync();
                try
                {
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefEnrollmentStatus ON");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefEnrollmentStatus OFF");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }

            //EvaluationStatus
            if (!_context.EvaluationStatus.Any())
            {
                var refEvaluationStatus = new RefEvaluationStatus[]
                {
                    new RefEvaluationStatus{ RefEvaluationStatusId = 1, EvaluationStatusCode = "E", EvaluationStatus = "Enrolled" },
                    new RefEvaluationStatus{ RefEvaluationStatusId = 2, EvaluationStatusCode = "C", EvaluationStatus = "Completed" }
                };

                foreach (RefEvaluationStatus item in refEvaluationStatus)
                {
                    _context.EvaluationStatus.Add(item);
                }

                await _context.Database.OpenConnectionAsync();
                try
                {
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefEvaluationStatus ON");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefEvaluationStatus OFF");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }

            //Attendance Units
            if (!_context.AttendanceUnits.Any())
            {
                var refAttendanceUnits = new RefAttendanceUnit[]
                {
                    new RefAttendanceUnit{ RefAttendanceUnitId = 1, AttendanceUnitCode = "Hour", AttendanceUnit = "Hours", AttendanceUnitId = "h" },
                    new RefAttendanceUnit{ RefAttendanceUnitId = 2, AttendanceUnitCode = "Day", AttendanceUnit = "Days", AttendanceUnitId = "d" }
                };

                foreach (RefAttendanceUnit item in refAttendanceUnits)
                {
                    _context.AttendanceUnits.Add(item);
                }

                await _context.Database.OpenConnectionAsync();
                try
                {
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefAttendanceUnit ON");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT dbo.RefAttendanceUnit OFF");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }
        }
    }
}
