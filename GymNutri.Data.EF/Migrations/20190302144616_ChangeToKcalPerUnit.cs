using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class ChangeToKcalPerUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Kcal",
                table: "Foods",
                newName: "KcalPerUnit");

            migrationBuilder.AddColumn<decimal>(
                name: "Carb",
                table: "SetOfFoods",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Fat",
                table: "SetOfFoods",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Kcal",
                table: "SetOfFoods",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Protein",
                table: "SetOfFoods",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Saturated",
                table: "SetOfFoods",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carb",
                table: "SetOfFoods");

            migrationBuilder.DropColumn(
                name: "Fat",
                table: "SetOfFoods");

            migrationBuilder.DropColumn(
                name: "Kcal",
                table: "SetOfFoods");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "SetOfFoods");

            migrationBuilder.DropColumn(
                name: "Saturated",
                table: "SetOfFoods");

            migrationBuilder.RenameColumn(
                name: "KcalPerUnit",
                table: "Foods",
                newName: "Kcal");
        }
    }
}
