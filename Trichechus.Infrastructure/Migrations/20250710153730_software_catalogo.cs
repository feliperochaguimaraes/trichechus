using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class software_catalogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Fornecedores",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "AreaGestora",
                table: "Contratos",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gerencia",
                table: "Contratos",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BaseDados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cluster = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    NomeBaseDados = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Versao = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseDados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProdutoSoftware = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Situacao = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(type: "Text", nullable: true),
                    Segmento = table.Column<string>(type: "varchar(20)", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(30)", nullable: false),
                    LicencaSoftware = table.Column<string>(type: "varchar(3)", nullable: false),
                    Tecnologia = table.Column<string>(type: "Text", nullable: false),
                    Descontinuado = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    EntrarCatalogo = table.Column<string>(type: "varchar(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseDadosSoftware",
                columns: table => new
                {
                    BaseDadosId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SoftwareId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseDadosSoftware", x => new { x.BaseDadosId, x.SoftwareId });
                    table.ForeignKey(
                        name: "FK_BaseDadosSoftware_BaseDados_BaseDadosId",
                        column: x => x.BaseDadosId,
                        principalTable: "BaseDados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseDadosSoftware_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Catalogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    HelixId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    HelixEquipe = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    HelixService = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    HelixCategoria = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    HelixSubcategoria = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CatalogoEquipe = table.Column<string>(type: "varchar(39)", maxLength: 39, nullable: false),
                    Ativo = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Observacao = table.Column<string>(type: "Text", nullable: true),
                    SoftwareId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogo_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Repositorio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    SoftwareId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositorio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repositorio_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "URL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Ambiente = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Servidor = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    IP = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    SoftwareId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_URL_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseDadosSoftware_SoftwareId",
                table: "BaseDadosSoftware",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogo_SoftwareId",
                table: "Catalogo",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositorio_SoftwareId",
                table: "Repositorio",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_URL_SoftwareId",
                table: "URL",
                column: "SoftwareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseDadosSoftware");

            migrationBuilder.DropTable(
                name: "Catalogo");

            migrationBuilder.DropTable(
                name: "Repositorio");

            migrationBuilder.DropTable(
                name: "URL");

            migrationBuilder.DropTable(
                name: "BaseDados");

            migrationBuilder.DropTable(
                name: "Software");

            migrationBuilder.DropColumn(
                name: "AreaGestora",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "Gerencia",
                table: "Contratos");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Fornecedores",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");
        }
    }
}
