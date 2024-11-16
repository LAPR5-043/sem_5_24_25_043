using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sem_5_24_25_043.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueToRequestID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "appointmentID",
                keyValue: "1",
                column: "dateAndTime",
                value: "20241028,720,900");

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400001",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "697366", "+351996754549" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400002",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "370397", "+351954937677" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400003",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "916524", "+351980290844" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400011",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "142970", "+351917558901" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400012",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "100342", "+351986696940" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400023",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "391643", "+351980841702" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400022",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "211375", "+351939363347" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400024",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "536257", "+351978120645" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400025",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "843243", "+351924324544" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400026",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "351381", "+351964797207" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400027",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "548199", "+351935751031" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400028",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "915397", "+351966223195" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400029",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "627863", "+351945430139" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400030",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "552685", "+351979216924" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400031",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "135066", "+351999552988" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "s202400001",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "259287", "+351968811249" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_requestID",
                table: "Appointments",
                column: "requestID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_requestID",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "appointmentID",
                keyValue: "1",
                column: "dateAndTime",
                value: "20241028,720,1200");

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400001",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "456958", "+351971722703" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400002",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "417857", "+351965430265" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400003",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "334279", "+351938265415" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400011",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "437757", "+351915996993" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400012",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "233649", "+351916360018" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "d202400023",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "127104", "+351981823461" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400022",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "176713", "+351983226788" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400024",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "297604", "+351960137705" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400025",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "181811", "+351957884247" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400026",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "688645", "+351948841592" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400027",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "296280", "+351925828325" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400028",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "747872", "+351950238550" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400029",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "119897", "+351918319372" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400030",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "738111", "+351970156657" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "n202400031",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "509604", "+351957640516" });

            migrationBuilder.UpdateData(
                table: "Staffs",
                keyColumn: "Id",
                keyValue: "s202400001",
                columns: new[] { "licenseNumber", "phoneNumber" },
                values: new object[] { "157815", "+351957014138" });
        }
    }
}
