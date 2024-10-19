using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class AddPatients3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "allergiesAndConditions", "appointmentHistory", "dateOfBirth", "email", "emergencyContact", "firstName", "fullName", "gender", "lastName", "medicalRecordNumber", "phoneNumber" },
                values: new object[,]
                {
                    { "1", "[]", "{}", "01/01/1999 00:00:00", "john@email.com", "{\"name\":\"Jane\",\"phoneNumber\":919919919}", "John", "John Doe", "Male", "Doe", 1, "919919919" },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
