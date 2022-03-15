using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendlyRS1.Repository.Migrations
{
    public partial class jj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Warning",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Warning",
                table: "AspNetUsers");
        }
    }
}
