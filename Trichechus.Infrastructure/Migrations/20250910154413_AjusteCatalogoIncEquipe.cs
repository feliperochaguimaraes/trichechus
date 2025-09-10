using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteCatalogoIncEquipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogoSoftware");

            migrationBuilder.DropColumn(
                name: "Gerencia",
                table: "Contrato");

            migrationBuilder.RenameColumn(
                name: "Equipe",
                table: "Usuario",
                newName: "GerenciaId");

            migrationBuilder.AddColumn<Guid>(
                name: "GerenciaId",
                table: "Software",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GerenciaId",
                table: "Contrato",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SoftwareId",
                table: "Catalogo",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Gerencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Gerencia = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Superintendencia = table.Column<string>(type: "varchar(50)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerencia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_GerenciaId",
                table: "Usuario",
                column: "GerenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Software_GerenciaId",
                table: "Software",
                column: "GerenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_GerenciaId",
                table: "Contrato",
                column: "GerenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogo_SoftwareId",
                table: "Catalogo",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalogo_Software_SoftwareId",
                table: "Catalogo",
                column: "SoftwareId",
                principalTable: "Software",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Gerencia_GerenciaId",
                table: "Contrato",
                column: "GerenciaId",
                principalTable: "Gerencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Software_Gerencia_GerenciaId",
                table: "Software",
                column: "GerenciaId",
                principalTable: "Gerencia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Gerencia_GerenciaId",
                table: "Usuario",
                column: "GerenciaId",
                principalTable: "Gerencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalogo_Software_SoftwareId",
                table: "Catalogo");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Gerencia_GerenciaId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Software_Gerencia_GerenciaId",
                table: "Software");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Gerencia_GerenciaId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Gerencia");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_GerenciaId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Software_GerenciaId",
                table: "Software");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_GerenciaId",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Catalogo_SoftwareId",
                table: "Catalogo");

            migrationBuilder.DropColumn(
                name: "GerenciaId",
                table: "Software");

            migrationBuilder.DropColumn(
                name: "GerenciaId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "SoftwareId",
                table: "Catalogo");

            migrationBuilder.RenameColumn(
                name: "GerenciaId",
                table: "Usuario",
                newName: "Equipe");

            migrationBuilder.AddColumn<string>(
                name: "Gerencia",
                table: "Contrato",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

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
    }
}
