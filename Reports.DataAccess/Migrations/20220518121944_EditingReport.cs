using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class EditingReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "FinishWorkHour",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "FinishWorkMinute",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "StartWorkHour",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "StartWorkMinute",
                table: "Reports");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishTime",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertionDate",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartWorkDateAndTime",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7263057e-037f-40c7-91f4-07f6beccb231", "63aa75be-3d66-46a9-9f38-990e91a1f736", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d5dba52-6410-4e9c-9cfa-e31650787b6c", "97d3e275-e616-4101-afaa-6ea7c008d951", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7263057e-037f-40c7-91f4-07f6beccb231");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8d5dba52-6410-4e9c-9cfa-e31650787b6c");

            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "InsertionDate",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "StartWorkDateAndTime",
                table: "Reports");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reports",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "FinishWorkHour",
                table: "Reports",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "FinishWorkMinute",
                table: "Reports",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "StartWorkHour",
                table: "Reports",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "StartWorkMinute",
                table: "Reports",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7182d264-7ac1-4578-a7f7-c8cddd44f1f4", "d8b140c8-b383-4721-a3cc-0035fbbb964a", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c0a35402-24b1-402d-a5eb-92502cd52868", "536f6825-a433-4823-ab32-c1672ce43add", "User", "USER" });
        }
    }
}
