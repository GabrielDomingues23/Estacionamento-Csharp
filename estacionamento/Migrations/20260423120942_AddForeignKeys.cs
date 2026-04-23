using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Vagas");

            migrationBuilder.RenameColumn(
                name: "Ocupada",
                table: "Vagas",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "CarroId",
                table: "Vagas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraEntrada",
                table: "Vagas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroVaga",
                table: "Vagas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorCobrado",
                table: "Estadias",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Carros",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_CarroId",
                table: "Vagas",
                column: "CarroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vagas_Carros_CarroId",
                table: "Vagas",
                column: "CarroId",
                principalTable: "Carros",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vagas_Carros_CarroId",
                table: "Vagas");

            migrationBuilder.DropIndex(
                name: "IX_Vagas_CarroId",
                table: "Vagas");

            migrationBuilder.DropColumn(
                name: "CarroId",
                table: "Vagas");

            migrationBuilder.DropColumn(
                name: "HoraEntrada",
                table: "Vagas");

            migrationBuilder.DropColumn(
                name: "NumeroVaga",
                table: "Vagas");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Carros");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Vagas",
                newName: "Ocupada");

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Vagas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "ValorCobrado",
                table: "Estadias",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
