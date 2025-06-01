using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoPetMedDigital.Migrations
{
    /// <inheritdoc />
    public partial class AjustarModelosParaNulidadeFKs2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaVeterinario_Paciente_PacienteIdPaciente",
                table: "AgendaVeterinario");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "CadastroColaborador");

            migrationBuilder.AlterColumn<int>(
                name: "PacienteIdPaciente",
                table: "AgendaVeterinario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaVeterinario_Paciente_PacienteIdPaciente",
                table: "AgendaVeterinario",
                column: "PacienteIdPaciente",
                principalTable: "Paciente",
                principalColumn: "IdPaciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaVeterinario_Paciente_PacienteIdPaciente",
                table: "AgendaVeterinario");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "CadastroColaborador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PacienteIdPaciente",
                table: "AgendaVeterinario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaVeterinario_Paciente_PacienteIdPaciente",
                table: "AgendaVeterinario",
                column: "PacienteIdPaciente",
                principalTable: "Paciente",
                principalColumn: "IdPaciente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
