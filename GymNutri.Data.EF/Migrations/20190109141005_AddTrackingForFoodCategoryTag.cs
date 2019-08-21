using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class AddTrackingForFoodCategoryTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Tags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Tags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Tags",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Tags",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "FoodCategoryTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "FoodCategoryTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "FoodCategoryTags",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "FoodCategoryTags",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "FoodCategoryTags");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "FoodCategoryTags");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "FoodCategoryTags");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "FoodCategoryTags");
        }
    }
}
