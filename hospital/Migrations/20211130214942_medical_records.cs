using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hospital.Migrations
{
    public partial class medical_records : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pregnancies = table.Column<int>(type: "int", nullable: true),
                    Glucose = table.Column<int>(type: "int", nullable: true),
                    BloodPresure = table.Column<int>(type: "int", nullable: true),
                    SkinThickness = table.Column<int>(type: "int", nullable: true),
                    Insulin = table.Column<int>(type: "int", nullable: true),
                    Bmi = table.Column<float>(type: "real", nullable: true),
                    DiabetesPedigreeFunction = table.Column<float>(type: "real", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Outcome = table.Column<bool>(type: "bit", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Filled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalRecords");
        }
    }
}
