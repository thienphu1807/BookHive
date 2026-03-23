using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookHiveApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBookReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBookReviews",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookReviews", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_UserBookReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookReviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85cf603a-6761-4603-99fd-f661f0000001",
                column: "ConcurrencyStamp",
                value: "19b423dc-55e2-4338-a9cf-d9bf41d808c8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85cf603a-6761-4603-99fd-f661f0000002",
                column: "ConcurrencyStamp",
                value: "1097b358-7711-4633-b867-51c86bb2cb06");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85cf603a-6761-4603-99fd-f661f0000003",
                column: "ConcurrencyStamp",
                value: "a811ab3c-21e4-493a-ab5d-cc452564f9dc");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookReviews_BookId",
                table: "UserBookReviews",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBookReviews");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85cf603a-6761-4603-99fd-f661f0000001",
                column: "ConcurrencyStamp",
                value: "0d088989-d19f-4d12-8c79-81c82b02b71c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85cf603a-6761-4603-99fd-f661f0000002",
                column: "ConcurrencyStamp",
                value: "aa4217ba-9a87-448e-88d9-8549020f6c2b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85cf603a-6761-4603-99fd-f661f0000003",
                column: "ConcurrencyStamp",
                value: "4aa9ffdf-cef7-49ea-93fa-6453fe307b3a");
        }
    }
}
