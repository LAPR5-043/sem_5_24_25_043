using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class ResetDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    logId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timestamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationRequests",
                columns: table => new
                {
                    operationRequestID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    doctorID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    operationTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deadlineDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationRequests", x => x.operationRequestID);
                });

            migrationBuilder.CreateTable(
                name: "OperationTypes",
                columns: table => new
                {
                    operationTypeName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    estimatedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    specializations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTypes", x => x.operationTypeName);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    medicalRecordNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    emergencyContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    allergiesAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    appointmentHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.medicalRecordNumber);
                });

            migrationBuilder.CreateTable(
                name: "PendingRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    requestID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    attributeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pendingValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    oldValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    staffID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    licenseNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    availabilitySlots = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specializationID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OperationRequests",
                columns: new[] { "operationRequestID", "Id", "deadlineDate", "doctorID", "operationTypeID", "patientID", "priority" },
                values: new object[,]
                {
                    { "1", "1", "01/01/2025 00:00:00", "s202400001", "Knee Surgery", 1, "Emergency" },
                    { "2", "2", "01/01/2025 00:00:00", "s202400002", "Heart Surgery", 2, "Effective" }
                });

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

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "availabilitySlots", "email", "firstName", "fullName", "isActive", "lastName", "licenseNumber", "phoneNumber", "specializationID", "staffID" },
                values: new object[,]
                {
                    { "D202400001", "", "D202400001@medopt.com", "John", "John,Doe", true, "Doe", "123456", "919919919", "Cardiology", "D202400001" },
                    { "D202400011", "", "D202400011@medopt.com", "Carlos", "Carlos,Moedas", true, "Moedas", "121236", "919911319", "Orthopedics", "D202400011" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_email",
                table: "Patients",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_medicalRecordNumber",
                table: "Patients",
                column: "medicalRecordNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_phoneNumber",
                table: "Patients",
                column: "phoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequests_requestID",
                table: "PendingRequests",
                column: "requestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_email",
                table: "Staffs",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_licenseNumber",
                table: "Staffs",
                column: "licenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_phoneNumber",
                table: "Staffs",
                column: "phoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "OperationRequests");

            migrationBuilder.DropTable(
                name: "OperationTypes");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "PendingRequests");

            migrationBuilder.DropTable(
                name: "Staffs");
        }
    }
}
