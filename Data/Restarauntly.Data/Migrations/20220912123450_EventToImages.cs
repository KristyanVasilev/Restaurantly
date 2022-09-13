using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restarauntly.Data.Migrations
{
    public partial class EventToImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Dishes_DishId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "DishId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Dishes_DishId",
                table: "Images",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Dishes_DishId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "DishId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Dishes_DishId",
                table: "Images",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
