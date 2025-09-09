using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trichechus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrecaoName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome1",
                table: "Fornecedor");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Fornecedor",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Fornecedor",
                type: "varchar(250)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Fornecedor");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Fornecedor",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AddColumn<string>(
                name: "Nome1",
                table: "Fornecedor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
