using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class changingdateTimeToDateForPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "010ea7c8-5f63-4a1b-a372-70f09d799ba9");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "47be57de-e67a-49b0-aef3-ab4e5d38440f");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartPeriod",
                table: "Periods",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishPeriod",
                table: "Periods",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89b1313e-0712-4453-bbae-aaa3aebe1cfe", "7af59e42-3d85-430e-962a-9a0da59829ab", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8f1b35e6-15e1-4a84-a36c-ac8acf597860", "838e3f70-b6da-4e94-ad67-271f354f09f0", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "89b1313e-0712-4453-bbae-aaa3aebe1cfe");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8f1b35e6-15e1-4a84-a36c-ac8acf597860");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartPeriod",
                table: "Periods",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishPeriod",
                table: "Periods",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "010ea7c8-5f63-4a1b-a372-70f09d799ba9", "93b58183-6399-4ff9-9c00-13e6caaa0180", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47be57de-e67a-49b0-aef3-ab4e5d38440f", "cb55f811-11cd-4c62-b94d-ab0366353ec2", "User", "USER" });
        }
    }
}
