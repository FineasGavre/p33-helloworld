using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HelloWorldWebApp.Migrations
{
    public partial class AddedDatabaseSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Interns",
                columns: new[] { "Id", "Birthdate", "Email", "Name" },
                values: new object[] { 1, new DateTime(2000, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "fineasgavre@gmail.com", "Fineas Gavre" });

            migrationBuilder.InsertData(
                table: "Interns",
                columns: new[] { "Id", "Birthdate", "Email", "Name" },
                values: new object[] { 2, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "andreipopescu@gmail.com", "Andrei Popescu" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "InternId", "Name", "SkillMatrixUrl" },
                values: new object[] { 1, "Nice skill 1", 1, "Skill1", "https://url.here" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "InternId", "Name", "SkillMatrixUrl" },
                values: new object[] { 2, "Nice skill 2", 2, "Skill2", "https://url2.here" });

            migrationBuilder.InsertData(
                table: "LibraryResources",
                columns: new[] { "Id", "Name", "Recommendation", "SkillId", "URL" },
                values: new object[] { 1, "LibraryResource", "Good", 1, "https://libresources.com" });

            migrationBuilder.InsertData(
                table: "LibraryResources",
                columns: new[] { "Id", "Name", "Recommendation", "SkillId", "URL" },
                values: new object[] { 2, "LibraryResource2", "Bad", 2, "https://libresources2.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LibraryResources",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LibraryResources",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Interns",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Interns",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
