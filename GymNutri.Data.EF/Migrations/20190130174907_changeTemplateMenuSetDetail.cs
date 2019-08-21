using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class changeTemplateMenuSetDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealCode",
                table: "TemplateMenuSetDetails");

            migrationBuilder.DropColumn(
                name: "SetOfFoodCode",
                table: "TemplateMenuSetDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MealCode",
                table: "TemplateMenuSetDetails",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SetOfFoodCode",
                table: "TemplateMenuSetDetails",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
