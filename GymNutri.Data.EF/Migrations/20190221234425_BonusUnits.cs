using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class BonusUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "UserBodyIndexes",
                newName: "WeightKg");

            migrationBuilder.RenameColumn(
                name: "Waist",
                table: "UserBodyIndexes",
                newName: "WaistCm");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "UserBodyIndexes",
                newName: "HeightCm");

            migrationBuilder.RenameColumn(
                name: "Chest",
                table: "UserBodyIndexes",
                newName: "ChestCm");

            migrationBuilder.RenameColumn(
                name: "Ass",
                table: "UserBodyIndexes",
                newName: "AssCm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeightKg",
                table: "UserBodyIndexes",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "WaistCm",
                table: "UserBodyIndexes",
                newName: "Waist");

            migrationBuilder.RenameColumn(
                name: "HeightCm",
                table: "UserBodyIndexes",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "ChestCm",
                table: "UserBodyIndexes",
                newName: "Chest");

            migrationBuilder.RenameColumn(
                name: "AssCm",
                table: "UserBodyIndexes",
                newName: "Ass");
        }
    }
}
