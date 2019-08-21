using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class removeCodesPropertiesUnneeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyClassificationCode",
                table: "UserBodyClassifications");

            migrationBuilder.DropColumn(
                name: "TemplateMenuCode",
                table: "TemplateMenuSets");

            migrationBuilder.DropColumn(
                name: "TemplateMenuSetCode",
                table: "TemplateMenuSetDetails");

            migrationBuilder.DropColumn(
                name: "BodyClassificationCode",
                table: "TemplateMenuForBodyClassifications");

            migrationBuilder.DropColumn(
                name: "FoodCategoryCode",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "MealCode",
                table: "DaisyOrderDetails");

            migrationBuilder.DropColumn(
                name: "SetOfFoodCode",
                table: "DaisyOrderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BodyClassificationCode",
                table: "UserBodyClassifications",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TemplateMenuCode",
                table: "TemplateMenuSets",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TemplateMenuSetCode",
                table: "TemplateMenuSetDetails",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BodyClassificationCode",
                table: "TemplateMenuForBodyClassifications",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FoodCategoryCode",
                table: "Foods",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MealCode",
                table: "DaisyOrderDetails",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SetOfFoodCode",
                table: "DaisyOrderDetails",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
