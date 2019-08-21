using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class AddFoodInfor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CarbPerUnit",
                table: "Foods",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FatPerUnit",
                table: "Foods",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProteinPerUnit",
                table: "Foods",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SaturatedFatPerUnit",
                table: "Foods",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbPerUnit",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "FatPerUnit",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "ProteinPerUnit",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "SaturatedFatPerUnit",
                table: "Foods");
        }
    }
}
