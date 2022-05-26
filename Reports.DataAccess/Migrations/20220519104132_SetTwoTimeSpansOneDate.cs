using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class SetTwoTimeSpansOneDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0b5fa99f-f422-4222-83b8-0da18cbc620e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5436f34a-3973-4181-b6a8-bfde9e835fbb");

            migrationBuilder.DropColumn(
                name: "InsertionDate",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "StartWorkDateAndTime",
                table: "Reports",
                newName: "InsertionDateAndTime");

            migrationBuilder.RenameColumn(
                name: "FinishTime",
                table: "Reports",
                newName: "StartWorkTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reports",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FinishWorkTime",
                table: "Reports",
                type: "time(1)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb4383dd-8526-424d-a7cb-20cd234e2be4", "ea746270-8031-41f8-ace3-44dc119be3d2", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f7193ba6-fc6f-4335-965f-e37a3750fe0b", "c1a45347-1605-449b-a49e-d5d8d9f4c742", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "eb4383dd-8526-424d-a7cb-20cd234e2be4");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f7193ba6-fc6f-4335-965f-e37a3750fe0b");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "FinishWorkTime",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "StartWorkTime",
                table: "Reports",
                newName: "FinishTime");

            migrationBuilder.RenameColumn(
                name: "InsertionDateAndTime",
                table: "Reports",
                newName: "StartWorkDateAndTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertionDate",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b5fa99f-f422-4222-83b8-0da18cbc620e", "2c23efda-58be-49cf-9419-0e03bd6dfcd2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5436f34a-3973-4181-b6a8-bfde9e835fbb", "517db3ea-787c-4ba2-bf69-71cc9398ff86", "Admin", "ADMIN" });
        }
    }
}
