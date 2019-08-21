using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class RemoveTemplateMenuForBodyClassification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateMenuForBodyClassifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateMenuForBodyClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    BodyClassificationId = table.Column<int>(maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    TemplateMenuId = table.Column<int>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMenuForBodyClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateMenuForBodyClassifications_BodyClassifications_BodyClassificationId",
                        column: x => x.BodyClassificationId,
                        principalTable: "BodyClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateMenuForBodyClassifications_TemplateMenus_TemplateMenuId",
                        column: x => x.TemplateMenuId,
                        principalTable: "TemplateMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuForBodyClassifications_BodyClassificationId",
                table: "TemplateMenuForBodyClassifications",
                column: "BodyClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuForBodyClassifications_TemplateMenuId",
                table: "TemplateMenuForBodyClassifications",
                column: "TemplateMenuId");
        }
    }
}
