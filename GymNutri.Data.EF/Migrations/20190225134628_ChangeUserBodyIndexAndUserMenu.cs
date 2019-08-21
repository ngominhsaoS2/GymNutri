using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class ChangeUserBodyIndexAndUserMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBodyClassifications");

            migrationBuilder.DropTable(
                name: "UserMenuDetails");

            migrationBuilder.AddColumn<int>(
                name: "TemplateMenuId",
                table: "UserMenus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BodyClassifications",
                table: "UserBodyIndexes",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IdiWproBmi",
                table: "UserBodyIndexes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_UserMenus_TemplateMenuId",
                table: "UserMenus",
                column: "TemplateMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMenus_TemplateMenus_TemplateMenuId",
                table: "UserMenus",
                column: "TemplateMenuId",
                principalTable: "TemplateMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMenus_TemplateMenus_TemplateMenuId",
                table: "UserMenus");

            migrationBuilder.DropIndex(
                name: "IX_UserMenus_TemplateMenuId",
                table: "UserMenus");

            migrationBuilder.DropColumn(
                name: "TemplateMenuId",
                table: "UserMenus");

            migrationBuilder.DropColumn(
                name: "BodyClassifications",
                table: "UserBodyIndexes");

            migrationBuilder.DropColumn(
                name: "IdiWproBmi",
                table: "UserBodyIndexes");

            migrationBuilder.CreateTable(
                name: "UserBodyClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    BodyClassificationId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBodyClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBodyClassifications_BodyClassifications_BodyClassificationId",
                        column: x => x.BodyClassificationId,
                        principalTable: "BodyClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    TemplateMenuId = table.Column<int>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserMenuId = table.Column<int>(nullable: false),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuDetails_TemplateMenus_TemplateMenuId",
                        column: x => x.TemplateMenuId,
                        principalTable: "TemplateMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuDetails_UserMenus_UserMenuId",
                        column: x => x.UserMenuId,
                        principalTable: "UserMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBodyClassifications_BodyClassificationId",
                table: "UserBodyClassifications",
                column: "BodyClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuDetails_TemplateMenuId",
                table: "UserMenuDetails",
                column: "TemplateMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuDetails_UserMenuId",
                table: "UserMenuDetails",
                column: "UserMenuId");
        }
    }
}
