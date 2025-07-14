using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class contrato_software : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContratoId",
                table: "Software",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Software_ContratoId",
                table: "Software",
                column: "ContratoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Software_Contratos_ContratoId",
                table: "Software",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Software_Contratos_ContratoId",
                table: "Software");

            migrationBuilder.DropIndex(
                name: "IX_Software_ContratoId",
                table: "Software");

            migrationBuilder.DropColumn(
                name: "ContratoId",
                table: "Software");
        }
    }
}
