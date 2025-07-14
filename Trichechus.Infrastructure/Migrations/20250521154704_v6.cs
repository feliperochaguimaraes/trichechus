using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Prazo",
                table: "Tarefas",
                type: "DateTime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletadoEm",
                table: "Tarefas",
                type: "DateTime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CriadoEm",
                table: "Tarefas",
                type: "DateTime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Tarefas",
                type: "DateTime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Prazo",
                table: "Atividades",
                type: "DateTime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletadoEm",
                table: "Atividades",
                type: "DateTime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Atividades",
                type: "DateTime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Prazo",
                table: "Tarefas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletadoEm",
                table: "Tarefas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CriadoEm",
                table: "Tarefas",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Tarefas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Prazo",
                table: "Atividades",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletadoEm",
                table: "Atividades",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Atividades",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldNullable: true);
        }
    }
}
