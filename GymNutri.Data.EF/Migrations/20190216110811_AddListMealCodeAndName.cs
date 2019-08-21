using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class AddListMealCodeAndName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListMealCodes",
                table: "SetOfFoods",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListMealNames",
                table: "SetOfFoods",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListMealCodes",
                table: "SetOfFoods");

            migrationBuilder.DropColumn(
                name: "ListMealNames",
                table: "SetOfFoods");
        }
    }
}
