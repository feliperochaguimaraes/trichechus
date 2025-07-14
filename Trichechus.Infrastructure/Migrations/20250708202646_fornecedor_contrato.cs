using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fornecedor_contrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeAlias = table.Column<string>(type: "varchar(20)", nullable: false),
                    Objeto = table.Column<string>(type: "Text", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Inicio = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    Fim = table.Column<DateTime>(type: "DateTime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    CPFCNPJ = table.Column<string>(type: "varchar(20)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(10)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(30)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(20)", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContratoFornecedor",
                columns: table => new
                {
                    ContratoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoFornecedor", x => new { x.ContratoId, x.FornecedorId });
                    table.ForeignKey(
                        name: "FK_ContratoFornecedor_Contratos_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoFornecedor_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContratoFornecedor_FornecedorId",
                table: "ContratoFornecedor",
                column: "FornecedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContratoFornecedor");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Fornecedores");
        }
    }
}
