using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoftwareURL");

            migrationBuilder.AddColumn<Guid>(
                name: "SoftwareId",
                table: "URL",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_URL_SoftwareId",
                table: "URL",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_URL_Software_SoftwareId",
                table: "URL",
                column: "SoftwareId",
                principalTable: "Software",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_URL_Software_SoftwareId",
                table: "URL");

            migrationBuilder.DropIndex(
                name: "IX_URL_SoftwareId",
                table: "URL");

            migrationBuilder.DropColumn(
                name: "SoftwareId",
                table: "URL");

            migrationBuilder.CreateTable(
                name: "SoftwareURL",
                columns: table => new
                {
                    SoftwareId = table.Column<Guid>(type: "TEXT", nullable: false),
                    URLId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareURL", x => new { x.SoftwareId, x.URLId });
                    table.ForeignKey(
                        name: "FK_SoftwareURL_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoftwareURL_URL_URLId",
                        column: x => x.URLId,
                        principalTable: "URL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareURL_URLId",
                table: "SoftwareURL",
                column: "URLId");
        }
    }
}
