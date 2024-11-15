using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class NewOpTypesAndStaff : Migration
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
                    roomID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateAndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.appointmentID);
                });

            migrationBuilder.CreateTable(
                name: "AvailabilitySlots",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StaffID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slots = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilitySlots", x => x.Id);
                });

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
                    patientID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctorID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    operationTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deadlineDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specializations = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    operationTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmergencyContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllergiesAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.MedicalRecordNumber);
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
                    availabilitySlotsID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specializationID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "appointmentID", "Id", "dateAndTime", "requestID", "roomID", "status" },
                values: new object[] { "1", "1", "20241028,720,1200", 1, "1", "Scheduled" });

            migrationBuilder.InsertData(
                table: "AvailabilitySlots",
                columns: new[] { "Id", "Slots", "StaffID" },
                values: new object[,]
                {
                    { "d202400001", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400001" },
                    { "d202400002", "{\"20241028\":{\"StartTime\":500,\"EndTime\":1440}}", "d202400002" },
                    { "d202400003", "{\"20241028\":{\"StartTime\":520,\"EndTime\":1320}}", "d202400003" },
                    { "d202400011", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400011" },
                    { "d202400012", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400012" },
                    { "d202400023", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400023" },
                    { "n202400022", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400022" },
                    { "n202400024", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1300}}", "n202400024" },
                    { "n202400025", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1300}}", "n202400025" },
                    { "n202400026", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400026" },
                    { "n202400027", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1300}}", "n202400027" },
                    { "n202400028", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400028" },
                    { "n202400029", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1400}}", "n202400029" },
                    { "n202400030", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1400}}", "n202400030" },
                    { "n202400031", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400031" },
                    { "s202400001", "{\"20241028\":{\"StartTime\":480,\"EndTime\":1400}}", "s202400001" }
                });

            migrationBuilder.InsertData(
                table: "OperationRequests",
                columns: new[] { "operationRequestID", "Id", "deadlineDate", "doctorID", "operationTypeID", "patientID", "priority", "specializations" },
                values: new object[] { "000001", "000001", "01/01/2025 00:00:00", "D202400001", "so2", "1", "Emergency", "{\"Anesthesist\":[\"D202400021\"]}" });

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "operationTypeName", "Id", "estimatedDuration", "isActive", "operationTypeDescription", "specializations" },
                values: new object[,]
                {
                    { "so2", "so2", "anesthesia:45,operation:60,cleaning:45", true, "Knee Replacement Surgery", "{\"Doctor_Orthopaedist\":3,\"Doctor_Anaesthetist\":1,\"Nurse_Circulating\":1,\"Nurse_Instrumenting\":1,\"Nurse_Anaesthetist\":1,\"Assistant_Medical_Action\":1}" },
                    { "so3", "so3", "anesthesia:45,operation:90,cleaning:45", true, "Shoulder Replacement Surgery", "{\"Doctor_Orthopaedist\":3,\"Doctor_Anaesthetist\":1,\"Nurse_Circulating\":1,\"Nurse_Instrumenting\":1,\"Nurse_Anaesthetist\":1,\"Assistant_Medical_Action\":1}" },
                    { "so4", "so4", "anesthesia:45,operation:75,cleaning:45", true, "Hip Replacement Surgery", "{\"Doctor_Orthopaedist\":2,\"Doctor_Anaesthetist\":1,\"Nurse_Circulating\":1,\"Nurse_Instrumenting\":1,\"Nurse_Anaesthetist\":1,\"Assistant_Medical_Action\":1}" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "MedicalRecordNumber", "AllergiesAndConditions", "AppointmentHistory", "DateOfBirth", "Email", "EmergencyContact", "FirstName", "FullName", "Gender", "Id", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { "1", "[]", "{}", "1/1/1999", "john@email.com", "{\"Name\":\"Jane\",\"PhoneNumber\":\"+351919919919\"}", "John", "John Doe", "Male", "1", "Doe", "+351919919919" },
                    { "2", "[]", "{}", "1/1/1999", "Jane@email.com", "{\"Name\":\"Jane\",\"PhoneNumber\":\"+351919999119\"}", "Jane", "Jane Does", "Male", "2", "Does", "+351919991919" }
                });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "availabilitySlotsID", "email", "firstName", "fullName", "isActive", "lastName", "licenseNumber", "phoneNumber", "specializationID", "staffID" },
                values: new object[,]
                {
                    { "d202400001", "d202400001", "d202400001@medopt.com", "John", "John,Doe", true, "Doe", "821322", "+351912297153", "orthopaedist", "d202400001" },
                    { "d202400002", "d202400002", "d202400002@medopt.com", "Jane", "Jane,Smith", true, "Smith", "805322", "+351931170255", "anaesthetist", "d202400002" },
                    { "d202400003", "d202400003", "d202400003@medopt.com", "Carlos", "Carlos,Moedas", true, "Moedas", "641149", "+351939272958", "orthopaedist", "d202400003" },
                    { "d202400011", "d202400011", "d202400011@medopt.com", "Maria", "Maria,Silva", true, "Silva", "858244", "+351956913062", "orthopaedist", "d202400011" },
                    { "d202400012", "d202400012", "d202400012@medopt.com", "Ana", "Ana,Costa", true, "Costa", "123949", "+351961716712", "orthopaedist", "d202400012" },
                    { "d202400023", "d202400023", "d202400023@medopt.com", "Luis", "Luis,Martins", true, "Martins", "269233", "+351993373418", "anaesthetist", "d202400023" },
                    { "n202400022", "n202400022", "n202400022@medopt.com", "David", "David,Fernandes", true, "Fernandes", "588010", "+351910848568", "anaesthetist", "n202400022" },
                    { "n202400024", "n202400024", "n202400024@medopt.com", "Pedro", "Pedro,Gomes", true, "Gomes", "793605", "+351923936967", "anaesthetist", "n202400024" },
                    { "n202400025", "n202400025", "n202400025@medopt.com", "Laura", "Laura,Sousa", true, "Sousa", "421206", "+351987269308", "circulating", "n202400025" },
                    { "n202400026", "n202400026", "n202400026@medopt.com", "Carlos", "Carlos,Moedas", true, "Moedas", "512602", "+351931017743", "instrumenting", "n202400026" },
                    { "n202400027", "n202400027", "n202400027@medopt.com", "Maria", "Maria,Silva", true, "Silva", "483791", "+351981736171", "instrumenting", "n202400027" },
                    { "n202400028", "n202400028", "n202400028@medopt.com", "Ana", "Ana,Costa", true, "Costa", "992894", "+351960692470", "instrumenting", "n202400028" },
                    { "n202400029", "n202400029", "n202400029@medopt.com", "Sara", "Sara,Ribeiro", true, "Ribeiro", "909177", "+351923282184", "anaesthetist", "n202400029" },
                    { "n202400030", "n202400030", "n202400030@medopt.com", "John", "John,Doe", true, "Doe", "390786", "+351917891154", "circulating", "n202400030" },
                    { "n202400031", "n202400031", "n202400031@medopt.com", "Jane", "Jane,Smith", true, "Smith", "630957", "+351993471022", "circulating", "n202400031" },
                    { "s202400001", "s202400001", "s202400001@medopt.com", "Luis", "Luis,Martins", true, "Martins", "932751", "+351988977448", "medical_action", "s202400001" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalRecordNumber",
                table: "Patients",
                column: "MedicalRecordNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PhoneNumber",
                table: "Patients",
                column: "PhoneNumber",
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
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AvailabilitySlots");

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
