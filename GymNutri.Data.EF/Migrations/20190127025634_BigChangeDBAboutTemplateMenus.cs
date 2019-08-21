using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class BigChangeDBAboutTemplateMenus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateMenuDetails");

            migrationBuilder.CreateTable(
                name: "TemplateMenuSets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    TemplateMenuId = table.Column<int>(nullable: false),
                    TemplateMenuCode = table.Column<string>(maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMenuSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateMenuSets_TemplateMenus_TemplateMenuId",
                        column: x => x.TemplateMenuId,
                        principalTable: "TemplateMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMenuSetDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    TemplateMenuSetId = table.Column<int>(nullable: false),
                    TemplateMenuSetCode = table.Column<string>(maxLength: 255, nullable: false),
                    MealId = table.Column<int>(nullable: false),
                    MealCode = table.Column<string>(maxLength: 255, nullable: false),
                    SetOfFoodId = table.Column<int>(nullable: false),
                    SetOfFoodCode = table.Column<string>(maxLength: 255, nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMenuSetDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateMenuSetDetails_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateMenuSetDetails_SetOfFoods_SetOfFoodId",
                        column: x => x.SetOfFoodId,
                        principalTable: "SetOfFoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateMenuSetDetails_TemplateMenuSets_TemplateMenuSetId",
                        column: x => x.TemplateMenuSetId,
                        principalTable: "TemplateMenuSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuSetDetails_MealId",
                table: "TemplateMenuSetDetails",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuSetDetails_SetOfFoodId",
                table: "TemplateMenuSetDetails",
                column: "SetOfFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuSetDetails_TemplateMenuSetId",
                table: "TemplateMenuSetDetails",
                column: "TemplateMenuSetId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuSets_TemplateMenuId",
                table: "TemplateMenuSets",
                column: "TemplateMenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateMenuSetDetails");

            migrationBuilder.DropTable(
                name: "TemplateMenuSets");

            migrationBuilder.CreateTable(
                name: "TemplateMenuDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    MealCode = table.Column<string>(maxLength: 255, nullable: false),
                    MealId = table.Column<int>(nullable: false),
                    SetOfFoodCode = table.Column<string>(maxLength: 255, nullable: false),
                    SetOfFoodId = table.Column<int>(nullable: false),
                    TemplateMenuCode = table.Column<string>(maxLength: 255, nullable: false),
                    TemplateMenuId = table.Column<int>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMenuDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateMenuDetails_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateMenuDetails_TemplateMenus_TemplateMenuId",
                        column: x => x.TemplateMenuId,
                        principalTable: "TemplateMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuDetails_MealId",
                table: "TemplateMenuDetails",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMenuDetails_TemplateMenuId",
                table: "TemplateMenuDetails",
                column: "TemplateMenuId");
        }
    }
}
