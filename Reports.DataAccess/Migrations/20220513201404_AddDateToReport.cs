using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class AddDateToReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0e767962-4b4f-4b93-b9a7-14c2a2658d0a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9022bc19-ac10-48e9-9256-10bd0799e78c");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reports",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7182d264-7ac1-4578-a7f7-c8cddd44f1f4", "d8b140c8-b383-4721-a3cc-0035fbbb964a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c0a35402-24b1-402d-a5eb-92502cd52868", "536f6825-a433-4823-ab32-c1672ce43add", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7182d264-7ac1-4578-a7f7-c8cddd44f1f4");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c0a35402-24b1-402d-a5eb-92502cd52868");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reports");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e767962-4b4f-4b93-b9a7-14c2a2658d0a", "d36a2bb1-85d1-4b22-b245-d6dea4b2eef2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9022bc19-ac10-48e9-9256-10bd0799e78c", "d5c31e00-4614-4654-bc51-5f6bfcde3178", "Admin", "ADMIN" });
        }
    }
}
