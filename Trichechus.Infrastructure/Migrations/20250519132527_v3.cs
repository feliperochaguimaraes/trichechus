using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concluida",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "Atividades",
                newName: "CriadoEm");

            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Tarefas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletadoEm",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeResponsavel",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Prazo",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Situacao",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletadoEm",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeEquipeResponsavel",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeResponsavel",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Prazo",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Situacao",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoEntrada",
                table: "Atividades",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "DeletadoEm",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "NomeResponsavel",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Prazo",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "DeletadoEm",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "NomeEquipeResponsavel",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "NomeResponsavel",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Prazo",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "TipoEntrada",
                table: "Atividades");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "Atividades",
                newName: "DataCriacao");

            migrationBuilder.AddColumn<bool>(
                name: "Concluida",
                table: "Tarefas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tarefas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
