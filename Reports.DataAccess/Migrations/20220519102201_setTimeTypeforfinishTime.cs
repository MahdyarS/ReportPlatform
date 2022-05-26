using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class setTimeTypeforfinishTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7263057e-037f-40c7-91f4-07f6beccb231");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8d5dba52-6410-4e9c-9cfa-e31650787b6c");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "FinishTime",
                table: "Reports",
                type: "time(1)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b5fa99f-f422-4222-83b8-0da18cbc620e", "2c23efda-58be-49cf-9419-0e03bd6dfcd2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5436f34a-3973-4181-b6a8-bfde9e835fbb", "517db3ea-787c-4ba2-bf69-71cc9398ff86", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0b5fa99f-f422-4222-83b8-0da18cbc620e");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5436f34a-3973-4181-b6a8-bfde9e835fbb");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishTime",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(1)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7263057e-037f-40c7-91f4-07f6beccb231", "63aa75be-3d66-46a9-9f38-990e91a1f736", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d5dba52-6410-4e9c-9cfa-e31650787b6c", "97d3e275-e616-4101-afaa-6ea7c008d951", "Admin", "ADMIN" });
        }
    }
}
