using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class Add3Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    MemberTypeCode = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    MonthDuration = table.Column<int>(nullable: false),
                    TemplateMenuId = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PromotionPrice = table.Column<decimal>(nullable: false),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberCards_TemplateMenus_TemplateMenuId",
                        column: x => x.TemplateMenuId,
                        principalTable: "TemplateMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberCards_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DaisyOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    MemberCardId = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderInDay = table.Column<int>(nullable: false),
                    PlanOrderTime = table.Column<TimeSpan>(nullable: false),
                    RealOrderTime = table.Column<TimeSpan>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    AlternativePersonName = table.Column<string>(maxLength: 255, nullable: true),
                    AlternativePersonPhone = table.Column<string>(maxLength: 255, nullable: true),
                    AlternativePersonEmail = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    UserFeedback = table.Column<string>(maxLength: 500, nullable: true),
                    SeoPageTitle = table.Column<string>(maxLength: 500, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 500, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 500, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaisyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaisyOrders_LocationToGetOrderOfUsers_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LocationToGetOrderOfUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaisyOrders_MemberCards_MemberCardId",
                        column: x => x.MemberCardId,
                        principalTable: "MemberCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DaisyOrderDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    DaisyOrderId = table.Column<long>(nullable: false),
                    MealId = table.Column<int>(nullable: false),
                    SetOfFoodId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    RealEatTime = table.Column<TimeSpan>(nullable: false),
                    UserFeedback = table.Column<string>(maxLength: 500, nullable: true),
                    UserCreated = table.Column<string>(maxLength: 255, nullable: true),
                    UserModified = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaisyOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaisyOrderDetails_DaisyOrders_DaisyOrderId",
                        column: x => x.DaisyOrderId,
                        principalTable: "DaisyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaisyOrderDetails_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DaisyOrderDetails_SetOfFoods_SetOfFoodId",
                        column: x => x.SetOfFoodId,
                        principalTable: "SetOfFoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrderDetails_DaisyOrderId",
                table: "DaisyOrderDetails",
                column: "DaisyOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrderDetails_MealId",
                table: "DaisyOrderDetails",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrderDetails_SetOfFoodId",
                table: "DaisyOrderDetails",
                column: "SetOfFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrders_LocationId",
                table: "DaisyOrders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DaisyOrders_MemberCardId",
                table: "DaisyOrders",
                column: "MemberCardId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_TemplateMenuId",
                table: "MemberCards",
                column: "TemplateMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_UserId",
                table: "MemberCards",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaisyOrderDetails");

            migrationBuilder.DropTable(
                name: "DaisyOrders");

            migrationBuilder.DropTable(
                name: "MemberCards");
        }
    }
}
