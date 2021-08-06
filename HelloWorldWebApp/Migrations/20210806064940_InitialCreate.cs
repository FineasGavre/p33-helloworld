// <copyright file="20210806064940_InitialCreate.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

namespace HelloWorldWebApp.Migrations
{
    /// <summary>
    /// Migration for creating the TeamMembers table.
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// Up migration method.
        /// </summary>
        /// <param name="migrationBuilder">MigrationBuilder.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                });
        }

        /// <summary>
        /// Down migration method.
        /// </summary>
        /// <param name="migrationBuilder">MigrationBuilder.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMembers");
        }
    }
}
