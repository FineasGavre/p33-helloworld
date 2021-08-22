﻿// <auto-generated/>
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HelloWorldWebApp.Migrations
{
    public partial class InitialPostgresCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SkillMatrixUrl = table.Column<string>(type: "text", nullable: true),
                    InternId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Interns_InternId",
                        column: x => x.InternId,
                        principalTable: "Interns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibraryResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Recommendation = table.Column<string>(type: "text", nullable: true),
                    URL = table.Column<string>(type: "text", nullable: true),
                    SkillId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryResources_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Interns",
                columns: new[] { "Id", "Birthdate", "Email", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "fineasgavre@gmail.com", "Fineas Gavre" },
                    { 2, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "andreipopescu@gmail.com", "Andrei Popescu" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "InternId", "Name", "SkillMatrixUrl" },
                values: new object[,]
                {
                    { 1, "Nice skill 1", 1, "Skill1", "https://url.here" },
                    { 2, "Nice skill 2", 2, "Skill2", "https://url2.here" }
                });

            migrationBuilder.InsertData(
                table: "LibraryResources",
                columns: new[] { "Id", "Name", "Recommendation", "SkillId", "URL" },
                values: new object[,]
                {
                    { 1, "LibraryResource", "Good", 1, "https://libresources.com" },
                    { 2, "LibraryResource2", "Bad", 2, "https://libresources2.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryResources_SkillId",
                table: "LibraryResources",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_InternId",
                table: "Skills",
                column: "InternId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibraryResources");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Interns");
        }
    }
}
