using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class OperaionRequestID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "337df494-a1dc-4669-9fe5-f04ae12da079");

            migrationBuilder.DeleteData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "9c78b5bc-9649-4416-8e70-7cb775416896");

            migrationBuilder.InsertData(
                table: "OperationRequests",
                columns: new[] { "operationRequestID", "Id", "deadlineDate", "doctorID", "operationTypeID", "patientID", "priority" },
                values: new object[,]
                {
                    { "1", "1", "01/01/2025 00:00:00", 1, 1, 1, 2 },
                    { "2", "2", "01/01/2025 00:00:00", 2, 2, 0, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "2");

            migrationBuilder.InsertData(
                table: "OperationRequests",
                columns: new[] { "operationRequestID", "Id", "deadlineDate", "doctorID", "operationTypeID", "patientID", "priority" },
                values: new object[,]
                {
                    { "337df494-a1dc-4669-9fe5-f04ae12da079", "898816fe-04c9-44f4-861e-1a4d1de6f738", "01/01/2025 00:00:00", 1, 1, 1, 2 },
                    { "9c78b5bc-9649-4416-8e70-7cb775416896", "59a0d98c-af5d-412d-9b84-3ee0c9555408", "01/01/2025 00:00:00", 2, 2, 2, 0 }
                });
        }
    }
}
