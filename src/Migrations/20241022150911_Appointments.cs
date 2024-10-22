using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class Appointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    appointmentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    requestID = table.Column<int>(type: "int", nullable: false),
                    roomID = table.Column<int>(type: "int", nullable: false),
                    dateAndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.appointmentID);
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "appointmentID", "Id", "dateAndTime", "requestID", "roomID", "status" },
                values: new object[] { "1", "1", "2024-10-22 16:09:10", 1, 1, "Scheduled" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
