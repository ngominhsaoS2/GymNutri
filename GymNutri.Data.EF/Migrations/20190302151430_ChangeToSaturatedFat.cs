using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class ChangeToSaturatedFat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FatSaturated",
                table: "SetOfFoods",
                newName: "SaturatedFat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaturatedFat",
                table: "SetOfFoods",
                newName: "FatSaturated");
        }
    }
}
