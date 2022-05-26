using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reports.DataAccess.Migrations
{
    public partial class addingIsRemoteToReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "254b8580-c544-475e-bddf-031cbfa8a35a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "311f2819-4943-4fc0-9921-6de92e2eed16");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemote",
                table: "Reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0283e3e8-ee30-47bb-a4a1-8390a6ee7316", "25c7afb4-dbb4-447b-8452-ce0bc1ab89f3", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "074d134d-1629-4d31-ad79-6fcdb9e2ec00", "a49ab53e-a78d-4bf4-9551-d2f82e4259ab", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0283e3e8-ee30-47bb-a4a1-8390a6ee7316");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "074d134d-1629-4d31-ad79-6fcdb9e2ec00");

            migrationBuilder.DropColumn(
                name: "IsRemote",
                table: "Reports");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "254b8580-c544-475e-bddf-031cbfa8a35a", "e61d8b88-b7bd-4fd8-90c6-4ad8141f0527", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "311f2819-4943-4fc0-9921-6de92e2eed16", "b5a71829-11c5-478a-978f-527869214a22", "User", "USER" });
        }
    }
}
