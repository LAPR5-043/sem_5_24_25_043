using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "requestID",
                table: "PendingRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "operationTypeID",
                table: "OperationRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "doctorID",
                table: "OperationRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "1",
                columns: new[] { "doctorID", "operationTypeID" },
                values: new object[] { "s202400001", "Knee Surgery" });

            migrationBuilder.UpdateData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "2",
                columns: new[] { "doctorID", "operationTypeID" },
                values: new object[] { "s202400002", "Heart Surgery" });

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_requestID",
                table: "PendingRequests",
                column: "requestID",
                unique: true);
            
                                    migrationBuilder.InsertData(
                table: "OperationRequests",
                columns: new[] { "operationRequestID", "Id", "deadlineDate", "doctorID", "operationTypeID", "patientID", "priority" },
                values: new object[,]
                {
                    { "1", "1", "01/01/2025 00:00:00", "s202400001", "Knee Surgery", 1, 2 },
             
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PendingRequests_requestID",
                table: "PendingRequests");

            migrationBuilder.AlterColumn<string>(
                name: "requestID",
                table: "PendingRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "operationTypeID",
                table: "OperationRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "doctorID",
                table: "OperationRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "1",
                columns: new[] { "doctorID", "operationTypeID" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "OperationRequests",
                keyColumn: "operationRequestID",
                keyValue: "2",
                columns: new[] { "doctorID", "operationTypeID" },
                values: new object[] { 2, 2 });
        }
    }
}
