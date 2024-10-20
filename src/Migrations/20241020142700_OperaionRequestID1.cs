using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class OperaionRequestID1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "2",
                column: "patientID",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "2",
                column: "patientID",
                value: 0);
        }
    }
}
