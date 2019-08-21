using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class ChangeDecimalPropertyDemo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDay",
                table: "AppUsers",
                newName: "Birthday");

            migrationBuilder.AlterColumn<decimal>(
                name: "FatPerUnit",
                table: "Foods",
                type: "decimal(5,5)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "AppUsers",
                newName: "BirthDay");

            migrationBuilder.AlterColumn<decimal>(
                name: "FatPerUnit",
                table: "Foods",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,5)");
        }
    }
}
