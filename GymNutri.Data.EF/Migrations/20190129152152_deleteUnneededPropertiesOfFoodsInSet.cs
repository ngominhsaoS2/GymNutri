using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class deleteUnneededPropertiesOfFoodsInSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodCode",
                table: "FoodsInSets");

            migrationBuilder.DropColumn(
                name: "SetOfFoodCode",
                table: "FoodsInSets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FoodCode",
                table: "FoodsInSets",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SetOfFoodCode",
                table: "FoodsInSets",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
