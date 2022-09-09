using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restarauntly.Data.Migrations
{
    public partial class AddedUserToDish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Dishes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_UserId",
                table: "Dishes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_AspNetUsers_UserId",
                table: "Dishes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_AspNetUsers_UserId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_UserId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Dishes");
        }
    }
}
