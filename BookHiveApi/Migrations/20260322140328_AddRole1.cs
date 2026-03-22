using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookHiveApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRole1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85cf603a-6761-4603-99fd-f661f0000001", "0d088989-d19f-4d12-8c79-81c82b02b71c", "Admin", "ADMIN" },
                    { "85cf603a-6761-4603-99fd-f661f0000002", "aa4217ba-9a87-448e-88d9-8549020f6c2b", "Reader", "READER" },
                    { "85cf603a-6761-4603-99fd-f661f0000003", "4aa9ffdf-cef7-49ea-93fa-6453fe307b3a", "Reviewer", "REVIEWER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85cf603a-6761-4603-99fd-f661f0000001");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85cf603a-6761-4603-99fd-f661f0000002");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85cf603a-6761-4603-99fd-f661f0000003");
        }
    }
}
