using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZwalker.Migrations.NZWalksAuthDb
{
    /// <inheritdoc />
    public partial class addRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48f60239-8837-4a01-a8ab-77eb3dca4433", "48f60239-8837-4a01-a8ab-77eb3dca4433", "Reader", "READER" },
                    { "49613479-2881-45eb-b458-a8e384c16ed9", "49613479-2881-45eb-b458-a8e384c16ed9", "Writer", "WRITER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48f60239-8837-4a01-a8ab-77eb3dca4433");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49613479-2881-45eb-b458-a8e384c16ed9");
        }
    }
}
