using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class addingTotalWorkedMinutes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "10b03f73-186f-4a85-9f59-8942d75f57ff");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "97bda5a2-fbaf-4e0b-9af3-0709cd1edd5b");

            migrationBuilder.AddColumn<short>(
                name: "TotalWorkedMinutes",
                table: "Reports",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "62fcb5be-2708-467f-ada9-099c6efa8322", "5e52a878-ea35-4f4b-baea-05a26a19a188", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8ede390a-5907-4fc9-9a95-589f2e917783", "4aa4b8e8-9882-4371-9762-69fa3d3175b6", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "62fcb5be-2708-467f-ada9-099c6efa8322");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8ede390a-5907-4fc9-9a95-589f2e917783");

            migrationBuilder.DropColumn(
                name: "TotalWorkedMinutes",
                table: "Reports");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10b03f73-186f-4a85-9f59-8942d75f57ff", "433d59ac-4f3b-40ef-b499-1776969669f4", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97bda5a2-fbaf-4e0b-9af3-0709cd1edd5b", "e98d05b4-8598-42a3-95bc-fd7092e80fe1", "User", "USER" });
        }
    }
}
