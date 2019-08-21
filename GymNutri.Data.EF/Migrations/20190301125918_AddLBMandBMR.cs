using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class AddLBMandBMR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Bmr",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Lbm",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bmr",
                table: "UserBodyIndexes");

            migrationBuilder.DropColumn(
                name: "Lbm",
                table: "UserBodyIndexes");
        }
    }
}
