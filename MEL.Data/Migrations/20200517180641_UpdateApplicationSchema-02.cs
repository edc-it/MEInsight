using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MEL.Data.Migrations
{
    public partial class UpdateApplicationSchema02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_RefStudentDisabilityType_RefStudentDisabilityTypeId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "RefStudentDisabilityType");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Disability",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "RefStudentDisabilityTypeId",
                table: "Student",
                newName: "RefStudentYearOfStudyId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_RefStudentDisabilityTypeId",
                table: "Student",
                newName: "IX_Student_RefStudentYearOfStudyId");

            migrationBuilder.AddColumn<int>(
                name: "RefTeacherEmploymentTypeId",
                table: "Teacher",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefProgramDeliveryTypeId",
                table: "Program",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Participant",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Participant",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Disability",
                table: "Participant",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefDisabilityTypeId",
                table: "Participant",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefParticipantCohortId",
                table: "Participant",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefDisabilityType",
                columns: table => new
                {
                    RefDisabilityTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisabilityTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    DisabilityType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefDisabilityType", x => x.RefDisabilityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefParticipantCohort",
                columns: table => new
                {
                    RefParticipantCohortId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParticipantCohortCode = table.Column<string>(maxLength: 25, nullable: false),
                    ParticipantCohort = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefParticipantCohort", x => x.RefParticipantCohortId);
                });

            migrationBuilder.CreateTable(
                name: "RefProgramDeliveryType",
                columns: table => new
                {
                    RefProgramDeliveryTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProgramDeliveryTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    ProgramDeliveryType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefProgramDeliveryType", x => x.RefProgramDeliveryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefStudentYearOfStudy",
                columns: table => new
                {
                    RefStudentYearOfStudyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentYearOfStudyCode = table.Column<string>(maxLength: 25, nullable: false),
                    StudentYearOfStudy = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefStudentYearOfStudy", x => x.RefStudentYearOfStudyId);
                });

            migrationBuilder.CreateTable(
                name: "RefTeacherEmploymentType",
                columns: table => new
                {
                    RefTeacherEmploymentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeacherEmploymentTypeCode = table.Column<string>(maxLength: 25, nullable: false),
                    TeacherEmploymentType = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTeacherEmploymentType", x => x.RefTeacherEmploymentTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_RefTeacherEmploymentTypeId",
                table: "Teacher",
                column: "RefTeacherEmploymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_RefProgramDeliveryTypeId",
                table: "Program",
                column: "RefProgramDeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_RefDisabilityTypeId",
                table: "Participant",
                column: "RefDisabilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Participant_RefParticipantCohortId",
                table: "Participant",
                column: "RefParticipantCohortId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_RefDisabilityType_RefDisabilityTypeId",
                table: "Participant",
                column: "RefDisabilityTypeId",
                principalTable: "RefDisabilityType",
                principalColumn: "RefDisabilityTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_RefParticipantCohort_RefParticipantCohortId",
                table: "Participant",
                column: "RefParticipantCohortId",
                principalTable: "RefParticipantCohort",
                principalColumn: "RefParticipantCohortId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Program_RefProgramDeliveryType_RefProgramDeliveryTypeId",
                table: "Program",
                column: "RefProgramDeliveryTypeId",
                principalTable: "RefProgramDeliveryType",
                principalColumn: "RefProgramDeliveryTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_RefStudentYearOfStudy_RefStudentYearOfStudyId",
                table: "Student",
                column: "RefStudentYearOfStudyId",
                principalTable: "RefStudentYearOfStudy",
                principalColumn: "RefStudentYearOfStudyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_RefTeacherEmploymentType_RefTeacherEmploymentTypeId",
                table: "Teacher",
                column: "RefTeacherEmploymentTypeId",
                principalTable: "RefTeacherEmploymentType",
                principalColumn: "RefTeacherEmploymentTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_RefDisabilityType_RefDisabilityTypeId",
                table: "Participant");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_RefParticipantCohort_RefParticipantCohortId",
                table: "Participant");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_RefProgramDeliveryType_RefProgramDeliveryTypeId",
                table: "Program");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_RefStudentYearOfStudy_RefStudentYearOfStudyId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_RefTeacherEmploymentType_RefTeacherEmploymentTypeId",
                table: "Teacher");

            migrationBuilder.DropTable(
                name: "RefDisabilityType");

            migrationBuilder.DropTable(
                name: "RefParticipantCohort");

            migrationBuilder.DropTable(
                name: "RefProgramDeliveryType");

            migrationBuilder.DropTable(
                name: "RefStudentYearOfStudy");

            migrationBuilder.DropTable(
                name: "RefTeacherEmploymentType");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_RefTeacherEmploymentTypeId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Program_RefProgramDeliveryTypeId",
                table: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Participant_RefDisabilityTypeId",
                table: "Participant");

            migrationBuilder.DropIndex(
                name: "IX_Participant_RefParticipantCohortId",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "RefTeacherEmploymentTypeId",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "RefProgramDeliveryTypeId",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "Disability",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "RefDisabilityTypeId",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "RefParticipantCohortId",
                table: "Participant");

            migrationBuilder.RenameColumn(
                name: "RefStudentYearOfStudyId",
                table: "Student",
                newName: "RefStudentDisabilityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_RefStudentYearOfStudyId",
                table: "Student",
                newName: "IX_Student_RefStudentDisabilityTypeId");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Disability",
                table: "Student",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefStudentDisabilityType",
                columns: table => new
                {
                    RefStudentDisabilityTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisabilityType = table.Column<string>(maxLength: 150, nullable: false),
                    DisabilityTypeCode = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefStudentDisabilityType", x => x.RefStudentDisabilityTypeId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Student_RefStudentDisabilityType_RefStudentDisabilityTypeId",
                table: "Student",
                column: "RefStudentDisabilityTypeId",
                principalTable: "RefStudentDisabilityType",
                principalColumn: "RefStudentDisabilityTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
