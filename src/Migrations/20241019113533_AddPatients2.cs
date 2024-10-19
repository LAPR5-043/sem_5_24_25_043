using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class AddPatients2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "1",
                column: "dateOfBirth",
                value: "1/1/1999");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "2",
                column: "dateOfBirth",
                value: "1/1/1999");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "1",
                column: "dateOfBirth",
                value: "01/01/1999 00:00:00");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "2",
                column: "dateOfBirth",
                value: "01/01/1999 00:00:00");
        }
    }
}
