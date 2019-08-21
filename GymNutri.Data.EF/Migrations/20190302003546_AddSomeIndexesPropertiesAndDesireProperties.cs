using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class AddSomeIndexesPropertiesAndDesireProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangingSpeed",
                table: "UserDesires",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PracticeIntensive",
                table: "UserDesires",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightFt",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightIn",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightM",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangingSpeed",
                table: "UserDesires");

            migrationBuilder.DropColumn(
                name: "PracticeIntensive",
                table: "UserDesires");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "UserBodyIndexes");

            migrationBuilder.DropColumn(
                name: "HeightFt",
                table: "UserBodyIndexes");

            migrationBuilder.DropColumn(
                name: "HeightIn",
                table: "UserBodyIndexes");

            migrationBuilder.DropColumn(
                name: "HeightM",
                table: "UserBodyIndexes");
        }
    }
}
