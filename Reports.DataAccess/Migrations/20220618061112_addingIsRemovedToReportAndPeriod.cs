using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class addingIsRemovedToReportAndPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "448f9c77-11e1-4442-a72d-60a385bb5e44");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7be1fc40-32af-430a-8fbd-204e2509e963");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Periods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10b03f73-186f-4a85-9f59-8942d75f57ff", "433d59ac-4f3b-40ef-b499-1776969669f4", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97bda5a2-fbaf-4e0b-9af3-0709cd1edd5b", "e98d05b4-8598-42a3-95bc-fd7092e80fe1", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "10b03f73-186f-4a85-9f59-8942d75f57ff");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "97bda5a2-fbaf-4e0b-9af3-0709cd1edd5b");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Periods");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "448f9c77-11e1-4442-a72d-60a385bb5e44", "a21bb682-b9db-4e0c-8ed4-38f25dc0f5d5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7be1fc40-32af-430a-8fbd-204e2509e963", "f6a61ebd-399c-419c-be62-45296fe0bd1a", "User", "USER" });
        }
    }
}
