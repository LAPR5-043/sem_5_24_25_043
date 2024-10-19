using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationTypes",
                table: "OperationTypes");

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "operationTypeName",
                keyValue: "Heart Surgery");

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "operationTypeName",
                keyValue: "Knee Surgery");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "medicalRecordNumber",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "medicalRecordNumber",
                keyValue: "2");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "OperationTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "operationTypeName",
                table: "OperationTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationTypes",
                table: "OperationTypes",
                column: "Id");

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "Id", "estimatedDuration", "isActive", "operationTypeName", "specializations" },
                values: new object[,]
                {
                    { "Heart Surgery", "3:15", true, "Heart Surgery", "{\"Cardiology\":1}" },
                    { "Knee Surgery", "2:0", true, "Knee Surgery", "{\"Orthopedics\":2}" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "allergiesAndConditions", "appointmentHistory", "dateOfBirth", "email", "emergencyContact", "firstName", "fullName", "gender", "lastName", "medicalRecordNumber", "phoneNumber" },
                values: new object[,]
                {
                    { "1", "[]", "{}", "1/1/1999", "john@email.com", "{\"name\":\"Jane\",\"phoneNumber\":\"919919919\"}", "John", "John Doe", "Male", "Doe", "1", "919919919" },
                    { "2", "[]", "{}", "1/1/1999", "Jane@email.com", "{\"name\":\"Jane\",\"phoneNumber\":\"919999119\"}", "Jane", "Jane Does", "Male", "Does", "2", "919991919" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationTypes",
                table: "OperationTypes");

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: "Heart Surgery");

            migrationBuilder.DeleteData(
                table: "OperationTypes",
                keyColumn: "Id",
                keyValue: "Knee Surgery");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "operationTypeName",
                table: "OperationTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "OperationTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "medicalRecordNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationTypes",
                table: "OperationTypes",
                column: "operationTypeName");

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "operationTypeName", "Id", "estimatedDuration", "isActive", "specializations" },
                values: new object[,]
                {
                    { "Heart Surgery", "Heart Surgery", "3:15", true, "{\"Cardiology\":1}" },
                    { "Knee Surgery", "Knee Surgery", "2:0", true, "{\"Orthopedics\":2}" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "medicalRecordNumber", "Id", "allergiesAndConditions", "appointmentHistory", "dateOfBirth", "email", "emergencyContact", "firstName", "fullName", "gender", "lastName", "phoneNumber" },
                values: new object[,]
                {
                    { "1", "1", "[]", "{}", "1/1/1999", "john@email.com", "{\"name\":\"Jane\",\"phoneNumber\":\"919919919\"}", "John", "John Doe", "Male", "Doe", "919919919" },
                    { "2", "2", "[]", "{}", "1/1/1999", "Jane@email.com", "{\"name\":\"Jane\",\"phoneNumber\":\"919999119\"}", "Jane", "Jane Does", "Male", "Does", "919991919" }
                });
        }
    }
}
