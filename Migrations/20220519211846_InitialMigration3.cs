using Microsoft.EntityFrameworkCore.Migrations;

namespace BookCollection.Migrations
{
    public partial class InitialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookUserApplicationUserId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookUserBookId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookUserApplicationUserId",
                table: "Likes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookUserBookId",
                table: "Likes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookUserApplicationUserId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookUserBookId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BookUserBookId_BookUserApplicationUserId",
                table: "Posts",
                columns: new[] { "BookUserBookId", "BookUserApplicationUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_BookUserBookId_BookUserApplicationUserId",
                table: "Likes",
                columns: new[] { "BookUserBookId", "BookUserApplicationUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookUserBookId_BookUserApplicationUserId",
                table: "Comments",
                columns: new[] { "BookUserBookId", "BookUserApplicationUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BookUsers_BookUserBookId_BookUserApplicationUserId",
                table: "Comments",
                columns: new[] { "BookUserBookId", "BookUserApplicationUserId" },
                principalTable: "BookUsers",
                principalColumns: new[] { "BookId", "ApplicationUserId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_BookUsers_BookUserBookId_BookUserApplicationUserId",
                table: "Likes",
                columns: new[] { "BookUserBookId", "BookUserApplicationUserId" },
                principalTable: "BookUsers",
                principalColumns: new[] { "BookId", "ApplicationUserId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BookUsers_BookUserBookId_BookUserApplicationUserId",
                table: "Posts",
                columns: new[] { "BookUserBookId", "BookUserApplicationUserId" },
                principalTable: "BookUsers",
                principalColumns: new[] { "BookId", "ApplicationUserId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BookUsers_BookUserBookId_BookUserApplicationUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_BookUsers_BookUserBookId_BookUserApplicationUserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BookUsers_BookUserBookId_BookUserApplicationUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BookUserBookId_BookUserApplicationUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Likes_BookUserBookId_BookUserApplicationUserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BookUserBookId_BookUserApplicationUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BookUserApplicationUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BookUserBookId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BookUserApplicationUserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "BookUserBookId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "BookUserApplicationUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BookUserBookId",
                table: "Comments");
        }
    }
}
