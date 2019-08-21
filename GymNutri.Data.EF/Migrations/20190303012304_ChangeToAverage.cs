using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class ChangeToAverage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaturatedFat",
                table: "TemplateMenus",
                newName: "SaturatedFatAverage");

            migrationBuilder.RenameColumn(
                name: "Protein",
                table: "TemplateMenus",
                newName: "ProteinAverage");

            migrationBuilder.RenameColumn(
                name: "Kcal",
                table: "TemplateMenus",
                newName: "KcalAverage");

            migrationBuilder.RenameColumn(
                name: "Fat",
                table: "TemplateMenus",
                newName: "FatAverage");

            migrationBuilder.RenameColumn(
                name: "Carb",
                table: "TemplateMenus",
                newName: "CarbAverage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaturatedFatAverage",
                table: "TemplateMenus",
                newName: "SaturatedFat");

            migrationBuilder.RenameColumn(
                name: "ProteinAverage",
                table: "TemplateMenus",
                newName: "Protein");

            migrationBuilder.RenameColumn(
                name: "KcalAverage",
                table: "TemplateMenus",
                newName: "Kcal");

            migrationBuilder.RenameColumn(
                name: "FatAverage",
                table: "TemplateMenus",
                newName: "Fat");

            migrationBuilder.RenameColumn(
                name: "CarbAverage",
                table: "TemplateMenus",
                newName: "Carb");
        }
    }
}
