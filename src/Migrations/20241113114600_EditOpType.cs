using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class EditOpType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "appointmentID",
                keyValue: "1",
                column: "dateAndTime",
                value: "2024-11-13 11:45:59");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "appointmentID",
                keyValue: "1",
                column: "dateAndTime",
                value: "2024-11-06 16:12:12");
        }
    }
}
