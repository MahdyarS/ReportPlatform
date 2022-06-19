using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class removeFinishWorkTimeAndNullableStartWorkTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FinishWorkTime",
                table: "Reports");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartWorkTime",
                table: "Reports",
                type: "time(1)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(1)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81fc407e-b9f4-4ada-b150-25fde9608f6a", "22a48286-df23-42e9-8607-8b03d4c92a7b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e35f889a-e861-497a-8740-ce781882d862", "48ce351d-6732-4acb-a92d-ee0404f43fa4", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "81fc407e-b9f4-4ada-b150-25fde9608f6a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e35f889a-e861-497a-8740-ce781882d862");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartWorkTime",
                table: "Reports",
                type: "time(1)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time(1)",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FinishWorkTime",
                table: "Reports",
                type: "time(1)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "62fcb5be-2708-467f-ada9-099c6efa8322", "5e52a878-ea35-4f4b-baea-05a26a19a188", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8ede390a-5907-4fc9-9a95-589f2e917783", "4aa4b8e8-9882-4371-9762-69fa3d3175b6", "User", "USER" });
        }
    }
}
