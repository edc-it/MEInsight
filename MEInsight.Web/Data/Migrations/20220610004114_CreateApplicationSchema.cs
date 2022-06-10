using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MEInsight.Web.Data.Migrations
{
    public partial class CreateApplicationSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefAssessmentType",
                columns: table => new
                {
                    RefAssessmentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AssessmentType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAssessmentType", x => x.RefAssessmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefAttendanceUnit",
                columns: table => new
                {
                    RefAttendanceUnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendanceUnitCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AttendanceUnit = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AttendanceUnitId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAttendanceUnit", x => x.RefAttendanceUnitId);
                });

            migrationBuilder.CreateTable(
                name: "RefDisabilityType",
                columns: table => new
                {
                    RefDisabilityTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisabilityTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DisabilityType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefDisabilityType", x => x.RefDisabilityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefEducationAdministratorOffice",
                columns: table => new
                {
                    RefEducationAdministratorOfficeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationAdministratorOfficeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EducationAdministratorOffice = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEducationAdministratorOffice", x => x.RefEducationAdministratorOfficeId);
                });

            migrationBuilder.CreateTable(
                name: "RefEducationAdministratorPosition",
                columns: table => new
                {
                    RefEducationAdministratorPositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationAdministratorPositionCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EducationAdministratorPosition = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEducationAdministratorPosition", x => x.RefEducationAdministratorPositionId);
                });

            migrationBuilder.CreateTable(
                name: "RefEducationAdministratorType",
                columns: table => new
                {
                    RefEducationAdministratorTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationAdministratorTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EducationAdministratorType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEducationAdministratorType", x => x.RefEducationAdministratorTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefEnrollmentStatus",
                columns: table => new
                {
                    RefEnrollmentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentStatusCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EnrollmentStatus = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEnrollmentStatus", x => x.RefEnrollmentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RefEvaluationStatus",
                columns: table => new
                {
                    RefEvaluationStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationStatusCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EvaluationStatus = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEvaluationStatus", x => x.RefEvaluationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RefGradeLevel",
                columns: table => new
                {
                    RefGradeLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeLevelCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    GradeLevel = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    GradeLevelId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefGradeLevel", x => x.RefGradeLevelId);
                });

            migrationBuilder.CreateTable(
                name: "RefLocationType",
                columns: table => new
                {
                    RefLocationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LocationType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LocationLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefLocationType", x => x.RefLocationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefOrganizationType",
                columns: table => new
                {
                    RefOrganizationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    OrganizationTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefOrganizationType", x => x.RefOrganizationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefParticipantCohort",
                columns: table => new
                {
                    RefParticipantCohortId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantCohortCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ParticipantCohort = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefParticipantCohort", x => x.RefParticipantCohortId);
                });

            migrationBuilder.CreateTable(
                name: "RefParticipantType",
                columns: table => new
                {
                    RefParticipantTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ParticipantType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefParticipantType", x => x.RefParticipantTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefPartnerSector",
                columns: table => new
                {
                    RefPartnerSectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartnerSectorCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PartnerSector = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefPartnerSector", x => x.RefPartnerSectorId);
                });

            migrationBuilder.CreateTable(
                name: "RefPartnerType",
                columns: table => new
                {
                    RefPartnerTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartnerTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PartnerType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefPartnerType", x => x.RefPartnerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefProgramDeliveryType",
                columns: table => new
                {
                    RefProgramDeliveryTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramDeliveryTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ProgramDeliveryType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefProgramDeliveryType", x => x.RefProgramDeliveryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefProgramType",
                columns: table => new
                {
                    RefProgramTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ProgramType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefProgramType", x => x.RefProgramTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolAdministrationType",
                columns: table => new
                {
                    RefSchoolAdministrationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolAdministrationTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SchoolAdministrationType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolAdministrationType", x => x.RefSchoolAdministrationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolLanguage",
                columns: table => new
                {
                    RefSchoolLanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolLanguageCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SchoolLanguage = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolLanguage", x => x.RefSchoolLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolLocation",
                columns: table => new
                {
                    RefSchoolLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolLocationCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SchoolLocation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolLocation", x => x.RefSchoolLocationId);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolStatus",
                columns: table => new
                {
                    RefSchoolStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolStatusCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SchoolStatus = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolStatus", x => x.RefSchoolStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolType",
                columns: table => new
                {
                    RefSchoolTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SchoolType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolType", x => x.RefSchoolTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefSex",
                columns: table => new
                {
                    RefSexId = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SexId = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSex", x => x.RefSexId);
                });

            migrationBuilder.CreateTable(
                name: "RefStudentSpecialization",
                columns: table => new
                {
                    RefStudentSpecializationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentSpecializationCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    StudentSpecialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefStudentSpecialization", x => x.RefStudentSpecializationId);
                });

            migrationBuilder.CreateTable(
                name: "RefStudentType",
                columns: table => new
                {
                    RefStudentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    StudentType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefStudentType", x => x.RefStudentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefStudentYearOfStudy",
                columns: table => new
                {
                    RefStudentYearOfStudyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentYearOfStudyCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    StudentYearOfStudy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefStudentYearOfStudy", x => x.RefStudentYearOfStudyId);
                });

            migrationBuilder.CreateTable(
                name: "RefTeacherEmploymentType",
                columns: table => new
                {
                    RefTeacherEmploymentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherEmploymentTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TeacherEmploymentType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTeacherEmploymentType", x => x.RefTeacherEmploymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefTeacherPosition",
                columns: table => new
                {
                    RefTeacherPositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherPositionCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TeacherPosition = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTeacherPosition", x => x.RefTeacherPositionId);
                });

            migrationBuilder.CreateTable(
                name: "RefTeacherType",
                columns: table => new
                {
                    RefTeacherTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TeacherType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTeacherType", x => x.RefTeacherTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMDistributionStatus",
                columns: table => new
                {
                    RefTLMDistributionStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributionStatusCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DistributionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMDistributionStatus", x => x.RefTLMDistributionStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMGroup",
                columns: table => new
                {
                    RefTLMGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TLMGroupCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TLMGroup = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMGroup", x => x.RefTLMGroupId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMLanguage",
                columns: table => new
                {
                    RefTLMLanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TLMLanguageCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TLMLanguage = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMLanguage", x => x.RefTLMLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMMaterialSet",
                columns: table => new
                {
                    RefTLMMaterialSetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TLMMaterialSetCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TLMMaterialSet = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMMaterialSet", x => x.RefTLMMaterialSetId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMMaterialType",
                columns: table => new
                {
                    RefTLMMaterialTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TLMMaterialTypeCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TLMMaterialType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMMaterialType", x => x.RefTLMMaterialTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMSubject",
                columns: table => new
                {
                    RefTLMSubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TLMSubjectCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TLMSubject = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMSubject", x => x.RefTLMSubjectId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolPeriod",
                columns: table => new
                {
                    SchoolPeriodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolPeriod", x => x.SchoolPeriodId);
                });

            migrationBuilder.CreateTable(
                name: "TLMDistributionPeriod",
                columns: table => new
                {
                    TLMDistributionPeriodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Closed = table.Column<bool>(type: "bit", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLMDistributionPeriod", x => x.TLMDistributionPeriodId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefLocation",
                columns: table => new
                {
                    RefLocationId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RefLocationTypeId = table.Column<int>(type: "int", nullable: false),
                    ParentLocationId = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefLocation", x => x.RefLocationId);
                    table.ForeignKey(
                        name: "FK_RefLocation_RefLocation_ParentLocationId",
                        column: x => x.ParentLocationId,
                        principalTable: "RefLocation",
                        principalColumn: "RefLocationId");
                    table.ForeignKey(
                        name: "FK_RefLocation_RefLocationType_RefLocationTypeId",
                        column: x => x.RefLocationTypeId,
                        principalTable: "RefLocationType",
                        principalColumn: "RefLocationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RefProgramTypeId = table.Column<int>(type: "int", nullable: true),
                    RefProgramDeliveryTypeId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Max = table.Column<int>(type: "int", nullable: true),
                    Min = table.Column<int>(type: "int", nullable: true),
                    RefAttendanceUnitId = table.Column<int>(type: "int", nullable: true),
                    HasAssessment = table.Column<bool>(type: "bit", nullable: false),
                    DisplayMarks = table.Column<bool>(type: "bit", nullable: false),
                    RefOrganizationTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.ProgramId);
                    table.ForeignKey(
                        name: "FK_Program_RefAttendanceUnit_RefAttendanceUnitId",
                        column: x => x.RefAttendanceUnitId,
                        principalTable: "RefAttendanceUnit",
                        principalColumn: "RefAttendanceUnitId");
                    table.ForeignKey(
                        name: "FK_Program_RefOrganizationType_RefOrganizationTypeId",
                        column: x => x.RefOrganizationTypeId,
                        principalTable: "RefOrganizationType",
                        principalColumn: "RefOrganizationTypeId");
                    table.ForeignKey(
                        name: "FK_Program_RefProgramDeliveryType_RefProgramDeliveryTypeId",
                        column: x => x.RefProgramDeliveryTypeId,
                        principalTable: "RefProgramDeliveryType",
                        principalColumn: "RefProgramDeliveryTypeId");
                    table.ForeignKey(
                        name: "FK_Program_RefProgramType_RefProgramTypeId",
                        column: x => x.RefProgramTypeId,
                        principalTable: "RefProgramType",
                        principalColumn: "RefProgramTypeId");
                });

            migrationBuilder.CreateTable(
                name: "TLMMaterial",
                columns: table => new
                {
                    TLMMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TLMMaterialCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TLMMaterialName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(384)", maxLength: 384, nullable: true),
                    RefTLMMaterialTypeId = table.Column<int>(type: "int", nullable: true),
                    RefGradeLevelId = table.Column<int>(type: "int", nullable: true),
                    RefTLMLanguageId = table.Column<int>(type: "int", nullable: true),
                    RefTLMSubjectId = table.Column<int>(type: "int", nullable: true),
                    RefTLMGroupId = table.Column<int>(type: "int", nullable: true),
                    RefTLMMaterialSetId = table.Column<int>(type: "int", nullable: true),
                    RatioNumerator = table.Column<int>(type: "int", nullable: true),
                    RatioDenominator = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLMMaterial", x => x.TLMMaterialId);
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefGradeLevel_RefGradeLevelId",
                        column: x => x.RefGradeLevelId,
                        principalTable: "RefGradeLevel",
                        principalColumn: "RefGradeLevelId");
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMGroup_RefTLMGroupId",
                        column: x => x.RefTLMGroupId,
                        principalTable: "RefTLMGroup",
                        principalColumn: "RefTLMGroupId");
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMLanguage_RefTLMLanguageId",
                        column: x => x.RefTLMLanguageId,
                        principalTable: "RefTLMLanguage",
                        principalColumn: "RefTLMLanguageId");
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMMaterialSet_RefTLMMaterialSetId",
                        column: x => x.RefTLMMaterialSetId,
                        principalTable: "RefTLMMaterialSet",
                        principalColumn: "RefTLMMaterialSetId");
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMMaterialType_RefTLMMaterialTypeId",
                        column: x => x.RefTLMMaterialTypeId,
                        principalTable: "RefTLMMaterialType",
                        principalColumn: "RefTLMMaterialTypeId");
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMSubject_RefTLMSubjectId",
                        column: x => x.RefTLMSubjectId,
                        principalTable: "RefTLMSubject",
                        principalColumn: "RefTLMSubjectId");
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizationCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RefOrganizationTypeId = table.Column<int>(type: "int", nullable: false),
                    ParentOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    RefLocationId = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(384)", maxLength: 384, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    IsOrganizationUnit = table.Column<bool>(type: "bit", nullable: false),
                    IsTenant = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organization_Organization_ParentOrganizationId",
                        column: x => x.ParentOrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_Organization_RefLocation_RefLocationId",
                        column: x => x.RefLocationId,
                        principalTable: "RefLocation",
                        principalColumn: "RefLocationId");
                    table.ForeignKey(
                        name: "FK_Organization_RefOrganizationType_RefOrganizationTypeId",
                        column: x => x.RefOrganizationTypeId,
                        principalTable: "RefOrganizationType",
                        principalColumn: "RefOrganizationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolCluster",
                columns: table => new
                {
                    RefSchoolClusterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolClusterCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    SchoolCluster = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RefLocationId = table.Column<string>(type: "nvarchar(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolCluster", x => x.RefSchoolClusterId);
                    table.ForeignKey(
                        name: "FK_RefSchoolCluster_RefLocation_RefLocationId",
                        column: x => x.RefLocationId,
                        principalTable: "RefLocation",
                        principalColumn: "RefLocationId");
                });

            migrationBuilder.CreateTable(
                name: "ProgramAssessment",
                columns: table => new
                {
                    ProgramAssessmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    AssessmentName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RefAssessmentTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrackAttendance = table.Column<bool>(type: "bit", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: true),
                    Min = table.Column<int>(type: "int", nullable: true),
                    RefAttendanceUnitId = table.Column<int>(type: "int", nullable: true),
                    MaximumScore = table.Column<int>(type: "int", nullable: true),
                    MinimumScore = table.Column<int>(type: "int", nullable: true),
                    CompletionScore = table.Column<int>(type: "int", nullable: true),
                    RefEvaluationStatusId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramAssessment", x => x.ProgramAssessmentId);
                    table.ForeignKey(
                        name: "FK_ProgramAssessment_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramAssessment_RefAssessmentType_RefAssessmentTypeId",
                        column: x => x.RefAssessmentTypeId,
                        principalTable: "RefAssessmentType",
                        principalColumn: "RefAssessmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramAssessment_RefAttendanceUnit_RefAttendanceUnitId",
                        column: x => x.RefAttendanceUnitId,
                        principalTable: "RefAttendanceUnit",
                        principalColumn: "RefAttendanceUnitId");
                    table.ForeignKey(
                        name: "FK_ProgramAssessment_RefEvaluationStatus_RefEvaluationStatusId",
                        column: x => x.RefEvaluationStatusId,
                        principalTable: "RefEvaluationStatus",
                        principalColumn: "RefEvaluationStatusId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefParticipantTypeId = table.Column<int>(type: "int", nullable: false),
                    RefParticipantCohortId = table.Column<int>(type: "int", nullable: true),
                    ParticipantCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    RefSexId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Disability = table.Column<bool>(type: "bit", nullable: true),
                    RefDisabilityTypeId = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    InstantMessenger = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RefLocationId = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(384)", maxLength: 384, nullable: true),
                    AdditionalData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Participant_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participant_RefDisabilityType_RefDisabilityTypeId",
                        column: x => x.RefDisabilityTypeId,
                        principalTable: "RefDisabilityType",
                        principalColumn: "RefDisabilityTypeId");
                    table.ForeignKey(
                        name: "FK_Participant_RefLocation_RefLocationId",
                        column: x => x.RefLocationId,
                        principalTable: "RefLocation",
                        principalColumn: "RefLocationId");
                    table.ForeignKey(
                        name: "FK_Participant_RefParticipantCohort_RefParticipantCohortId",
                        column: x => x.RefParticipantCohortId,
                        principalTable: "RefParticipantCohort",
                        principalColumn: "RefParticipantCohortId");
                    table.ForeignKey(
                        name: "FK_Participant_RefParticipantType_RefParticipantTypeId",
                        column: x => x.RefParticipantTypeId,
                        principalTable: "RefParticipantType",
                        principalColumn: "RefParticipantTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participant_RefSex_RefSexId",
                        column: x => x.RefSexId,
                        principalTable: "RefSex",
                        principalColumn: "RefSexId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartnerCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RefPartnerTypeId = table.Column<int>(type: "int", nullable: true),
                    RefPartnerSectorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Partner_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_Partner_RefPartnerSector_RefPartnerSectorId",
                        column: x => x.RefPartnerSectorId,
                        principalTable: "RefPartnerSector",
                        principalColumn: "RefPartnerSectorId");
                    table.ForeignKey(
                        name: "FK_Partner_RefPartnerType_RefPartnerTypeId",
                        column: x => x.RefPartnerTypeId,
                        principalTable: "RefPartnerType",
                        principalColumn: "RefPartnerTypeId");
                });

            migrationBuilder.CreateTable(
                name: "TLMDistribution",
                columns: table => new
                {
                    TLMDistributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TLMDistributionPeriodId = table.Column<int>(type: "int", nullable: false),
                    OrganizationIdFrom = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationIdTo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceivedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrackingCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RefTLMDistributionStatusId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentTLMDistributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLMDistribution", x => x.TLMDistributionId);
                    table.ForeignKey(
                        name: "FK_TLMDistribution_Organization_OrganizationIdFrom",
                        column: x => x.OrganizationIdFrom,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_TLMDistribution_Organization_OrganizationIdTo",
                        column: x => x.OrganizationIdTo,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_TLMDistribution_RefTLMDistributionStatus_RefTLMDistributionStatusId",
                        column: x => x.RefTLMDistributionStatusId,
                        principalTable: "RefTLMDistributionStatus",
                        principalColumn: "RefTLMDistributionStatusId");
                    table.ForeignKey(
                        name: "FK_TLMDistribution_TLMDistribution_ParentTLMDistributionId",
                        column: x => x.ParentTLMDistributionId,
                        principalTable: "TLMDistribution",
                        principalColumn: "TLMDistributionId");
                    table.ForeignKey(
                        name: "FK_TLMDistribution_TLMDistributionPeriod_TLMDistributionPeriodId",
                        column: x => x.TLMDistributionPeriodId,
                        principalTable: "TLMDistributionPeriod",
                        principalColumn: "TLMDistributionPeriodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RefSchoolTypeId = table.Column<int>(type: "int", nullable: true),
                    RefSchoolLocationId = table.Column<int>(type: "int", nullable: true),
                    RefSchoolLanguageId = table.Column<int>(type: "int", nullable: true),
                    RefSchoolAdministrationTypeId = table.Column<int>(type: "int", nullable: true),
                    PartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsClusterCenter = table.Column<bool>(type: "bit", nullable: true),
                    RefSchoolClusterId = table.Column<int>(type: "int", nullable: true),
                    RefSchoolStatusId = table.Column<int>(type: "int", nullable: true),
                    HeadTeacher = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_School_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_School_RefSchoolAdministrationType_RefSchoolAdministrationTypeId",
                        column: x => x.RefSchoolAdministrationTypeId,
                        principalTable: "RefSchoolAdministrationType",
                        principalColumn: "RefSchoolAdministrationTypeId");
                    table.ForeignKey(
                        name: "FK_School_RefSchoolCluster_RefSchoolClusterId",
                        column: x => x.RefSchoolClusterId,
                        principalTable: "RefSchoolCluster",
                        principalColumn: "RefSchoolClusterId");
                    table.ForeignKey(
                        name: "FK_School_RefSchoolLanguage_RefSchoolLanguageId",
                        column: x => x.RefSchoolLanguageId,
                        principalTable: "RefSchoolLanguage",
                        principalColumn: "RefSchoolLanguageId");
                    table.ForeignKey(
                        name: "FK_School_RefSchoolLocation_RefSchoolLocationId",
                        column: x => x.RefSchoolLocationId,
                        principalTable: "RefSchoolLocation",
                        principalColumn: "RefSchoolLocationId");
                    table.ForeignKey(
                        name: "FK_School_RefSchoolStatus_RefSchoolStatusId",
                        column: x => x.RefSchoolStatusId,
                        principalTable: "RefSchoolStatus",
                        principalColumn: "RefSchoolStatusId");
                    table.ForeignKey(
                        name: "FK_School_RefSchoolType_RefSchoolTypeId",
                        column: x => x.RefSchoolTypeId,
                        principalTable: "RefSchoolType",
                        principalColumn: "RefSchoolTypeId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationAdministrator",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefEducationAdministratorTypeId = table.Column<int>(type: "int", nullable: true),
                    RefEducationAdministratorPositionId = table.Column<int>(type: "int", nullable: true),
                    RefEducationAdministratorOfficeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAdministrator", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_EducationAdministrator_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "ParticipantId");
                    table.ForeignKey(
                        name: "FK_EducationAdministrator_RefEducationAdministratorOffice_RefEducationAdministratorOfficeId",
                        column: x => x.RefEducationAdministratorOfficeId,
                        principalTable: "RefEducationAdministratorOffice",
                        principalColumn: "RefEducationAdministratorOfficeId");
                    table.ForeignKey(
                        name: "FK_EducationAdministrator_RefEducationAdministratorPosition_RefEducationAdministratorPositionId",
                        column: x => x.RefEducationAdministratorPositionId,
                        principalTable: "RefEducationAdministratorPosition",
                        principalColumn: "RefEducationAdministratorPositionId");
                    table.ForeignKey(
                        name: "FK_EducationAdministrator_RefEducationAdministratorType_RefEducationAdministratorTypeId",
                        column: x => x.RefEducationAdministratorTypeId,
                        principalTable: "RefEducationAdministratorType",
                        principalColumn: "RefEducationAdministratorTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RefStudentTypeId = table.Column<int>(type: "int", nullable: true),
                    RefStudentSpecializationId = table.Column<int>(type: "int", nullable: true),
                    ParentGuardian = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RefStudentYearOfStudyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Student_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "ParticipantId");
                    table.ForeignKey(
                        name: "FK_Student_RefStudentSpecialization_RefStudentSpecializationId",
                        column: x => x.RefStudentSpecializationId,
                        principalTable: "RefStudentSpecialization",
                        principalColumn: "RefStudentSpecializationId");
                    table.ForeignKey(
                        name: "FK_Student_RefStudentType_RefStudentTypeId",
                        column: x => x.RefStudentTypeId,
                        principalTable: "RefStudentType",
                        principalColumn: "RefStudentTypeId");
                    table.ForeignKey(
                        name: "FK_Student_RefStudentYearOfStudy_RefStudentYearOfStudyId",
                        column: x => x.RefStudentYearOfStudyId,
                        principalTable: "RefStudentYearOfStudy",
                        principalColumn: "RefStudentYearOfStudyId");
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefTeacherTypeId = table.Column<int>(type: "int", nullable: true),
                    RefTeacherPositionId = table.Column<int>(type: "int", nullable: true),
                    RefTeacherEmploymentTypeId = table.Column<int>(type: "int", nullable: true),
                    GradeLevels = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Teacher_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "ParticipantId");
                    table.ForeignKey(
                        name: "FK_Teacher_RefTeacherEmploymentType_RefTeacherEmploymentTypeId",
                        column: x => x.RefTeacherEmploymentTypeId,
                        principalTable: "RefTeacherEmploymentType",
                        principalColumn: "RefTeacherEmploymentTypeId");
                    table.ForeignKey(
                        name: "FK_Teacher_RefTeacherPosition_RefTeacherPositionId",
                        column: x => x.RefTeacherPositionId,
                        principalTable: "RefTeacherPosition",
                        principalColumn: "RefTeacherPositionId");
                    table.ForeignKey(
                        name: "FK_Teacher_RefTeacherType_RefTeacherTypeId",
                        column: x => x.RefTeacherTypeId,
                        principalTable: "RefTeacherType",
                        principalColumn: "RefTeacherTypeId");
                });

            migrationBuilder.CreateTable(
                name: "TLMDistributionDetail",
                columns: table => new
                {
                    TLMDistributionDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TLMDistributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TLMMaterialId = table.Column<int>(type: "int", nullable: false),
                    QuantityShipped = table.Column<int>(type: "int", nullable: false),
                    QuantityReceived = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLMDistributionDetail", x => x.TLMDistributionDetailId);
                    table.ForeignKey(
                        name: "FK_TLMDistributionDetail_TLMDistribution_TLMDistributionId",
                        column: x => x.TLMDistributionId,
                        principalTable: "TLMDistribution",
                        principalColumn: "TLMDistributionId");
                    table.ForeignKey(
                        name: "FK_TLMDistributionDetail_TLMMaterial_TLMMaterialId",
                        column: x => x.TLMMaterialId,
                        principalTable: "TLMMaterial",
                        principalColumn: "TLMMaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolClassroom",
                columns: table => new
                {
                    SchoolClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefGradeLevelId = table.Column<int>(type: "int", nullable: false),
                    Classrooms = table.Column<int>(type: "int", nullable: true),
                    Classes = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClassroom", x => x.SchoolClassroomId);
                    table.ForeignKey(
                        name: "FK_SchoolClassroom_RefGradeLevel_RefGradeLevelId",
                        column: x => x.RefGradeLevelId,
                        principalTable: "RefGradeLevel",
                        principalColumn: "RefGradeLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolClassroom_School_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "School",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolEnrollment",
                columns: table => new
                {
                    SchoolEnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SchoolPeriodId = table.Column<int>(type: "int", nullable: false),
                    RefParticipantTypeId = table.Column<int>(type: "int", nullable: false),
                    RefGradeLevelId = table.Column<int>(type: "int", nullable: false),
                    Male = table.Column<int>(type: "int", nullable: true),
                    Female = table.Column<int>(type: "int", nullable: true),
                    DisabledMale = table.Column<int>(type: "int", nullable: true),
                    DisabledFemale = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolEnrollment", x => x.SchoolEnrollmentId);
                    table.ForeignKey(
                        name: "FK_SchoolEnrollment_RefGradeLevel_RefGradeLevelId",
                        column: x => x.RefGradeLevelId,
                        principalTable: "RefGradeLevel",
                        principalColumn: "RefGradeLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolEnrollment_RefParticipantType_RefParticipantTypeId",
                        column: x => x.RefParticipantTypeId,
                        principalTable: "RefParticipantType",
                        principalColumn: "RefParticipantTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolEnrollment_School_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "School",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_SchoolEnrollment_SchoolPeriod_SchoolPeriodId",
                        column: x => x.SchoolPeriodId,
                        principalTable: "SchoolPeriod",
                        principalColumn: "SchoolPeriodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RefGradeLevelId = table.Column<int>(type: "int", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Closed = table.Column<bool>(type: "bit", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Group_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Group_RefGradeLevel_RefGradeLevelId",
                        column: x => x.RefGradeLevelId,
                        principalTable: "RefGradeLevel",
                        principalColumn: "RefGradeLevelId");
                    table.ForeignKey(
                        name: "FK_Group_Teacher_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Teacher",
                        principalColumn: "ParticipantId");
                });

            migrationBuilder.CreateTable(
                name: "GroupEnrollment",
                columns: table => new
                {
                    GroupEnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Attendance = table.Column<int>(type: "int", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefEnrollmentStatusId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEnrollment", x => x.GroupEnrollmentId);
                    table.ForeignKey(
                        name: "FK_GroupEnrollment_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_GroupEnrollment_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupEnrollment_RefEnrollmentStatus_RefEnrollmentStatusId",
                        column: x => x.RefEnrollmentStatusId,
                        principalTable: "RefEnrollmentStatus",
                        principalColumn: "RefEnrollmentStatusId");
                });

            migrationBuilder.CreateTable(
                name: "GroupEvaluation",
                columns: table => new
                {
                    GroupEvaluationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupEnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgramAssessmentId = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefEvaluationStatusId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEvaluation", x => x.GroupEvaluationId);
                    table.ForeignKey(
                        name: "FK_GroupEvaluation_GroupEnrollment_GroupEnrollmentId",
                        column: x => x.GroupEnrollmentId,
                        principalTable: "GroupEnrollment",
                        principalColumn: "GroupEnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupEvaluation_ProgramAssessment_ProgramAssessmentId",
                        column: x => x.ProgramAssessmentId,
                        principalTable: "ProgramAssessment",
                        principalColumn: "ProgramAssessmentId");
                    table.ForeignKey(
                        name: "FK_GroupEvaluation_RefEvaluationStatus_RefEvaluationStatusId",
                        column: x => x.RefEvaluationStatusId,
                        principalTable: "RefEvaluationStatus",
                        principalColumn: "RefEvaluationStatusId");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4ceca73e-b85c-424d-981a-78f11ace7c9d"), "decdd185-8cb0-41d2-a186-3f11205ae135", "Create, Edit, and Delete Role", "Delete", null },
                    { new Guid("7dd268bd-e3c2-48f7-b143-7785509e2bd7"), "be50ac5e-d70c-430f-aac7-c732b0afe29a", "Read only Role", "Read", null },
                    { new Guid("9318cd39-3636-4752-a7b4-339692a10ea6"), "f41e83a4-32c7-4af0-a41a-e5fe59269709", "Monitoring, Evaluation and Learning Officer Role", "MELOfficer", null },
                    { new Guid("93655472-90eb-44f5-9408-d6d62b008c59"), "b15576c7-b355-434a-97b0-931879902aa2", "Monitoring, Evaluation and Learning Role", "MEL", null },
                    { new Guid("a6ffbcf8-8aa5-4427-bd56-d6e226e1fbf7"), "2bb78266-f596-476f-9702-732d0c06a739", "Administrator Role", "Administrator", null },
                    { new Guid("bf2adec9-5b5b-4251-bce1-a8a5cc619f3b"), "96bcaa7a-b87e-4dae-82c9-04de0180632a", "Create and Edit Role", "Edit", null },
                    { new Guid("c814d6a8-08f8-4f14-bcbb-fa65569bec51"), "82f7e5ce-005c-4ff9-81bf-b6c7df287292", "Create only Role", "Create", null }
                });

            migrationBuilder.InsertData(
                table: "RefAttendanceUnit",
                columns: new[] { "RefAttendanceUnitId", "AttendanceUnit", "AttendanceUnitCode", "AttendanceUnitId" },
                values: new object[,]
                {
                    { 1, "Hours", "Hour", "h" },
                    { 2, "Days", "Day", "d" }
                });

            migrationBuilder.InsertData(
                table: "RefEnrollmentStatus",
                columns: new[] { "RefEnrollmentStatusId", "EnrollmentStatus", "EnrollmentStatusCode" },
                values: new object[,]
                {
                    { 1, "Enrolled", "E" },
                    { 2, "Completed", "C" },
                    { 3, "Dropped out", "D" }
                });

            migrationBuilder.InsertData(
                table: "RefEvaluationStatus",
                columns: new[] { "RefEvaluationStatusId", "EvaluationStatus", "EvaluationStatusCode" },
                values: new object[,]
                {
                    { 1, "Enrolled", "E" },
                    { 2, "Completed", "C" }
                });

            migrationBuilder.InsertData(
                table: "RefOrganizationType",
                columns: new[] { "RefOrganizationTypeId", "OrganizationType", "OrganizationTypeCode" },
                values: new object[,]
                {
                    { 1, "Organization Unit", "OU" },
                    { 2, "School", "School" }
                });

            migrationBuilder.InsertData(
                table: "RefParticipantType",
                columns: new[] { "RefParticipantTypeId", "ParticipantType", "ParticipantTypeCode" },
                values: new object[,]
                {
                    { 1, "Student", "ST" },
                    { 2, "Teacher", "TE" },
                    { 3, "Education Administrator", "EA" }
                });

            migrationBuilder.InsertData(
                table: "RefProgramType",
                columns: new[] { "RefProgramTypeId", "ProgramType", "ProgramTypeCode" },
                values: new object[] { 1, "Default Program Type", "D01" });

            migrationBuilder.InsertData(
                table: "RefSex",
                columns: new[] { "RefSexId", "Sex", "SexId" },
                values: new object[,]
                {
                    { 1, "Male", "M" },
                    { 2, "Female", "F" }
                });

            migrationBuilder.InsertData(
                table: "Organization",
                columns: new[] { "OrganizationId", "Address", "Contact", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "IsOrganizationUnit", "IsTenant", "Latitude", "Longitude", "ModifiedBy", "ModifiedDate", "OrganizationCode", "OrganizationName", "ParentOrganizationId", "Phone", "RefLocationId", "RefOrganizationTypeId", "RegistrationDate" },
                values: new object[] { new Guid("d28cdcc2-211b-47f2-984b-82a01bf6c9e5"), null, null, "admin@meinsight.org", new DateTimeOffset(new DateTime(2022, 6, 10, 0, 41, 14, 309, DateTimeKind.Unspecified).AddTicks(7220), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, true, true, null, null, null, null, "MO01", "Management Organization", null, null, null, 1, new DateTime(2022, 6, 10, 0, 41, 14, 309, DateTimeKind.Utc).AddTicks(7245) });

            migrationBuilder.InsertData(
                table: "Program",
                columns: new[] { "ProgramId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "DisplayMarks", "HasAssessment", "IsDeleted", "Max", "Min", "ModifiedBy", "ModifiedDate", "ProgramName", "RefAttendanceUnitId", "RefOrganizationTypeId", "RefProgramDeliveryTypeId", "RefProgramTypeId" },
                values: new object[] { 1, null, null, null, null, null, false, false, false, null, null, null, null, "Default Program", 1, null, null, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrganizationId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("170dc8a3-945c-42f5-9a00-360e138fdeab"), 0, "f67f78de-4d5d-40e2-9a25-7d8212f974de", "admin@meinsight.org", true, null, null, false, null, null, "ADMIN@MEINSIGHT.ORG", new Guid("d28cdcc2-211b-47f2-984b-82a01bf6c9e5"), "AQAAAAEAACcQAAAAEE1oOiWvhl82d2ZcEcKVd/dHUgZv9La9ImkjdgKMNfcn7LGqK6n1IhwmNwtDZNoHBA==", null, true, "2ae5991f-47ad-432c-90c7-855b03dbb2cc", false, "admin@meinsight.org" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a6ffbcf8-8aa5-4427-bd56-d6e226e1fbf7"), new Guid("170dc8a3-945c-42f5-9a00-360e138fdeab") });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrganizationId",
                table: "AspNetUsers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EducationAdministrator_RefEducationAdministratorOfficeId",
                table: "EducationAdministrator",
                column: "RefEducationAdministratorOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationAdministrator_RefEducationAdministratorPositionId",
                table: "EducationAdministrator",
                column: "RefEducationAdministratorPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationAdministrator_RefEducationAdministratorTypeId",
                table: "EducationAdministrator",
                column: "RefEducationAdministratorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_OrganizationId",
                table: "Group",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_ParticipantId",
                table: "Group",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_ProgramId",
                table: "Group",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_RefGradeLevelId",
                table: "Group",
                column: "RefGradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupEnrollment_GroupId",
                table: "GroupEnrollment",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupEnrollment_ParticipantId",
                table: "GroupEnrollment",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupEnrollment_RefEnrollmentStatusId",
                table: "GroupEnrollment",
                column: "RefEnrollmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupEvaluation_GroupEnrollmentId",
                table: "GroupEvaluation",
                column: "GroupEnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupEvaluation_ProgramAssessmentId",
                table: "GroupEvaluation",
                column: "ProgramAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupEvaluation_RefEvaluationStatusId",
                table: "GroupEvaluation",
                column: "RefEvaluationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_ParentOrganizationId",
                table: "Organization",
                column: "ParentOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_RefLocationId",
                table: "Organization",
                column: "RefLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_RefOrganizationTypeId",
                table: "Organization",
                column: "RefOrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_OrganizationId",
                table: "Participant",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_RefDisabilityTypeId",
                table: "Participant",
                column: "RefDisabilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_RefLocationId",
                table: "Participant",
                column: "RefLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_RefParticipantCohortId",
                table: "Participant",
                column: "RefParticipantCohortId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_RefParticipantTypeId",
                table: "Participant",
                column: "RefParticipantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_RefSexId",
                table: "Participant",
                column: "RefSexId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_RefPartnerSectorId",
                table: "Partner",
                column: "RefPartnerSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_RefPartnerTypeId",
                table: "Partner",
                column: "RefPartnerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_RefAttendanceUnitId",
                table: "Program",
                column: "RefAttendanceUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_RefOrganizationTypeId",
                table: "Program",
                column: "RefOrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_RefProgramDeliveryTypeId",
                table: "Program",
                column: "RefProgramDeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_RefProgramTypeId",
                table: "Program",
                column: "RefProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramAssessment_ProgramId",
                table: "ProgramAssessment",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramAssessment_RefAssessmentTypeId",
                table: "ProgramAssessment",
                column: "RefAssessmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramAssessment_RefAttendanceUnitId",
                table: "ProgramAssessment",
                column: "RefAttendanceUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramAssessment_RefEvaluationStatusId",
                table: "ProgramAssessment",
                column: "RefEvaluationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RefLocation_ParentLocationId",
                table: "RefLocation",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RefLocation_RefLocationTypeId",
                table: "RefLocation",
                column: "RefLocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefSchoolCluster_RefLocationId",
                table: "RefSchoolCluster",
                column: "RefLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_School_RefSchoolAdministrationTypeId",
                table: "School",
                column: "RefSchoolAdministrationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_School_RefSchoolClusterId",
                table: "School",
                column: "RefSchoolClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_School_RefSchoolLanguageId",
                table: "School",
                column: "RefSchoolLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_School_RefSchoolLocationId",
                table: "School",
                column: "RefSchoolLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_School_RefSchoolStatusId",
                table: "School",
                column: "RefSchoolStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_School_RefSchoolTypeId",
                table: "School",
                column: "RefSchoolTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassroom_OrganizationId",
                table: "SchoolClassroom",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassroom_RefGradeLevelId",
                table: "SchoolClassroom",
                column: "RefGradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEnrollment_OrganizationId",
                table: "SchoolEnrollment",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEnrollment_RefGradeLevelId",
                table: "SchoolEnrollment",
                column: "RefGradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEnrollment_RefParticipantTypeId",
                table: "SchoolEnrollment",
                column: "RefParticipantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEnrollment_SchoolPeriodId",
                table: "SchoolEnrollment",
                column: "SchoolPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_RefStudentSpecializationId",
                table: "Student",
                column: "RefStudentSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_RefStudentTypeId",
                table: "Student",
                column: "RefStudentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_RefStudentYearOfStudyId",
                table: "Student",
                column: "RefStudentYearOfStudyId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_RefTeacherEmploymentTypeId",
                table: "Teacher",
                column: "RefTeacherEmploymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_RefTeacherPositionId",
                table: "Teacher",
                column: "RefTeacherPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_RefTeacherTypeId",
                table: "Teacher",
                column: "RefTeacherTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMDistribution_OrganizationIdFrom",
                table: "TLMDistribution",
                column: "OrganizationIdFrom");

            migrationBuilder.CreateIndex(
                name: "IX_TLMDistribution_OrganizationIdTo",
                table: "TLMDistribution",
                column: "OrganizationIdTo");

            migrationBuilder.CreateIndex(
                name: "IX_TLMDistribution_ParentTLMDistributionId",
                table: "TLMDistribution",
                column: "ParentTLMDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMDistribution_RefTLMDistributionStatusId",
                table: "TLMDistribution",
                column: "RefTLMDistributionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMDistribution_TLMDistributionPeriodId",
                table: "TLMDistribution",
                column: "TLMDistributionPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMDistributionDetail_TLMDistributionId",
                table: "TLMDistributionDetail",
                column: "TLMDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMDistributionDetail_TLMMaterialId",
                table: "TLMDistributionDetail",
                column: "TLMMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMMaterial_RefGradeLevelId",
                table: "TLMMaterial",
                column: "RefGradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMMaterial_RefTLMGroupId",
                table: "TLMMaterial",
                column: "RefTLMGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMMaterial_RefTLMLanguageId",
                table: "TLMMaterial",
                column: "RefTLMLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMMaterial_RefTLMMaterialSetId",
                table: "TLMMaterial",
                column: "RefTLMMaterialSetId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMMaterial_RefTLMMaterialTypeId",
                table: "TLMMaterial",
                column: "RefTLMMaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TLMMaterial_RefTLMSubjectId",
                table: "TLMMaterial",
                column: "RefTLMSubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EducationAdministrator");

            migrationBuilder.DropTable(
                name: "GroupEvaluation");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropTable(
                name: "SchoolClassroom");

            migrationBuilder.DropTable(
                name: "SchoolEnrollment");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "TLMDistributionDetail");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RefEducationAdministratorOffice");

            migrationBuilder.DropTable(
                name: "RefEducationAdministratorPosition");

            migrationBuilder.DropTable(
                name: "RefEducationAdministratorType");

            migrationBuilder.DropTable(
                name: "GroupEnrollment");

            migrationBuilder.DropTable(
                name: "ProgramAssessment");

            migrationBuilder.DropTable(
                name: "RefPartnerSector");

            migrationBuilder.DropTable(
                name: "RefPartnerType");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "SchoolPeriod");

            migrationBuilder.DropTable(
                name: "RefStudentSpecialization");

            migrationBuilder.DropTable(
                name: "RefStudentType");

            migrationBuilder.DropTable(
                name: "RefStudentYearOfStudy");

            migrationBuilder.DropTable(
                name: "TLMDistribution");

            migrationBuilder.DropTable(
                name: "TLMMaterial");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "RefEnrollmentStatus");

            migrationBuilder.DropTable(
                name: "RefAssessmentType");

            migrationBuilder.DropTable(
                name: "RefEvaluationStatus");

            migrationBuilder.DropTable(
                name: "RefSchoolAdministrationType");

            migrationBuilder.DropTable(
                name: "RefSchoolCluster");

            migrationBuilder.DropTable(
                name: "RefSchoolLanguage");

            migrationBuilder.DropTable(
                name: "RefSchoolLocation");

            migrationBuilder.DropTable(
                name: "RefSchoolStatus");

            migrationBuilder.DropTable(
                name: "RefSchoolType");

            migrationBuilder.DropTable(
                name: "RefTLMDistributionStatus");

            migrationBuilder.DropTable(
                name: "TLMDistributionPeriod");

            migrationBuilder.DropTable(
                name: "RefTLMGroup");

            migrationBuilder.DropTable(
                name: "RefTLMLanguage");

            migrationBuilder.DropTable(
                name: "RefTLMMaterialSet");

            migrationBuilder.DropTable(
                name: "RefTLMMaterialType");

            migrationBuilder.DropTable(
                name: "RefTLMSubject");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "RefGradeLevel");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "RefAttendanceUnit");

            migrationBuilder.DropTable(
                name: "RefProgramDeliveryType");

            migrationBuilder.DropTable(
                name: "RefProgramType");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "RefTeacherEmploymentType");

            migrationBuilder.DropTable(
                name: "RefTeacherPosition");

            migrationBuilder.DropTable(
                name: "RefTeacherType");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "RefDisabilityType");

            migrationBuilder.DropTable(
                name: "RefParticipantCohort");

            migrationBuilder.DropTable(
                name: "RefParticipantType");

            migrationBuilder.DropTable(
                name: "RefSex");

            migrationBuilder.DropTable(
                name: "RefLocation");

            migrationBuilder.DropTable(
                name: "RefOrganizationType");

            migrationBuilder.DropTable(
                name: "RefLocationType");
        }
    }
}
