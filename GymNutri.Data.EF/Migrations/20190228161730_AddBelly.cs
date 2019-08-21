using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class AddBelly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BellyCm",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BellyIn",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WeightLb",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BellyCm",
                table: "UserBodyIndexes");

            migrationBuilder.DropColumn(
                name: "BellyIn",
                table: "UserBodyIndexes");

            migrationBuilder.DropColumn(
                name: "WeightLb",
                table: "UserBodyIndexes");
        }
    }
}
