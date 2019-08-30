using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MEL.Data.Migrations
{
    public partial class CreateApplicationSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefAssessmentType",
                columns: table => new
                {
                    RefAssessmentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssessmentTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    AssessmentType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAssessmentType", x => x.RefAssessmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefAttendanceUnit",
                columns: table => new
                {
                    RefAttendanceUnitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttendanceUnitCode = table.Column<string>(maxLength: 25, nullable: false),
                    AttendanceUnit = table.Column<string>(maxLength: 150, nullable: false),
                    AttendanceUnitId = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAttendanceUnit", x => x.RefAttendanceUnitId);
                });

            migrationBuilder.CreateTable(
                name: "RefEducationAdministratorOffice",
                columns: table => new
                {
                    RefEducationAdministratorOfficeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EducationAdministratorOfficeCode = table.Column<string>(maxLength: 25, nullable: false),
                    EducationAdministratorOffice = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEducationAdministratorOffice", x => x.RefEducationAdministratorOfficeId);
                });

            migrationBuilder.CreateTable(
                name: "RefEducationAdministratorPosition",
                columns: table => new
                {
                    RefEducationAdministratorPositionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EducationAdministratorPositionCode = table.Column<string>(maxLength: 25, nullable: false),
                    EducationAdministratorPosition = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEducationAdministratorPosition", x => x.RefEducationAdministratorPositionId);
                });

            migrationBuilder.CreateTable(
                name: "RefEducationAdministratorType",
                columns: table => new
                {
                    RefEducationAdministratorTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EducationAdministratorTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    EducationAdministratorType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEducationAdministratorType", x => x.RefEducationAdministratorTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefEnrollmentStatus",
                columns: table => new
                {
                    RefEnrollmentStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnrollmentStatusCode = table.Column<string>(maxLength: 25, nullable: false),
                    EnrollmentStatus = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEnrollmentStatus", x => x.RefEnrollmentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RefEvaluationStatus",
                columns: table => new
                {
                    RefEvaluationStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationStatusCode = table.Column<string>(maxLength: 25, nullable: false),
                    EvaluationStatus = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefEvaluationStatus", x => x.RefEvaluationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RefGradeLevel",
                columns: table => new
                {
                    RefGradeLevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GradeLevelCode = table.Column<string>(maxLength: 25, nullable: false),
                    GradeLevel = table.Column<string>(maxLength: 150, nullable: false),
                    GradeLevelId = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefGradeLevel", x => x.RefGradeLevelId);
                });

            migrationBuilder.CreateTable(
                name: "RefLocationType",
                columns: table => new
                {
                    RefLocationTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LocationTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    LocationType = table.Column<string>(maxLength: 150, nullable: false),
                    LocationLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefLocationType", x => x.RefLocationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefOrganizationType",
                columns: table => new
                {
                    RefOrganizationTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrganizationType = table.Column<string>(maxLength: 150, nullable: false),
                    OrganizationTypeCode = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefOrganizationType", x => x.RefOrganizationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefParticipantType",
                columns: table => new
                {
                    RefParticipantTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParticipantTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    ParticipantType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefParticipantType", x => x.RefParticipantTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefPartnerSector",
                columns: table => new
                {
                    RefPartnerSectorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartnerSectorCode = table.Column<string>(maxLength: 25, nullable: false),
                    PartnerSector = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefPartnerSector", x => x.RefPartnerSectorId);
                });

            migrationBuilder.CreateTable(
                name: "RefPartnerType",
                columns: table => new
                {
                    RefPartnerTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartnerTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    PartnerType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefPartnerType", x => x.RefPartnerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefProgramType",
                columns: table => new
                {
                    RefProgramTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProgramTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    ProgramType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefProgramType", x => x.RefProgramTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolAdministrationType",
                columns: table => new
                {
                    RefSchoolAdministrationTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolAdministrationTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    SchoolAdministrationType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolAdministrationType", x => x.RefSchoolAdministrationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolLocation",
                columns: table => new
                {
                    RefSchoolLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolLocationCode = table.Column<string>(maxLength: 25, nullable: false),
                    SchoolLocation = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolLocation", x => x.RefSchoolLocationId);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolStatus",
                columns: table => new
                {
                    RefSchoolStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolStatusCode = table.Column<string>(maxLength: 25, nullable: false),
                    SchoolStatus = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolStatus", x => x.RefSchoolStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolType",
                columns: table => new
                {
                    RefSchoolTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    SchoolType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolType", x => x.RefSchoolTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefSex",
                columns: table => new
                {
                    RefSexId = table.Column<int>(nullable: false),
                    Sex = table.Column<string>(maxLength: 50, nullable: false),
                    SexId = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSex", x => x.RefSexId);
                });

            migrationBuilder.CreateTable(
                name: "RefStudentDisabilityType",
                columns: table => new
                {
                    RefStudentDisabilityTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisabilityTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    DisabilityType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefStudentDisabilityType", x => x.RefStudentDisabilityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefStudentSpecialization",
                columns: table => new
                {
                    RefStudentSpecializationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentSpecializationCode = table.Column<string>(maxLength: 25, nullable: false),
                    StudentSpecialization = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefStudentSpecialization", x => x.RefStudentSpecializationId);
                });

            migrationBuilder.CreateTable(
                name: "RefStudentType",
                columns: table => new
                {
                    RefStudentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    StudentType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefStudentType", x => x.RefStudentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefTeacherPosition",
                columns: table => new
                {
                    RefTeacherPositionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeacherPositionCode = table.Column<string>(maxLength: 25, nullable: false),
                    TeacherPosition = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTeacherPosition", x => x.RefTeacherPositionId);
                });

            migrationBuilder.CreateTable(
                name: "RefTeacherType",
                columns: table => new
                {
                    RefTeacherTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeacherTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    TeacherType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTeacherType", x => x.RefTeacherTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMDistributionStatus",
                columns: table => new
                {
                    RefTLMDistributionStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DistributionStatusCode = table.Column<string>(maxLength: 25, nullable: false),
                    DistributionStatus = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMDistributionStatus", x => x.RefTLMDistributionStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMGroup",
                columns: table => new
                {
                    RefTLMGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TLMGroupCode = table.Column<string>(maxLength: 25, nullable: false),
                    TLMGroup = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMGroup", x => x.RefTLMGroupId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMLanguage",
                columns: table => new
                {
                    RefTLMLanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TLMLanguageCode = table.Column<string>(maxLength: 25, nullable: false),
                    TLMLanguage = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMLanguage", x => x.RefTLMLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMMaterialSet",
                columns: table => new
                {
                    RefTLMMaterialSetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TLMMaterialSetCode = table.Column<string>(maxLength: 25, nullable: false),
                    TLMMaterialSet = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMMaterialSet", x => x.RefTLMMaterialSetId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMMaterialType",
                columns: table => new
                {
                    RefTLMMaterialTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TLMMaterialTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    TLMMaterialType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMMaterialType", x => x.RefTLMMaterialTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefTLMSubject",
                columns: table => new
                {
                    RefTLMSubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TLMSubjectCode = table.Column<string>(maxLength: 25, nullable: false),
                    TLMSubject = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTLMSubject", x => x.RefTLMSubjectId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolPeriod",
                columns: table => new
                {
                    SchoolPeriodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PeriodName = table.Column<string>(maxLength: 150, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolPeriod", x => x.SchoolPeriodId);
                });

            migrationBuilder.CreateTable(
                name: "TLMDistributionPeriod",
                columns: table => new
                {
                    TLMDistributionPeriodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PeriodName = table.Column<string>(maxLength: 150, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Closed = table.Column<bool>(nullable: true),
                    ClosedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ClosedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLMDistributionPeriod", x => x.TLMDistributionPeriodId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefLocation",
                columns: table => new
                {
                    RefLocationId = table.Column<string>(maxLength: 25, nullable: false),
                    LocationName = table.Column<string>(maxLength: 255, nullable: false),
                    RefLocationTypeId = table.Column<int>(nullable: false),
                    ParentLocationId = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefLocation", x => x.RefLocationId);
                    table.ForeignKey(
                        name: "FK_RefLocation_RefLocation_ParentLocationId",
                        column: x => x.ParentLocationId,
                        principalTable: "RefLocation",
                        principalColumn: "RefLocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefLocation_RefLocationType_RefLocationTypeId",
                        column: x => x.RefLocationTypeId,
                        principalTable: "RefLocationType",
                        principalColumn: "RefLocationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    ProgramId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProgramName = table.Column<string>(maxLength: 150, nullable: false),
                    RefProgramTypeId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Max = table.Column<int>(nullable: true),
                    Min = table.Column<int>(nullable: true),
                    RefAttendanceUnitId = table.Column<int>(nullable: true),
                    HasAssessment = table.Column<bool>(nullable: false),
                    DisplayMarks = table.Column<bool>(nullable: false),
                    RefOrganizationTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.ProgramId);
                    table.ForeignKey(
                        name: "FK_Program_RefAttendanceUnit_RefAttendanceUnitId",
                        column: x => x.RefAttendanceUnitId,
                        principalTable: "RefAttendanceUnit",
                        principalColumn: "RefAttendanceUnitId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Program_RefOrganizationType_RefOrganizationTypeId",
                        column: x => x.RefOrganizationTypeId,
                        principalTable: "RefOrganizationType",
                        principalColumn: "RefOrganizationTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Program_RefProgramType_RefProgramTypeId",
                        column: x => x.RefProgramTypeId,
                        principalTable: "RefProgramType",
                        principalColumn: "RefProgramTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TLMMaterial",
                columns: table => new
                {
                    TLMMaterialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TLMMaterialCode = table.Column<string>(maxLength: 100, nullable: false),
                    TLMMaterialName = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 384, nullable: true),
                    RefTLMMaterialTypeId = table.Column<int>(nullable: true),
                    RefGradeLevelId = table.Column<int>(nullable: true),
                    RefTLMLanguageId = table.Column<int>(nullable: true),
                    RefTLMSubjectId = table.Column<int>(nullable: true),
                    RefTLMGroupId = table.Column<int>(nullable: true),
                    RefTLMMaterialSetId = table.Column<int>(nullable: true),
                    RatioNumerator = table.Column<int>(nullable: true),
                    RatioDenominator = table.Column<int>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Barcode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLMMaterial", x => x.TLMMaterialId);
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefGradeLevel_RefGradeLevelId",
                        column: x => x.RefGradeLevelId,
                        principalTable: "RefGradeLevel",
                        principalColumn: "RefGradeLevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMGroup_RefTLMGroupId",
                        column: x => x.RefTLMGroupId,
                        principalTable: "RefTLMGroup",
                        principalColumn: "RefTLMGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMLanguage_RefTLMLanguageId",
                        column: x => x.RefTLMLanguageId,
                        principalTable: "RefTLMLanguage",
                        principalColumn: "RefTLMLanguageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMMaterialSet_RefTLMMaterialSetId",
                        column: x => x.RefTLMMaterialSetId,
                        principalTable: "RefTLMMaterialSet",
                        principalColumn: "RefTLMMaterialSetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMMaterialType_RefTLMMaterialTypeId",
                        column: x => x.RefTLMMaterialTypeId,
                        principalTable: "RefTLMMaterialType",
                        principalColumn: "RefTLMMaterialTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMMaterial_RefTLMSubject_RefTLMSubjectId",
                        column: x => x.RefTLMSubjectId,
                        principalTable: "RefTLMSubject",
                        principalColumn: "RefTLMSubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    OrganizationCode = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationName = table.Column<string>(maxLength: 255, nullable: false),
                    RefOrganizationTypeId = table.Column<int>(nullable: false),
                    ParentOrganizationId = table.Column<Guid>(nullable: true),
                    Contact = table.Column<string>(maxLength: 150, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    RefLocationId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 384, nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    IsOrganizationUnit = table.Column<bool>(nullable: false),
                    IsTenant = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organization_Organization_ParentOrganizationId",
                        column: x => x.ParentOrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organization_RefLocation_RefLocationId",
                        column: x => x.RefLocationId,
                        principalTable: "RefLocation",
                        principalColumn: "RefLocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organization_RefOrganizationType_RefOrganizationTypeId",
                        column: x => x.RefOrganizationTypeId,
                        principalTable: "RefOrganizationType",
                        principalColumn: "RefOrganizationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefSchoolCluster",
                columns: table => new
                {
                    RefSchoolClusterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolClusterCode = table.Column<string>(maxLength: 25, nullable: true),
                    SchoolCluster = table.Column<string>(maxLength: 150, nullable: false),
                    RefLocationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolCluster", x => x.RefSchoolClusterId);
                    table.ForeignKey(
                        name: "FK_RefSchoolCluster_RefLocation_RefLocationId",
                        column: x => x.RefLocationId,
                        principalTable: "RefLocation",
                        principalColumn: "RefLocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramAssessment",
                columns: table => new
                {
                    ProgramAssessmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
                    AssessmentName = table.Column<string>(maxLength: 150, nullable: false),
                    RefAssessmentTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    TrackAttendance = table.Column<bool>(nullable: false),
                    Max = table.Column<int>(nullable: true),
                    Min = table.Column<int>(nullable: true),
                    RefAttendanceUnitId = table.Column<int>(nullable: true),
                    MaximumScore = table.Column<int>(nullable: true),
                    MinimumScore = table.Column<int>(nullable: true),
                    CompletionScore = table.Column<int>(nullable: true),
                    RefEvaluationStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramAssessment", x => x.ProgramAssessmentId);
                    table.ForeignKey(
                        name: "FK_ProgramAssessment_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramAssessment_RefAssessmentType_RefAssessmentTypeId",
                        column: x => x.RefAssessmentTypeId,
                        principalTable: "RefAssessmentType",
                        principalColumn: "RefAssessmentTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramAssessment_RefAttendanceUnit_RefAttendanceUnitId",
                        column: x => x.RefAttendanceUnitId,
                        principalTable: "RefAttendanceUnit",
                        principalColumn: "RefAttendanceUnitId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramAssessment_RefEvaluationStatus_RefEvaluationStatusId",
                        column: x => x.RefEvaluationStatusId,
                        principalTable: "RefEvaluationStatus",
                        principalColumn: "RefEvaluationStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 70, nullable: true),
                    LastName = table.Column<string>(maxLength: 70, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    RefParticipantTypeId = table.Column<int>(nullable: false),
                    ParticipantCode = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 35, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 35, nullable: true),
                    LastName = table.Column<string>(maxLength: 35, nullable: false),
                    RefSexId = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    Mobile = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 320, nullable: true),
                    Facebook = table.Column<string>(maxLength: 200, nullable: true),
                    InstantMessenger = table.Column<string>(maxLength: 200, nullable: true),
                    RefLocationId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 384, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Participant_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participant_RefLocation_RefLocationId",
                        column: x => x.RefLocationId,
                        principalTable: "RefLocation",
                        principalColumn: "RefLocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participant_RefParticipantType_RefParticipantTypeId",
                        column: x => x.RefParticipantTypeId,
                        principalTable: "RefParticipantType",
                        principalColumn: "RefParticipantTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participant_RefSex_RefSexId",
                        column: x => x.RefSexId,
                        principalTable: "RefSex",
                        principalColumn: "RefSexId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PartnerCode = table.Column<string>(maxLength: 100, nullable: false),
                    RefPartnerTypeId = table.Column<int>(nullable: true),
                    RefPartnerSectorId = table.Column<int>(nullable: true),
                    Contact = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Partner_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partner_RefPartnerSector_RefPartnerSectorId",
                        column: x => x.RefPartnerSectorId,
                        principalTable: "RefPartnerSector",
                        principalColumn: "RefPartnerSectorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partner_RefPartnerType_RefPartnerTypeId",
                        column: x => x.RefPartnerTypeId,
                        principalTable: "RefPartnerType",
                        principalColumn: "RefPartnerTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TLMDistribution",
                columns: table => new
                {
                    TLMDistributionId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    TLMDistributionPeriodId = table.Column<int>(nullable: false),
                    OrganizationIdFrom = table.Column<Guid>(nullable: true),
                    OrganizationIdTo = table.Column<Guid>(nullable: true),
                    ShippedDate = table.Column<DateTime>(nullable: true),
                    ReceivedDate = table.Column<DateTime>(nullable: true),
                    ReceivedBy = table.Column<string>(maxLength: 50, nullable: true),
                    TrackingCode = table.Column<string>(maxLength: 100, nullable: false),
                    RefTLMDistributionStatusId = table.Column<int>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    ParentTLMDistributionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLMDistribution", x => x.TLMDistributionId);
                    table.ForeignKey(
                        name: "FK_TLMDistribution_Organization_OrganizationIdFrom",
                        column: x => x.OrganizationIdFrom,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMDistribution_Organization_OrganizationIdTo",
                        column: x => x.OrganizationIdTo,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMDistribution_TLMDistribution_ParentTLMDistributionId",
                        column: x => x.ParentTLMDistributionId,
                        principalTable: "TLMDistribution",
                        principalColumn: "TLMDistributionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMDistribution_RefTLMDistributionStatus_RefTLMDistributionStatusId",
                        column: x => x.RefTLMDistributionStatusId,
                        principalTable: "RefTLMDistributionStatus",
                        principalColumn: "RefTLMDistributionStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMDistribution_TLMDistributionPeriod_TLMDistributionPeriodId",
                        column: x => x.TLMDistributionPeriodId,
                        principalTable: "TLMDistributionPeriod",
                        principalColumn: "TLMDistributionPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationAdministrator",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RefEducationAdministratorTypeId = table.Column<int>(nullable: true),
                    RefEducationAdministratorPositionId = table.Column<int>(nullable: true),
                    RefEducationAdministratorOfficeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAdministrator", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_EducationAdministrator_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationAdministrator_RefEducationAdministratorOffice_RefEducationAdministratorOfficeId",
                        column: x => x.RefEducationAdministratorOfficeId,
                        principalTable: "RefEducationAdministratorOffice",
                        principalColumn: "RefEducationAdministratorOfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationAdministrator_RefEducationAdministratorPosition_RefEducationAdministratorPositionId",
                        column: x => x.RefEducationAdministratorPositionId,
                        principalTable: "RefEducationAdministratorPosition",
                        principalColumn: "RefEducationAdministratorPositionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationAdministrator_RefEducationAdministratorType_RefEducationAdministratorTypeId",
                        column: x => x.RefEducationAdministratorTypeId,
                        principalTable: "RefEducationAdministratorType",
                        principalColumn: "RefEducationAdministratorTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StudentCode = table.Column<string>(maxLength: 100, nullable: true),
                    RefStudentTypeId = table.Column<int>(nullable: true),
                    RefStudentSpecializationId = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    ParentGuardian = table.Column<string>(maxLength: 255, nullable: true),
                    Disability = table.Column<bool>(nullable: true),
                    RefStudentDisabilityTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Student_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_RefStudentDisabilityType_RefStudentDisabilityTypeId",
                        column: x => x.RefStudentDisabilityTypeId,
                        principalTable: "RefStudentDisabilityType",
                        principalColumn: "RefStudentDisabilityTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_RefStudentSpecialization_RefStudentSpecializationId",
                        column: x => x.RefStudentSpecializationId,
                        principalTable: "RefStudentSpecialization",
                        principalColumn: "RefStudentSpecializationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_RefStudentType_RefStudentTypeId",
                        column: x => x.RefStudentTypeId,
                        principalTable: "RefStudentType",
                        principalColumn: "RefStudentTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RefTeacherTypeId = table.Column<int>(nullable: true),
                    RefTeacherPositionId = table.Column<int>(nullable: true),
                    GradeLevels = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Teacher_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teacher_RefTeacherPosition_RefTeacherPositionId",
                        column: x => x.RefTeacherPositionId,
                        principalTable: "RefTeacherPosition",
                        principalColumn: "RefTeacherPositionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teacher_RefTeacherType_RefTeacherTypeId",
                        column: x => x.RefTeacherTypeId,
                        principalTable: "RefTeacherType",
                        principalColumn: "RefTeacherTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SchoolCode = table.Column<string>(maxLength: 100, nullable: true),
                    RefSchoolTypeId = table.Column<int>(nullable: true),
                    RefSchoolLocationId = table.Column<int>(nullable: true),
                    RefSchoolAdministrationTypeId = table.Column<int>(nullable: true),
                    PartnerId = table.Column<Guid>(nullable: true),
                    IsClusterCenter = table.Column<bool>(nullable: true),
                    RefSchoolClusterId = table.Column<int>(nullable: true),
                    RefSchoolStatusId = table.Column<int>(nullable: true),
                    HeadTeacher = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_School_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_RefSchoolAdministrationType_RefSchoolAdministrationTypeId",
                        column: x => x.RefSchoolAdministrationTypeId,
                        principalTable: "RefSchoolAdministrationType",
                        principalColumn: "RefSchoolAdministrationTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_RefSchoolCluster_RefSchoolClusterId",
                        column: x => x.RefSchoolClusterId,
                        principalTable: "RefSchoolCluster",
                        principalColumn: "RefSchoolClusterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_RefSchoolLocation_RefSchoolLocationId",
                        column: x => x.RefSchoolLocationId,
                        principalTable: "RefSchoolLocation",
                        principalColumn: "RefSchoolLocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_RefSchoolStatus_RefSchoolStatusId",
                        column: x => x.RefSchoolStatusId,
                        principalTable: "RefSchoolStatus",
                        principalColumn: "RefSchoolStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_RefSchoolType_RefSchoolTypeId",
                        column: x => x.RefSchoolTypeId,
                        principalTable: "RefSchoolType",
                        principalColumn: "RefSchoolTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TLMDistributionDetail",
                columns: table => new
                {
                    TLMDistributionDetailId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    TLMDistributionId = table.Column<Guid>(nullable: true),
                    TLMMaterialId = table.Column<int>(nullable: false),
                    QuantityShipped = table.Column<int>(nullable: false),
                    QuantityReceived = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLMDistributionDetail", x => x.TLMDistributionDetailId);
                    table.ForeignKey(
                        name: "FK_TLMDistributionDetail_TLMDistribution_TLMDistributionId",
                        column: x => x.TLMDistributionId,
                        principalTable: "TLMDistribution",
                        principalColumn: "TLMDistributionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLMDistributionDetail_TLMMaterial_TLMMaterialId",
                        column: x => x.TLMMaterialId,
                        principalTable: "TLMMaterial",
                        principalColumn: "TLMMaterialId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false),
                    GroupCode = table.Column<string>(maxLength: 100, nullable: false),
                    GroupName = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ParticipantId = table.Column<Guid>(nullable: true),
                    RefGradeLevelId = table.Column<int>(nullable: true),
                    CompletionDate = table.Column<DateTime>(nullable: true),
                    Closed = table.Column<bool>(nullable: true),
                    ClosedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Group_Teacher_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Teacher",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Group_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Group_RefGradeLevel_RefGradeLevelId",
                        column: x => x.RefGradeLevelId,
                        principalTable: "RefGradeLevel",
                        principalColumn: "RefGradeLevelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolEnrollment",
                columns: table => new
                {
                    SchoolEnrollmentId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: true),
                    SchoolPeriodId = table.Column<int>(nullable: false),
                    RefParticipantTypeId = table.Column<int>(nullable: false),
                    RefSchoolGradeLevelId = table.Column<int>(nullable: false),
                    Male = table.Column<int>(nullable: true),
                    Female = table.Column<int>(nullable: true),
                    DisabledMale = table.Column<int>(nullable: true),
                    DisabledFemale = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolEnrollment", x => x.SchoolEnrollmentId);
                    table.ForeignKey(
                        name: "FK_SchoolEnrollment_School_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "School",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolEnrollment_RefParticipantType_RefParticipantTypeId",
                        column: x => x.RefParticipantTypeId,
                        principalTable: "RefParticipantType",
                        principalColumn: "RefParticipantTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolEnrollment_RefGradeLevel_RefSchoolGradeLevelId",
                        column: x => x.RefSchoolGradeLevelId,
                        principalTable: "RefGradeLevel",
                        principalColumn: "RefGradeLevelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolEnrollment_SchoolPeriod_SchoolPeriodId",
                        column: x => x.SchoolPeriodId,
                        principalTable: "SchoolPeriod",
                        principalColumn: "SchoolPeriodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupEnrollment",
                columns: table => new
                {
                    GroupEnrollmentId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: true),
                    ParticipantId = table.Column<Guid>(nullable: false),
                    EnrollmentDate = table.Column<DateTime>(nullable: true),
                    Attendance = table.Column<int>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    RefEnrollmentStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEnrollment", x => x.GroupEnrollmentId);
                    table.ForeignKey(
                        name: "FK_GroupEnrollment_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupEnrollment_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participant",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupEnrollment_RefEnrollmentStatus_RefEnrollmentStatusId",
                        column: x => x.RefEnrollmentStatusId,
                        principalTable: "RefEnrollmentStatus",
                        principalColumn: "RefEnrollmentStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupEvaluation",
                columns: table => new
                {
                    GroupEvaluationId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GroupEnrollmentId = table.Column<Guid>(nullable: false),
                    EvaluationDate = table.Column<DateTime>(nullable: true),
                    ProgramAssessmentId = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    RefEvaluationStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEvaluation", x => x.GroupEvaluationId);
                    table.ForeignKey(
                        name: "FK_GroupEvaluation_GroupEnrollment_GroupEnrollmentId",
                        column: x => x.GroupEnrollmentId,
                        principalTable: "GroupEnrollment",
                        principalColumn: "GroupEnrollmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupEvaluation_ProgramAssessment_ProgramAssessmentId",
                        column: x => x.ProgramAssessmentId,
                        principalTable: "ProgramAssessment",
                        principalColumn: "ProgramAssessmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupEvaluation_RefEvaluationStatus_RefEvaluationStatusId",
                        column: x => x.RefEvaluationStatusId,
                        principalTable: "RefEvaluationStatus",
                        principalColumn: "RefEvaluationStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrganizationId",
                table: "AspNetUsers",
                column: "OrganizationId");

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
                name: "IX_Participant_RefLocationId",
                table: "Participant",
                column: "RefLocationId");

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
                name: "IX_School_PartnerId",
                table: "School",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_School_RefSchoolAdministrationTypeId",
                table: "School",
                column: "RefSchoolAdministrationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_School_RefSchoolClusterId",
                table: "School",
                column: "RefSchoolClusterId");

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
                name: "IX_SchoolEnrollment_OrganizationId",
                table: "SchoolEnrollment",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEnrollment_RefParticipantTypeId",
                table: "SchoolEnrollment",
                column: "RefParticipantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEnrollment_RefSchoolGradeLevelId",
                table: "SchoolEnrollment",
                column: "RefSchoolGradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEnrollment_SchoolPeriodId",
                table: "SchoolEnrollment",
                column: "SchoolPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_RefStudentDisabilityTypeId",
                table: "Student",
                column: "RefStudentDisabilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_RefStudentSpecializationId",
                table: "Student",
                column: "RefStudentSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_RefStudentTypeId",
                table: "Student",
                column: "RefStudentTypeId");

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
                name: "School");

            migrationBuilder.DropTable(
                name: "SchoolPeriod");

            migrationBuilder.DropTable(
                name: "RefStudentDisabilityType");

            migrationBuilder.DropTable(
                name: "RefStudentSpecialization");

            migrationBuilder.DropTable(
                name: "RefStudentType");

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
                name: "Partner");

            migrationBuilder.DropTable(
                name: "RefSchoolAdministrationType");

            migrationBuilder.DropTable(
                name: "RefSchoolCluster");

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
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "RefGradeLevel");

            migrationBuilder.DropTable(
                name: "RefPartnerSector");

            migrationBuilder.DropTable(
                name: "RefPartnerType");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "RefTeacherPosition");

            migrationBuilder.DropTable(
                name: "RefTeacherType");

            migrationBuilder.DropTable(
                name: "RefAttendanceUnit");

            migrationBuilder.DropTable(
                name: "RefProgramType");

            migrationBuilder.DropTable(
                name: "Organization");

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
