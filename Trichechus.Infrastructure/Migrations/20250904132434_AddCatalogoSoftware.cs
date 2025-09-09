using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCatalogoSoftware : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalogo_Software_SoftwareId",
                table: "Catalogo");

            migrationBuilder.DropIndex(
                name: "IX_Catalogo_SoftwareId",
                table: "Catalogo");

            migrationBuilder.DropColumn(
                name: "SoftwareId",
                table: "Catalogo");

            migrationBuilder.CreateTable(
                name: "CatalogoSoftware",
                columns: table => new
                {
                    CatalogoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SoftwareId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoSoftware", x => new { x.CatalogoId, x.SoftwareId });
                    table.ForeignKey(
                        name: "FK_CatalogoSoftware_Catalogo_CatalogoId",
                        column: x => x.CatalogoId,
                        principalTable: "Catalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogoSoftware_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoSoftware_SoftwareId",
                table: "CatalogoSoftware",
                column: "SoftwareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogoSoftware");

            migrationBuilder.AddColumn<Guid>(
                name: "SoftwareId",
                table: "Catalogo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Catalogo_SoftwareId",
                table: "Catalogo",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalogo_Software_SoftwareId",
                table: "Catalogo",
                column: "SoftwareId",
                principalTable: "Software",
                principalColumn: "Id");
        }
    }
}
