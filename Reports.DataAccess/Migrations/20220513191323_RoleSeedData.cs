using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class RoleSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e767962-4b4f-4b93-b9a7-14c2a2658d0a", "d36a2bb1-85d1-4b22-b245-d6dea4b2eef2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9022bc19-ac10-48e9-9256-10bd0799e78c", "d5c31e00-4614-4654-bc51-5f6bfcde3178", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0e767962-4b4f-4b93-b9a7-14c2a2658d0a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9022bc19-ac10-48e9-9256-10bd0799e78c");
        }
    }
}
