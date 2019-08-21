using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class AddClassCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassCode1",
                table: "CommonCategories ",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassCode2",
                table: "CommonCategories ",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassCode3",
                table: "CommonCategories ",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassCode1",
                table: "CommonCategories ");

            migrationBuilder.DropColumn(
                name: "ClassCode2",
                table: "CommonCategories ");

            migrationBuilder.DropColumn(
                name: "ClassCode3",
                table: "CommonCategories ");
        }
    }
}
