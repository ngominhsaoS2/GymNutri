using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymNutri.Data.EF.Migrations
{
    public partial class ChangeUserMenuAndMemberCaredEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberCards_UserMenus_UserMenuId",
                table: "MemberCards");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "UserMenus");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "UserMenus",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "UserMenuId",
                table: "MemberCards",
                newName: "TemplateMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberCards_UserMenuId",
                table: "MemberCards",
                newName: "IX_MemberCards_TemplateMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberCards_TemplateMenus_TemplateMenuId",
                table: "MemberCards",
                column: "TemplateMenuId",
                principalTable: "TemplateMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberCards_TemplateMenus_TemplateMenuId",
                table: "MemberCards");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "UserMenus",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "TemplateMenuId",
                table: "MemberCards",
                newName: "UserMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberCards_TemplateMenuId",
                table: "MemberCards",
                newName: "IX_MemberCards_UserMenuId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UserMenus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_MemberCards_UserMenus_UserMenuId",
                table: "MemberCards",
                column: "UserMenuId",
                principalTable: "UserMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
