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
                table: "AvailabilitySlots",
                columns: new[] { "Id", "Slots", "StaffID" },
                values: new object[,]
                {
                    { "d202400001", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400001" },
                    { "d202400002", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400002" },
                    { "d202400003", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400003" },
                    { "d202400004", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400004" },
                    { "d202400005", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400005" },
                    { "d202400006", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400006" },
                    { "d202400007", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400007" },
                    { "d202400008", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400008" },
                    { "d202400009", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400009" },
                    { "d202400010", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "d202400010" },
                    { "n202400011", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400011" },
                    { "n202400012", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400012" },
                    { "n202400013", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400013" },
                    { "n202400014", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400014" },
                    { "n202400015", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400015" },
                    { "n202400016", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400016" },
                    { "n202400017", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400017" },
                    { "n202400018", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400018" },
                    { "n202400019", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "n202400019" },
                    { "s202400001", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "s202400001" },
                    { "s202400002", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "s202400002" },
                    { "s202400003", "{\"20250101\":{\"StartTime\":480,\"EndTime\":1200},\"20250102\":{\"StartTime\":480,\"EndTime\":1200},\"20250103\":{\"StartTime\":480,\"EndTime\":1200},\"20250104\":{\"StartTime\":480,\"EndTime\":1200},\"20250105\":{\"StartTime\":480,\"EndTime\":1200},\"20250106\":{\"StartTime\":480,\"EndTime\":1200},\"20250107\":{\"StartTime\":480,\"EndTime\":1200},\"20250108\":{\"StartTime\":480,\"EndTime\":1200},\"20250109\":{\"StartTime\":480,\"EndTime\":1200},\"20250110\":{\"StartTime\":480,\"EndTime\":1200},\"20250111\":{\"StartTime\":480,\"EndTime\":1200},\"20250112\":{\"StartTime\":480,\"EndTime\":1200},\"20250113\":{\"StartTime\":480,\"EndTime\":1200},\"20250114\":{\"StartTime\":480,\"EndTime\":1200},\"20250115\":{\"StartTime\":480,\"EndTime\":1200},\"20250116\":{\"StartTime\":480,\"EndTime\":1200},\"20250117\":{\"StartTime\":480,\"EndTime\":1200},\"20250118\":{\"StartTime\":480,\"EndTime\":1200},\"20250119\":{\"StartTime\":480,\"EndTime\":1200},\"20250120\":{\"StartTime\":480,\"EndTime\":1200},\"20250121\":{\"StartTime\":480,\"EndTime\":1200},\"20250122\":{\"StartTime\":480,\"EndTime\":1200},\"20250123\":{\"StartTime\":480,\"EndTime\":1200},\"20250124\":{\"StartTime\":480,\"EndTime\":1200},\"20250125\":{\"StartTime\":480,\"EndTime\":1200},\"20250126\":{\"StartTime\":480,\"EndTime\":1200},\"20250127\":{\"StartTime\":480,\"EndTime\":1200},\"20250128\":{\"StartTime\":480,\"EndTime\":1200},\"20250129\":{\"StartTime\":480,\"EndTime\":1200},\"20250130\":{\"StartTime\":480,\"EndTime\":1200},\"20250131\":{\"StartTime\":480,\"EndTime\":1200}}", "s202400003" }
                });

            migrationBuilder.InsertData(
                table: "OperationRequests",
                columns: new[] { "operationRequestID", "Id", "deadlineDate", "doctorID", "operationTypeID", "patientID", "priority", "specializations" },
                values: new object[,]
                {
                    { "1", "1", "1/2/2025", "d202400001", "so2", "202310056123", "Emergency", "{\"orthopaedist\":[\"d202400001\",\"d202400002\",\"d202400003\"],\"anaesthetist\":[\"d202400008\",\"n202400011\"],\"instrumenting\":[\"n202400014\"],\"circulating\":[\"n202400017\"],\"medical_action\":[\"s202400001\"]}" },
                    { "10", "10", "1/2/2025", "d202400004", "so3", "202410000943", "Urgent", "{\"orthopaedist\":[\"d202400004\",\"d202400005\"],\"anaesthetist\":[\"d202400009\",\"n202400012\"],\"instrumenting\":[\"n202400015\"],\"circulating\":[\"n202400018\"],\"medical_action\":[\"s202400002\"]}" },
                    { "11", "11", "1/2/2025", "d202400004", "so3", "202410000944", "Effective", "{\"orthopaedist\":[\"d202400004\",\"d202400005\"],\"anaesthetist\":[\"d202400009\",\"n202400012\"],\"instrumenting\":[\"n202400015\"],\"circulating\":[\"n202400018\"],\"medical_action\":[\"s202400002\"]}" },
                    { "12", "12", "1/2/2025", "d202400004", "so3", "202410000945", "Effective", "{\"orthopaedist\":[\"d202400004\",\"d202400005\"],\"anaesthetist\":[\"d202400009\",\"n202400012\"],\"instrumenting\":[\"n202400015\"],\"circulating\":[\"n202400018\"],\"medical_action\":[\"s202400002\"]}" },
                    { "13", "13", "1/2/2025", "d202400004", "so3", "202410000946", "Emergency", "{\"orthopaedist\":[\"d202400004\",\"d202400005\"],\"anaesthetist\":[\"d202400009\",\"n202400012\"],\"instrumenting\":[\"n202400015\"],\"circulating\":[\"n202400018\"],\"medical_action\":[\"s202400002\"]}" },
                    { "14", "14", "1/2/2025", "d202400006", "so4", "202410000947", "Emergency", "{\"orthopaedist\":[\"d202400006\",\"d202400007\"],\"anaesthetist\":[\"n202400013\",\"d202400010\"],\"instrumenting\":[\"n202400016\"],\"circulating\":[\"n202400019\"],\"medical_action\":[\"s202400003\"]}" },
                    { "15", "15", "1/2/2025", "d202400006", "so4", "202410000950", "Urgent", "{\"orthopaedist\":[\"d202400006\",\"d202400007\"],\"anaesthetist\":[\"n202400013\",\"d202400010\"],\"instrumenting\":[\"n202400016\"],\"circulating\":[\"n202400019\"],\"medical_action\":[\"s202400003\"]}" },
                    { "16", "16", "1/2/2025", "d202400006", "so4", "202410000951", "Urgent", "{\"orthopaedist\":[\"d202400006\",\"d202400007\"],\"anaesthetist\":[\"n202400013\",\"d202400010\"],\"instrumenting\":[\"n202400016\"],\"circulating\":[\"n202400019\"],\"medical_action\":[\"s202400003\"]}" },
                    { "17", "17", "1/2/2025", "d202400006", "so4", "202410000952", "Effective", "{\"orthopaedist\":[\"d202400006\",\"d202400007\"],\"anaesthetist\":[\"n202400013\",\"d202400010\"],\"instrumenting\":[\"n202400016\"],\"circulating\":[\"n202400019\"],\"medical_action\":[\"s202400003\"]}" },
                    { "18", "18", "1/2/2025", "d202400006", "so4", "202310001975", "Effective", "{\"orthopaedist\":[\"d202400006\",\"d202400007\"],\"anaesthetist\":[\"n202400013\",\"d202400010\"],\"instrumenting\":[\"n202400016\"],\"circulating\":[\"n202400019\"],\"medical_action\":[\"s202400003\"]}" },
                    { "19", "19", "1/2/2025", "d202400006", "so4", "202410000948", "Emergency", "{\"orthopaedist\":[\"d202400006\",\"d202400007\"],\"anaesthetist\":[\"n202400013\",\"d202400010\"],\"instrumenting\":[\"n202400016\"],\"circulating\":[\"n202400019\"],\"medical_action\":[\"s202400003\"]}" },
                    { "2", "2", "1/2/2025", "d202400001", "so2", "202410007891", "Emergency", "{\"orthopaedist\":[\"d202400001\",\"d202400002\",\"d202400003\"],\"anaesthetist\":[\"d202400008\",\"n202400011\"],\"instrumenting\":[\"n202400014\"],\"circulating\":[\"n202400017\"],\"medical_action\":[\"s202400001\"]}" },
                    { "20", "20", "1/2/2025", "d202400006", "so4", "202410000949", "Urgent", "{\"orthopaedist\":[\"d202400006\",\"d202400007\"],\"anaesthetist\":[\"n202400013\",\"d202400010\"],\"instrumenting\":[\"n202400016\"],\"circulating\":[\"n202400019\"],\"medical_action\":[\"s202400003\"]}" },
                    { "3", "3", "1/2/2025", "d202400001", "so2", "202410007911", "Urgent", "{\"orthopaedist\":[\"d202400001\",\"d202400002\",\"d202400003\"],\"anaesthetist\":[\"d202400008\",\"n202400011\"],\"instrumenting\":[\"n202400014\"],\"circulating\":[\"n202400017\"],\"medical_action\":[\"s202400001\"]}" },
                    { "4", "4", "1/2/2025", "d202400001", "so2", "202410120782", "Urgent", "{\"orthopaedist\":[\"d202400001\",\"d202400002\",\"d202400003\"],\"anaesthetist\":[\"d202400008\",\"n202400011\"],\"instrumenting\":[\"n202400014\"],\"circulating\":[\"n202400017\"],\"medical_action\":[\"s202400001\"]}" },
                    { "5", "5", "1/2/2025", "d202400001", "so2", "202410033891", "Urgent", "{\"orthopaedist\":[\"d202400001\",\"d202400002\",\"d202400003\"],\"anaesthetist\":[\"d202400008\",\"n202400011\"],\"instrumenting\":[\"n202400014\"],\"circulating\":[\"n202400017\"],\"medical_action\":[\"s202400001\"]}" },
                    { "6", "6", "1/2/2025", "d202400001", "so2", "202410019271", "Effective", "{\"orthopaedist\":[\"d202400001\",\"d202400002\",\"d202400003\"],\"anaesthetist\":[\"d202400008\",\"n202400011\"],\"instrumenting\":[\"n202400014\"],\"circulating\":[\"n202400017\"],\"medical_action\":[\"s202400001\"]}" },
                    { "7", "7", "1/2/2025", "d202400004", "so3", "202410555891", "Emergency", "{\"orthopaedist\":[\"d202400004\",\"d202400005\"],\"anaesthetist\":[\"d202400009\",\"n202400012\"],\"instrumenting\":[\"n202400015\"],\"circulating\":[\"n202400018\"],\"medical_action\":[\"s202400002\"]}" },
                    { "8", "8", "1/2/2025", "d202400004", "so3", "202410000941", "Emergency", "{\"orthopaedist\":[\"d202400004\",\"d202400005\"],\"anaesthetist\":[\"d202400009\",\"n202400012\"],\"instrumenting\":[\"n202400015\"],\"circulating\":[\"n202400018\"],\"medical_action\":[\"s202400002\"]}" },
                    { "9", "9", "1/2/2025", "d202400004", "so3", "202410000942", "Urgent", "{\"orthopaedist\":[\"d202400004\",\"d202400005\"],\"anaesthetist\":[\"d202400009\",\"n202400012\"],\"instrumenting\":[\"n202400015\"],\"circulating\":[\"n202400018\"],\"medical_action\":[\"s202400002\"]}" }
                });

            migrationBuilder.InsertData(
                table: "OperationTypes",
                columns: new[] { "operationTypeName", "Id", "estimatedDuration", "isActive", "operationTypeDescription", "specializations" },
                values: new object[,]
                {
                    { "so2", "so2", "anesthesia:10,operation:30,cleaning:10", true, "Knee Replacement Surgery", "{\"d;orthopaedist\":3,\"d;anaesthetist\":1,\"n;circulating\":1,\"n;instrumenting\":1,\"n;anaesthetist\":1,\"s;medical_action\":1}" },
                    { "so3", "so3", "anesthesia:15,operation:20,cleaning:15", true, "Shoulder Replacement Surgery", "{\"d;orthopaedist\":3,\"d;anaesthetist\":1,\"n;circulating\":1,\"n;instrumenting\":1,\"n;anaesthetist\":1,\"s;medical_action\":1}" },
                    { "so4", "so4", "anesthesia:10,operation:35,cleaning:5", true, "Hip Replacement Surgery", "{\"d;orthopaedist\":2,\"d;anaesthetist\":1,\"n;circulating\":1,\"n;instrumenting\":1,\"n;anaesthetist\":1,\"s;medical_action\":1}" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "MedicalRecordNumber", "AllergiesAndConditions", "AppointmentHistory", "DateOfBirth", "Email", "EmergencyContact", "FirstName", "FullName", "Gender", "Id", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { "202310001975", "[]", "{}", "2/3/2010", "1220606@isep.ipp.pt", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Ricky", "Ricky Simons", "Male", "202310001975", "Simons", "+351913613541" },
                    { "202310056123", "[]", "{}", "17/8/1997", "john@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919919919\"\r\n}", "John", "John Doe", "Male", "202310056123", "Doe", "+351919919919" },
                    { "202410000941", "[]", "{}", "25/10/1971", "rmiguel@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Miguel", "Miguel Rios", "Male", "202410000941", "Rios", "+351919108919" },
                    { "202410000942", "[]", "{}", "1/8/1974", "smiguel@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Miguel", "Miguel Santos", "Male", "202410000942", "Santos", "+351972086871" },
                    { "202410000943", "[]", "{}", "10/9/1996", "rrada@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Carlos", "Carlos Prada", "Male", "202410000943", "Prada", "+351920908079" },
                    { "202410000944", "[]", "{}", "2/7/1994", "furtado@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "José", "José Furtado", "Male", "202410000944", "Furtado", "+351928199291" },
                    { "202410000945", "[]", "{}", "18/10/2012", "ochal@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Diana", "Diana Rocha", "Female", "202410000945", "Rocha", "+351958921901" },
                    { "202410000946", "[]", "{}", "11/7/2011", "nessa@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Jéssica", "Jéssica Vanessa", "Female", "202410000946", "Vanessa", "+351910147943" },
                    { "202410000947", "[]", "{}", "20/5/1981", "dreia@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Andreia", "Andreia Rios", "Female", "202410000947", "Rios", "+351965703922" },
                    { "202410000948", "[]", "{}", "9/2/1979", "biggle@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Mariana", "Mariana Ribeiro", "Female", "202410000948", "Ribeiro", "+351963814392" },
                    { "202410000949", "[]", "{}", "28/2/1970", "key@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Matilde", "Matilde Chaves", "Female", "202410000949", "Chaves", "+351979520831" },
                    { "202410000950", "[]", "{}", "19/2/2006", "isaa@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Isabel", "Isabel Areal", "Female", "202410000950", "Areal", "+351919430214" },
                    { "202410000951", "[]", "{}", "3/6/2012", "angel@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Noélia", "Noélia Anjos", "Female", "202410000951", "Anjos", "+351954878702" },
                    { "202410000952", "[]", "{}", "24/7/2002", "minda@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Ermelinda", "Ermelinda Barbosa", "Female", "202410000952", "Barbosa", "+351931988386" },
                    { "202410007891", "[]", "{}", "19/11/2018", "Jane@email.com", "{\r\n  \"Name\": \"Carlos\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Jane", "Jane Does", "Male", "202410007891", "Does", "+351919991919" },
                    { "202410007911", "[]", "{}", "4/3/1974", "silva@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "João", "João Silva", "Male", "202410007911", "Silva", "+351911382919" },
                    { "202410019271", "[]", "{}", "3/10/2002", "diogonet@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Diogo", "Diogo Neto", "Male", "202410019271", "Neto", "+351919994919" },
                    { "202410033891", "[]", "{}", "28/6/1996", "joanac@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Joana", "Joana Silva", "Male", "202410033891", "Silva", "+351911231919" },
                    { "202410120782", "[]", "{}", "14/6/1989", "costa@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "André", "André Costa", "Male", "202410120782", "Costa", "+351911011919" },
                    { "202410555891", "[]", "{}", "17/3/1970", "franlopes@email.com", "{\r\n  \"Name\": \"Jane\",\r\n  \"PhoneNumber\": \"\\u002B351919999119\"\r\n}", "Francisca", "Francisca Lopes", "Male", "202410555891", "Lopes", "+351919991072" }
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
                    { "d202400001", "d202400001", "d202400001@medopt.com", "John", "John,Doe", true, "Doe", "913747", "+351922236531", "orthopaedist", "d202400001" },
                    { "d202400002", "d202400002", "d202400002@medopt.com", "Jane", "Jane,Smith", true, "Smith", "630413", "+351940724643", "orthopaedist", "d202400002" },
                    { "d202400003", "d202400003", "d202400003@medopt.com", "Carlos", "Carlos,Moedas", true, "Moedas", "540020", "+351967343528", "orthopaedist", "d202400003" },
                    { "d202400004", "d202400004", "d202400004@medopt.com", "Maria", "Maria,Silva", true, "Silva", "919110", "+351927035655", "orthopaedist", "d202400004" },
                    { "d202400005", "d202400005", "d202400005@medopt.com", "Ana", "Ana,Costa", true, "Costa", "463911", "+351953899204", "orthopaedist", "d202400005" },
                    { "d202400006", "d202400006", "d202400006@medopt.com", "Luis", "Luis,Martins", true, "Martins", "167458", "+351928804647", "orthopaedist", "d202400006" },
                    { "d202400007", "d202400007", "d202400007@medopt.com", "Pedro", "Pedro,Gomes", true, "Gomes", "518083", "+351989680327", "orthopaedist", "d202400007" },
                    { "d202400008", "d202400008", "d202400008@medopt.com", "Sara", "Sara,Ribeiro", true, "Ribeiro", "337430", "+351913573399", "anaesthetist", "d202400008" },
                    { "d202400009", "d202400009", "d202400009@medopt.com", "David", "David,Fernandes", true, "Fernandes", "715551", "+351945381157", "anaesthetist", "d202400009" },
                    { "d202400010", "d202400010", "d202400010@medopt.com", "Laura", "Laura,Sousa", true, "Sousa", "781405", "+351966532149", "anaesthetist", "d202400010" },
                    { "n202400011", "n202400011", "n202400011@medopt.com", "John", "John,Doe", true, "Doe", "131092", "+351920022904", "anaesthetist", "n202400011" },
                    { "n202400012", "n202400012", "n202400012@medopt.com", "Jane", "Jane,Smith", true, "Smith", "215315", "+351976305239", "anaesthetist", "n202400012" },
                    { "n202400013", "n202400013", "n202400013@medopt.com", "Carlos", "Carlos,Moedas", true, "Moedas", "612155", "+351989418925", "anaesthetist", "n202400013" },
                    { "n202400014", "n202400014", "n202400014@medopt.com", "Maria", "Maria,Silva", true, "Silva", "402461", "+351977395134", "instrumenting", "n202400014" },
                    { "n202400015", "n202400015", "n202400015@medopt.com", "Nádia", "Nádia,Silva", true, "Silva", "520990", "+351955143561", "instrumenting", "n202400015" },
                    { "n202400016", "n202400016", "n202400016@medopt.com", "José", "José,Costa", true, "Costa", "695514", "+351956736469", "instrumenting", "n202400016" },
                    { "n202400017", "n202400017", "n202400017@medopt.com", "Diogo", "Diogo,Costa", true, "Costa", "337579", "+351948090603", "circulating", "n202400017" },
                    { "n202400018", "n202400018", "n202400018@medopt.com", "Arménio", "Arménio,Costa", true, "Costa", "553791", "+351981477646", "circulating", "n202400018" },
                    { "n202400019", "n202400019", "n202400019@medopt.com", "Nininho", "Nininho,Costa", true, "Costa", "650828", "+351963885322", "circulating", "n202400019" },
                    { "s202400001", "s202400001", "s202400001@medopt.com", "Luis", "Luis,Martins", true, "Martins", "695544", "+351973568790", "medical_action", "s202400001" },
                    { "s202400002", "s202400002", "s202400002@medopt.com", "Cândido", "Cândido,Costa", true, "Costa", "175140", "+351912312947", "medical_action", "s202400002" },
                    { "s202400003", "s202400003", "s202400003@medopt.com", "Reinaldo", "Reinaldo,Teles", true, "Teles", "615113", "+351993057373", "medical_action", "s202400003" }
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
