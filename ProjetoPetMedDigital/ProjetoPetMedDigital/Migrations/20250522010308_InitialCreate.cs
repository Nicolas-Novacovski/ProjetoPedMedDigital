using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoPetMedDigital.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CadastroColaborador",
                columns: table => new
                {
                    IdColaborador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dtnascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false),
                    TipoUsuario = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastroColaborador", x => x.IdColaborador);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "ItemEstoque",
                columns: table => new
                {
                    IdProduto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: true),
                    PrecoCusto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PrecoVenda = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnidadeMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fornecedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransacaoDesejada = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEstoque", x => x.IdProduto);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdColaborador = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Login);
                    table.ForeignKey(
                        name: "FK_Usuario_CadastroColaborador_IdColaborador",
                        column: x => x.IdColaborador,
                        principalTable: "CadastroColaborador",
                        principalColumn: "IdColaborador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Veterinarios",
                columns: table => new
                {
                    IdVeterinario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeVeterinario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdColaborador = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarios", x => x.IdVeterinario);
                    table.ForeignKey(
                        name: "FK_Veterinarios_CadastroColaborador_IdColaborador",
                        column: x => x.IdColaborador,
                        principalTable: "CadastroColaborador",
                        principalColumn: "IdColaborador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    NomeCachorro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Problema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoAtendimento = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    SinaisVitais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recomendacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.IdPaciente);
                    table.ForeignKey(
                        name: "FK_Paciente_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valor",
                columns: table => new
                {
                    IdValor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorProcedimento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoPagamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorReceita = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorSaida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompraProdutos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valor", x => x.IdValor);
                    table.ForeignKey(
                        name: "FK_Valor_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Procedimento",
                columns: table => new
                {
                    IdProcedimento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProcedimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimento", x => x.IdProcedimento);
                    table.ForeignKey(
                        name: "FK_Procedimento_ItemEstoque_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "ItemEstoque",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    IdAgendamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdVeterinario = table.Column<int>(type: "int", nullable: false),
                    DataAgendamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraAgendamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.IdAgendamento);
                    table.ForeignKey(
                        name: "FK_Agendamento_Paciente_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Paciente",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Veterinarios_IdVeterinario",
                        column: x => x.IdVeterinario,
                        principalTable: "Veterinarios",
                        principalColumn: "IdVeterinario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgendaVeterinario",
                columns: table => new
                {
                    IdAgendaVeterinario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVeterinario = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    PacienteIdPaciente = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaVeterinario", x => x.IdAgendaVeterinario);
                    table.ForeignKey(
                        name: "FK_AgendaVeterinario_Paciente_PacienteIdPaciente",
                        column: x => x.PacienteIdPaciente,
                        principalTable: "Paciente",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendaVeterinario_Veterinarios_IdVeterinario",
                        column: x => x.IdVeterinario,
                        principalTable: "Veterinarios",
                        principalColumn: "IdVeterinario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacina",
                columns: table => new
                {
                    IdVacina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeVacina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duracao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacina", x => x.IdVacina);
                    table.ForeignKey(
                        name: "FK_Vacina_ItemEstoque_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "ItemEstoque",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacina_Paciente_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Paciente",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prontuario",
                columns: table => new
                {
                    IdProntuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAgendamento = table.Column<int>(type: "int", nullable: true),
                    IdVeterinario = table.Column<int>(type: "int", nullable: true),
                    DataConsulta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MotivoConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tratamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: true),
                    Temperatura = table.Column<int>(type: "int", nullable: true),
                    FrequenciaCardiaca = table.Column<int>(type: "int", nullable: true),
                    FrequenciaRespiratoria = table.Column<int>(type: "int", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prontuario", x => x.IdProntuario);
                    table.ForeignKey(
                        name: "FK_Prontuario_Agendamento_IdAgendamento",
                        column: x => x.IdAgendamento,
                        principalTable: "Agendamento",
                        principalColumn: "IdAgendamento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prontuario_Paciente_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Paciente",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prontuario_Veterinarios_IdVeterinario",
                        column: x => x.IdVeterinario,
                        principalTable: "Veterinarios",
                        principalColumn: "IdVeterinario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    IdServico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoVenda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeServico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdVeterinario = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAgendamento = table.Column<int>(type: "int", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    IdValor = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.IdServico);
                    table.ForeignKey(
                        name: "FK_Servico_Agendamento_IdAgendamento",
                        column: x => x.IdAgendamento,
                        principalTable: "Agendamento",
                        principalColumn: "IdAgendamento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servico_ItemEstoque_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "ItemEstoque",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servico_Valor_IdValor",
                        column: x => x.IdValor,
                        principalTable: "Valor",
                        principalColumn: "IdValor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servico_Veterinarios_IdVeterinario",
                        column: x => x.IdVeterinario,
                        principalTable: "Veterinarios",
                        principalColumn: "IdVeterinario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdPaciente",
                table: "Agendamento",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdVeterinario",
                table: "Agendamento",
                column: "IdVeterinario");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaVeterinario_IdVeterinario",
                table: "AgendaVeterinario",
                column: "IdVeterinario");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaVeterinario_PacienteIdPaciente",
                table: "AgendaVeterinario",
                column: "PacienteIdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_IdCliente",
                table: "Paciente",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimento_IdProduto",
                table: "Procedimento",
                column: "IdProduto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prontuario_IdAgendamento",
                table: "Prontuario",
                column: "IdAgendamento",
                unique: true,
                filter: "[IdAgendamento] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuario_IdPaciente",
                table: "Prontuario",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuario_IdVeterinario",
                table: "Prontuario",
                column: "IdVeterinario");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_IdAgendamento",
                table: "Servico",
                column: "IdAgendamento");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_IdProduto",
                table: "Servico",
                column: "IdProduto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servico_IdValor",
                table: "Servico",
                column: "IdValor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servico_IdVeterinario",
                table: "Servico",
                column: "IdVeterinario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdColaborador",
                table: "Usuario",
                column: "IdColaborador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacina_IdPaciente",
                table: "Vacina",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Vacina_IdProduto",
                table: "Vacina",
                column: "IdProduto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Valor_IdCliente",
                table: "Valor",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarios_IdColaborador",
                table: "Veterinarios",
                column: "IdColaborador",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaVeterinario");

            migrationBuilder.DropTable(
                name: "Procedimento");

            migrationBuilder.DropTable(
                name: "Prontuario");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Vacina");

            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "Valor");

            migrationBuilder.DropTable(
                name: "ItemEstoque");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Veterinarios");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "CadastroColaborador");
        }
    }
}
