using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restarauntly.Data.Migrations
{
    public partial class RemoveMyPropFromEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
