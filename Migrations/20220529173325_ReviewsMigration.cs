using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookCollection.Migrations
{
    public partial class ReviewsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    BookId = table.Column<int>(nullable: false),
                    BookUserApplicationUserId = table.Column<string>(nullable: true),
                    BookUserBookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_BookUsers_BookUserBookId_BookUserApplicationUserId",
                        columns: x => new { x.BookUserBookId, x.BookUserApplicationUserId },
                        principalTable: "BookUsers",
                        principalColumns: new[] { "BookId", "ApplicationUserId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BookId",
                table: "Posts",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BookUserBookId_BookUserApplicationUserId",
                table: "Posts",
                columns: new[] { "BookUserBookId", "BookUserApplicationUserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
