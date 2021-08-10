using Microsoft.EntityFrameworkCore.Migrations;

namespace HelloWorldWebApp.Migrations
{
    public partial class ChangedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryResources_Skills_SkillId",
                table: "LibraryResources");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Interns_InternId",
                table: "Skills");

            migrationBuilder.AlterColumn<int>(
                name: "InternId",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SkillId",
                table: "LibraryResources",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryResources_Skills_SkillId",
                table: "LibraryResources",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Interns_InternId",
                table: "Skills",
                column: "InternId",
                principalTable: "Interns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryResources_Skills_SkillId",
                table: "LibraryResources");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Interns_InternId",
                table: "Skills");

            migrationBuilder.AlterColumn<int>(
                name: "InternId",
                table: "Skills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SkillId",
                table: "LibraryResources",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryResources_Skills_SkillId",
                table: "LibraryResources",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Interns_InternId",
                table: "Skills",
                column: "InternId",
                principalTable: "Interns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
