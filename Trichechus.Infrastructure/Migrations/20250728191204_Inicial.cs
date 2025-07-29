using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atividade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Situacao = table.Column<string>(type: "TEXT", nullable: true),
                    NomeResponsavel = table.Column<string>(type: "TEXT", nullable: true),
                    Prazo = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    NomeEquipeResponsavel = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    TipoEntrada = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividade", x => x.Id);
                });

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
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeAlias = table.Column<string>(type: "varchar(20)", nullable: false),
                    Objeto = table.Column<string>(type: "Text", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Inicio = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    Fim = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    AreaGestora = table.Column<string>(type: "varchar(10)", nullable: false),
                    Gerencia = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
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
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionalidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionalidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Equipe = table.Column<string>(type: "TEXT", nullable: false),
                    Matricula = table.Column<string>(type: "TEXT", nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "DateTime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Prazo = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    NomeResponsavel = table.Column<string>(type: "TEXT", nullable: true),
                    Situacao = table.Column<string>(type: "TEXT", nullable: true),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    DeletadoEm = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    AtividadeId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarefa_Atividade_AtividadeId",
                        column: x => x.AtividadeId,
                        principalTable: "Atividade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    EntrarCatalogo = table.Column<string>(type: "varchar(3)", nullable: false),
                    ContratoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Software_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id");
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
                        name: "FK_ContratoFornecedor_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoFornecedor_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilFuncionalidade",
                columns: table => new
                {
                    FuncionalidadeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PerfilId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilFuncionalidade", x => new { x.FuncionalidadeId, x.PerfilId });
                    table.ForeignKey(
                        name: "FK_PerfilFuncionalidade_Funcionalidade_FuncionalidadeId",
                        column: x => x.FuncionalidadeId,
                        principalTable: "Funcionalidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilFuncionalidade_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPerfil",
                columns: table => new
                {
                    PerfilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPerfil", x => new { x.PerfilId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuarioPerfil_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPerfil_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CatalogoEquipe = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
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
                name: "IX_ContratoFornecedor_FornecedorId",
                table: "ContratoFornecedor",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionalidade_Nome",
                table: "Funcionalidade",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Perfil_Nome",
                table: "Perfil",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilFuncionalidade_PerfilId",
                table: "PerfilFuncionalidade",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositorio_SoftwareId",
                table: "Repositorio",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Software_ContratoId",
                table: "Software",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_AtividadeId",
                table: "Tarefa",
                column: "AtividadeId");

            migrationBuilder.CreateIndex(
                name: "IX_URL_SoftwareId",
                table: "URL",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPerfil_UsuarioId",
                table: "UsuarioPerfil",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseDadosSoftware");

            migrationBuilder.DropTable(
                name: "Catalogo");

            migrationBuilder.DropTable(
                name: "ContratoFornecedor");

            migrationBuilder.DropTable(
                name: "PerfilFuncionalidade");

            migrationBuilder.DropTable(
                name: "Repositorio");

            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "URL");

            migrationBuilder.DropTable(
                name: "UsuarioPerfil");

            migrationBuilder.DropTable(
                name: "BaseDados");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Funcionalidade");

            migrationBuilder.DropTable(
                name: "Atividade");

            migrationBuilder.DropTable(
                name: "Software");

            migrationBuilder.DropTable(
                name: "Perfil");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Contrato");
        }
    }
}
