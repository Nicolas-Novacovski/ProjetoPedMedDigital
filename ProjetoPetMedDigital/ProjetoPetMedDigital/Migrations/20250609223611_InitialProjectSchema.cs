using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoPetMedDigital.Migrations
{
    /// <inheritdoc />
    public partial class InitialProjectSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeResponsavel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    RG = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DtNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                    NomeProduto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: true),
                    PrecoCusto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PrecoVenda = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnidadeMedida = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fornecedor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TransacaoDesejada = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEstoque", x => x.IdProduto);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CadastroColaborador",
                columns: table => new
                {
                    IdColaborador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    RG = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Dtnascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false),
                    TipoUsuario = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastroColaborador", x => x.IdColaborador);
                    table.ForeignKey(
                        name: "FK_CadastroColaborador_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    NomeCachorro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Problema = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TipoAtendimento = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    SinaisVitais = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Recomendacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    TipoPagamento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    NomeProcedimento = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                name: "Veterinarios",
                columns: table => new
                {
                    IdVeterinario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeVeterinario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacina",
                columns: table => new
                {
                    IdVacina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeVacina = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Duracao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                name: "Agendamento",
                columns: table => new
                {
                    IdAgendamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdVeterinario = table.Column<int>(type: "int", nullable: false),
                    DataAgendamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraAgendamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    PacienteIdPaciente = table.Column<int>(type: "int", nullable: true),
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
                        principalColumn: "IdPaciente");
                    table.ForeignKey(
                        name: "FK_AgendaVeterinario_Veterinarios_IdVeterinario",
                        column: x => x.IdVeterinario,
                        principalTable: "Veterinarios",
                        principalColumn: "IdVeterinario",
                        onDelete: ReferentialAction.Restrict);
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
                    MotivoConsulta = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Tratamento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: true),
                    Temperatura = table.Column<int>(type: "int", nullable: true),
                    FrequenciaCardiaca = table.Column<int>(type: "int", nullable: true),
                    FrequenciaRespiratoria = table.Column<int>(type: "int", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                    TipoVenda = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeServico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdVeterinario = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CadastroColaborador_IdentityUserId",
                table: "CadastroColaborador",
                column: "IdentityUserId",
                unique: true);

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Procedimento");

            migrationBuilder.DropTable(
                name: "Prontuario");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Vacina");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

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

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
