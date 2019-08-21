using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class RemoveTemplateMenuCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateMenuCode",
                table: "TemplateMenuForBodyClassifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TemplateMenuCode",
                table: "TemplateMenuForBodyClassifications",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
