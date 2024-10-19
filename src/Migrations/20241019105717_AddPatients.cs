using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class AddPatients : Migration
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
                    { "2", "[]", "{}", "01/01/1999 00:00:00", "Jane@email.com", "{\"name\":\"Jane\",\"phoneNumber\":919999119}", "Jane", "Jane Does", "Male", "Does", 2, "919991919" }
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
