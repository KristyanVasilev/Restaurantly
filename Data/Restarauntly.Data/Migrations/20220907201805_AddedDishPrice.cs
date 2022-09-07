using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restarauntly.Data.Migrations
{
    public partial class AddedDishPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Dishes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Dishes");
        }
    }
}
