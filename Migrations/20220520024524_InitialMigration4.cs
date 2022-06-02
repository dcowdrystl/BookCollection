using Microsoft.EntityFrameworkCore.Migrations;

namespace BookCollection.Migrations
{
    public partial class InitialMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BookUsers_BookUserBookId_BookUserApplicationUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BookUserBookId_BookUserApplicationUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BookUserApplicationUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BookUserBookId",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "PostUserApplicationUserId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostUserBookId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostUserBookId_PostUserApplicationUserId",
                table: "Posts",
                columns: new[] { "PostUserBookId", "PostUserApplicationUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BookUsers_PostUserBookId_PostUserApplicationUserId",
                table: "Posts",
                columns: new[] { "PostUserBookId", "PostUserApplicationUserId" },
                principalTable: "BookUsers",
                principalColumns: new[] { "BookId", "ApplicationUserId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BookUsers_PostUserBookId_PostUserApplicationUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostUserBookId_PostUserApplicationUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostUserApplicationUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostUserBookId",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "BookUserApplicationUserId",
                table: "Posts",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookUserBookId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BookUserBookId_BookUserApplicationUserId",
                table: "Posts",
                columns: new[] { "BookUserBookId", "BookUserApplicationUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BookUsers_BookUserBookId_BookUserApplicationUserId",
                table: "Posts",
                columns: new[] { "BookUserBookId", "BookUserApplicationUserId" },
                principalTable: "BookUsers",
                principalColumns: new[] { "BookId", "ApplicationUserId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
