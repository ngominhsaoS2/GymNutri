using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class AddNutritionProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Carb",
                table: "TemplateMenuSets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Fat",
                table: "TemplateMenuSets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Kcal",
                table: "TemplateMenuSets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Protein",
                table: "TemplateMenuSets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SaturatedFat",
                table: "TemplateMenuSets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Carb",
                table: "TemplateMenus",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Fat",
                table: "TemplateMenus",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Kcal",
                table: "TemplateMenus",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Protein",
                table: "TemplateMenus",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SaturatedFat",
                table: "TemplateMenus",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carb",
                table: "TemplateMenuSets");

            migrationBuilder.DropColumn(
                name: "Fat",
                table: "TemplateMenuSets");

            migrationBuilder.DropColumn(
                name: "Kcal",
                table: "TemplateMenuSets");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "TemplateMenuSets");

            migrationBuilder.DropColumn(
                name: "SaturatedFat",
                table: "TemplateMenuSets");

            migrationBuilder.DropColumn(
                name: "Carb",
                table: "TemplateMenus");

            migrationBuilder.DropColumn(
                name: "Fat",
                table: "TemplateMenus");

            migrationBuilder.DropColumn(
                name: "Kcal",
                table: "TemplateMenus");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "TemplateMenus");

            migrationBuilder.DropColumn(
                name: "SaturatedFat",
                table: "TemplateMenus");
        }
    }
}
