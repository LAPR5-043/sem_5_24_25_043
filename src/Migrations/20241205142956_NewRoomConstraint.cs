using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class NewRoomConstraint : Migration
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
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    specializationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specializationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "SurgeryRooms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgeryRooms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "appointmentID", "Id", "dateAndTime", "requestID", "roomID", "status" },
                values: new object[,]
                {
                    { "000001", "000001", "20241028,720,900", 2, "or1", "Scheduled" },
                    { "000002", "000002", "20241118,960,1020", 3, "or2", "Scheduled" },
                    { "000003", "000003", "20241118,960,1020", 4, "or4", "Scheduled" }
                });

            migrationBuilder.InsertData(
                table: "AvailabilitySlots",
                columns: new[] { "Id", "Slots", "StaffID" },
                values: new object[,]
                {
                    { "d202400001", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1200}}", "d202400001" },
                    { "d202400002", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1440}}", "d202400002" },
                    { "d202400003", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1320}}", "d202400003" },
                    { "d202400011", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1200}}", "d202400011" },
                    { "d202400012", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1200}}", "d202400012" },
                    { "d202400023", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1200}}", "d202400023" },
                    { "n202400022", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1200}}", "n202400022" },
                    { "n202400024", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1300}}", "n202400024" },
                    { "n202400025", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1300}}", "n202400025" },
                    { "n202400026", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1200}}", "n202400026" },
                    { "n202400027", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1300}}", "n202400027" },
                    { "n202400028", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1200}}", "n202400028" },
                    { "n202400029", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1400}}", "n202400029" },
                    { "n202400030", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1400}}", "n202400030" },
                    { "n202400031", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1200}}", "n202400031" },
                    { "s202400001", "{\"20241028\":{\"StartTime\":1,\"EndTime\":1400}}", "s202400001" }
                });

            migrationBuilder.InsertData(
                table: "OperationRequests",
                columns: new[] { "operationRequestID", "Id", "deadlineDate", "doctorID", "operationTypeID", "patientID", "priority", "specializations" },
                values: new object[,]
                {
                    { "1", "1", "1/2/2025", "D202400003", "so3", "1", "Emergency", "{\"orthopaedist\":[\"d202400003\",\"d202400011\",\"d202400012\"],\"anaesthetist\":[\"D202400002\",\"n202400022\"],\"instrumenting\":[\"n202400026\"],\"circulating\":[\"n202400030\"],\"medical_action\":[\"s202400001\"]}" },
                    { "10", "10", "1/5/2025", "d202400011", "so4", "2", "Urgent", "{\"orthopaedist\":[\"d202400011\",\"d202400012\"],\"anaesthetist\":[\"d202400023\",\"n202400024\"],\"instrumenting\":[\"n202400026\"],\"circulating\":[\"n202400025\"],\"medical_action\":[\"s202400001\"]}" },
                    { "2", "2", "1/2/2025", "D202400001", "so2", "1", "Emergency", "{\"orthopaedist\":[\"d202400003\",\"d202400011\",\"d202400012\"],\"anaesthetist\":[\"D202400002\",\"n202400022\"],\"instrumenting\":[\"n202400026\"],\"circulating\":[\"n202400030\"],\"medical_action\":[\"s202400001\"]}" },
                    { "3", "3", "1/1/2025", "d202400001", "so4", "2", "Emergency", "{\"orthopaedist\":[\"d202400001\",\"d202400012\"],\"anaesthetist\":[\"n202400024\",\"d202400002\"],\"instrumenting\":[\"n202400026\"],\"circulating\":[\"n202400025\"],\"medical_action\":[\"s202400001\"]}" },
                    { "4", "4", "2/1/2025", "d202400001", "so2", "2", "Effective", "{\"orthopaedist\":[\"d202400001\",\"d202400011\",\"d202400012\"],\"anaesthetist\":[\"d202400002\",\"n202400024\"],\"instrumenting\":[\"n202400027\"],\"circulating\":[\"n202400031\"],\"medical_action\":[\"s202400001\"]}" },
                    { "5", "5", "1/12/2025", "d202400011", "so4", "3", "Effective", "{\"orthopaedist\":[\"d202400011\",\"d202400012\"],\"anaesthetist\":[\"d202400023\",\"n202400024\"],\"instrumenting\":[\"n202400026\"],\"circulating\":[\"n202400025\"],\"medical_action\":[\"s202400001\"]}" },
                    { "6", "6", "15/1/2025", "d202400003", "so2", "3", "Effective", "{\"orthopaedist\":[\"d202400003\",\"d202400011\",\"d202400012\"],\"anaesthetist\":[\"d202400002\",\"n202400022\"],\"instrumenting\":[\"n202400026\"],\"circulating\":[\"n202400030\"],\"medical_action\":[\"s202400001\"]}" },
                    { "7", "7", "11/12/2025", "d202400003", "so3", "2", "Urgent", "{\"orthopaedist\":[\"d202400003\",\"d202400011\",\"d202400001\"],\"anaesthetist\":[\"d202400002\",\"n202400029\"],\"instrumenting\":[\"n202400026\"],\"circulating\":[\"n202400025\"],\"medical_action\":[\"s202400001\"]}" },
                    { "8", "8", "7/3/2025", "d202400001", "so4", "3", "Urgent", "{\"orthopaedist\":[\"d202400001\",\"d202400003\",\"d202400012\"],\"anaesthetist\":[\"d202400023\",\"n202400024\"],\"instrumenting\":[\"n202400026\"],\"circulating\":[\"n202400025\"],\"medical_action\":[\"s202400001\"]}" },
                    { "9", "9", "4/1/2025", "d202400001", "so2", "1", "Urgent", "{\"orthopaedist\":[\"d202400001\",\"d202400011\",\"d202400012\"],\"anaesthetist\":[\"d202400002\",\"n202400024\"],\"instrumenting\":[\"n202400027\"],\"circulating\":[\"n202400031\"],\"medical_action\":[\"s202400001\"]}" }
                });

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "operationTypeName", "Id", "estimatedDuration", "isActive", "operationTypeDescription", "specializations" },
                values: new object[,]
                {
                    { "so2", "so2", "anesthesia:40,operation:60,cleaning:40", true, "Knee Replacement Surgery", "{\"d;orthopaedist\":3,\"d;anaesthetist\":1,\"n;circulating\":1,\"n;instrumenting\":1,\"n;anaesthetist\":1,\"s;medical_action\":1}" },
                    { "so3", "so3", "anesthesia:40,operation:90,cleaning:40", true, "Shoulder Replacement Surgery", "{\"d;orthopaedist\":3,\"d;anaesthetist\":1,\"n;circulating\":1,\"n;instrumenting\":1,\"n;anaesthetist\":1,\"s;medical_action\":1}" },
                    { "so4", "so4", "anesthesia:40,operation:75,cleaning:40", true, "Hip Replacement Surgery", "{\"d;orthopaedist\":2,\"d;anaesthetist\":1,\"n;circulating\":1,\"n;instrumenting\":1,\"n;anaesthetist\":1,\"s;medical_action\":1}" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "MedicalRecordNumber", "AllergiesAndConditions", "AppointmentHistory", "DateOfBirth", "Email", "EmergencyContact", "FirstName", "FullName", "Gender", "Id", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { "1", "[]", "{}", "1/1/1999", "john@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919919919\"\r\n}", "John", "John Doe", "Male", "1", "Doe", "+351919919919" },
                    { "2", "[]", "{}", "1/1/1999", "Jane@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Jane", "Jane Does", "Male", "2", "Does", "+351919991919" },
                    { "3", "[]", "{}", "1/1/1999", "1220606@isep.ipp.pt", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Ricky", "Ricky Simons", "Male", "3", "Simons", "+351913613541" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "specializationDescription", "specializationName" },
                values: new object[,]
                {
                    { "anaesthetist", "Anaesthetist", "anaesthetist" },
                    { "circulating", "Circulating", "circulating" },
                    { "instrumenting", "Instrumenting", "instrumenting" },
                    { "medical_action", "Medical Action", "medical_action" },
                    { "orthopaedist", "Orthopaedist", "orthopaedist" }
                });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "availabilitySlotsID", "email", "firstName", "fullName", "isActive", "lastName", "licenseNumber", "phoneNumber", "specializationID", "staffID" },
                values: new object[,]
                {
                    { "d202400001", "d202400001", "d202400001@medopt.com", "John", "John,Doe", true, "Doe", "668834", "+351989225252", "orthopaedist", "d202400001" },
                    { "d202400002", "d202400002", "d202400002@medopt.com", "Jane", "Jane,Smith", true, "Smith", "614628", "+351984960256", "anaesthetist", "d202400002" },
                    { "d202400003", "d202400003", "d202400003@medopt.com", "Carlos", "Carlos,Moedas", true, "Moedas", "653567", "+351967100824", "orthopaedist", "d202400003" },
                    { "d202400011", "d202400011", "d202400011@medopt.com", "Maria", "Maria,Silva", true, "Silva", "937325", "+351990966610", "orthopaedist", "d202400011" },
                    { "d202400012", "d202400012", "d202400012@medopt.com", "Ana", "Ana,Costa", true, "Costa", "945412", "+351945450919", "orthopaedist", "d202400012" },
                    { "d202400023", "d202400023", "d202400023@medopt.com", "Luis", "Luis,Martins", true, "Martins", "205996", "+351959517971", "anaesthetist", "d202400023" },
                    { "n202400022", "n202400022", "n202400022@medopt.com", "David", "David,Fernandes", true, "Fernandes", "997295", "+351956947440", "anaesthetist", "n202400022" },
                    { "n202400024", "n202400024", "n202400024@medopt.com", "Pedro", "Pedro,Gomes", true, "Gomes", "255440", "+351924703586", "anaesthetist", "n202400024" },
                    { "n202400025", "n202400025", "n202400025@medopt.com", "Laura", "Laura,Sousa", true, "Sousa", "758681", "+351928210707", "circulating", "n202400025" },
                    { "n202400026", "n202400026", "n202400026@medopt.com", "Carlos", "Carlos,Moedas", true, "Moedas", "857272", "+351971033460", "instrumenting", "n202400026" },
                    { "n202400027", "n202400027", "n202400027@medopt.com", "Maria", "Maria,Silva", true, "Silva", "585558", "+351959796225", "instrumenting", "n202400027" },
                    { "n202400028", "n202400028", "n202400028@medopt.com", "Ana", "Ana,Costa", true, "Costa", "612873", "+351932676114", "instrumenting", "n202400028" },
                    { "n202400029", "n202400029", "n202400029@medopt.com", "Sara", "Sara,Ribeiro", true, "Ribeiro", "352642", "+351949187959", "anaesthetist", "n202400029" },
                    { "n202400030", "n202400030", "n202400030@medopt.com", "John", "John,Doe", true, "Doe", "928773", "+351917040623", "circulating", "n202400030" },
                    { "n202400031", "n202400031", "n202400031@medopt.com", "Jane", "Jane,Smith", true, "Smith", "329668", "+351935014594", "circulating", "n202400031" },
                    { "s202400001", "s202400001", "s202400001@medopt.com", "Luis", "Luis,Martins", true, "Martins", "860657", "+351921992778", "medical_action", "s202400001" }
                });

            migrationBuilder.InsertData(
                table: "SurgeryRooms",
                columns: new[] { "Id", "Name", "RoomID" },
                values: new object[,]
                {
                    { "or1", "Orthopedic Surgery Room 1", "or1" },
                    { "or2", "Orthopedic Surgery Room 2", "or2" },
                    { "or3", "Orthopedic Surgery Room 3", "or3" },
                    { "or4", "Orthopedic Surgery Room 4", "or4" },
                    { "or5", "Orthopedic Surgery Room 5", "or5" },
                    { "or6", "Orthopedic Surgery Room 6", "or6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_requestID",
                table: "Appointments",
                column: "requestID",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_SurgeryRooms_Name",
                table: "SurgeryRooms",
                column: "Name",
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
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "SurgeryRooms");
        }
    }
}
