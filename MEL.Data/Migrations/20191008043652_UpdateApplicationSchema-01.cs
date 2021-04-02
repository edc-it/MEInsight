using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MEL.Data.Migrations
{
    public partial class UpdateApplicationSchema01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolEnrollment_RefGradeLevel_RefSchoolGradeLevelId",
                table: "SchoolEnrollment");

            migrationBuilder.RenameColumn(
                name: "RefSchoolGradeLevelId",
                table: "SchoolEnrollment",
                newName: "RefGradeLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolEnrollment_RefSchoolGradeLevelId",
                table: "SchoolEnrollment",
                newName: "IX_SchoolEnrollment_RefGradeLevelId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "TLMDistribution",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefSchoolLanguageId",
                table: "School",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefSchoolLanguage",
                columns: table => new
                {
                    RefSchoolLanguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolLanguageCode = table.Column<string>(maxLength: 25, nullable: false),
                    SchoolLanguage = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefSchoolLanguage", x => x.RefSchoolLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolClassroom",
                columns: table => new
                {
                    SchoolClassroomId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    RefGradeLevelId = table.Column<int>(nullable: false),
                    Classrooms = table.Column<int>(nullable: true),
                    Classes = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClassroom", x => x.SchoolClassroomId);
                    table.ForeignKey(
                        name: "FK_SchoolClassroom_School_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "School",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolClassroom_RefGradeLevel_RefGradeLevelId",
                        column: x => x.RefGradeLevelId,
                        principalTable: "RefGradeLevel",
                        principalColumn: "RefGradeLevelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_School_RefSchoolLanguageId",
                table: "School",
                column: "RefSchoolLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassroom_OrganizationId",
                table: "SchoolClassroom",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassroom_RefGradeLevelId",
                table: "SchoolClassroom",
                column: "RefGradeLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_School_RefSchoolLanguage_RefSchoolLanguageId",
                table: "School",
                column: "RefSchoolLanguageId",
                principalTable: "RefSchoolLanguage",
                principalColumn: "RefSchoolLanguageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolEnrollment_RefGradeLevel_RefGradeLevelId",
                table: "SchoolEnrollment",
                column: "RefGradeLevelId",
                principalTable: "RefGradeLevel",
                principalColumn: "RefGradeLevelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_RefSchoolLanguage_RefSchoolLanguageId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolEnrollment_RefGradeLevel_RefGradeLevelId",
                table: "SchoolEnrollment");

            migrationBuilder.DropTable(
                name: "RefSchoolLanguage");

            migrationBuilder.DropTable(
                name: "SchoolClassroom");

            migrationBuilder.DropIndex(
                name: "IX_School_RefSchoolLanguageId",
                table: "School");

            migrationBuilder.DropColumn(
                name: "RefSchoolLanguageId",
                table: "School");

            migrationBuilder.RenameColumn(
                name: "RefGradeLevelId",
                table: "SchoolEnrollment",
                newName: "RefSchoolGradeLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolEnrollment_RefGradeLevelId",
                table: "SchoolEnrollment",
                newName: "IX_SchoolEnrollment_RefSchoolGradeLevelId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "TLMDistribution",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolEnrollment_RefGradeLevel_RefSchoolGradeLevelId",
                table: "SchoolEnrollment",
                column: "RefSchoolGradeLevelId",
                principalTable: "RefGradeLevel",
                principalColumn: "RefGradeLevelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
