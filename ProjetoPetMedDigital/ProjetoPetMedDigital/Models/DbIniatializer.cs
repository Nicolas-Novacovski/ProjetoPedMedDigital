using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProjetoPetMedDigital.Data;
using ProjetoPetMedDigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bogus; // Necessário para a classe Faker
using Bogus.Extensions; // Para métodos de extensão como .OrDefault()
using Bogus.Extensions.Brazil; // <<< NOVO E CRÍTICO PARA CPF, RG (se usar Person.Cpf/Rg) e outros dados brasileiros
// Usar Bogus.DataSets para acesso direto a 'Name', 'Phone', 'Internet', 'Address', 'Company', 'Lorem', 'Commerce'.
// Removido: using Bogus.DataSets; se já está implícito ou não necessário
// Se ainda tiver problemas com 'Name', 'Phone', etc., pode ser necessário adicionar 'using Bogus.DataSets;'
// ou usar 'faker.Name.FullName()' etc.

namespace ProjetoPetMedDigital.Data.Initializer
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<PetMedContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // --- 1. Seed de Perfis (Roles) ---
            string[] roleNames = { "Administrador", "Veterinario", "Secretaria" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // --- 2. Seed de Usuário Administrador Padrão ---
            string adminEmail = "admin@petmed.com";
            string adminPassword = "Admin@123";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrador");

                    if (!context.CadastroColaboradores.Any(cc => cc.IdentityUserId == adminUser.Id))
                    {
                        var adminColaborador = new CadastroColaborador
                        {
                            Nome = new Faker("pt_BR").Name.FullName(), // Usar new Faker().Name
                            Telefone = new Faker("pt_BR").Phone.PhoneNumber("##########"),
                            Email = adminEmail,
                            CPF = new Faker("pt_BR").Person.Cpf(), // Requer Bogus.Extensions.Brazil
                            RG = new Faker("pt_BR").Random.Replace("##.###.###-#"),
                            Dtnascimento = new Faker("pt_BR").Date.Past(40, DateTime.Now.AddYears(-20)),
                            CEP = new Faker("pt_BR").Address.ZipCode("########"),
                            Endereco = new Faker("pt_BR").Address.StreetAddress(),
                            Bairro = new Faker("pt_BR").Address.County(),
                            Cidade = new Faker("pt_BR").Address.City(),
                            Cargo = 1,
                            TipoUsuario = 1,
                            IdentityUserId = adminUser.Id,
                            CreatedAt = DateTime.UtcNow
                        };
                        context.CadastroColaboradores.Add(adminColaborador);
                        await context.SaveChangesAsync();
                    }
                }
            }

            // --- 3. Seed de Dados Falsos para Suas Entidades ---
            var faker = new Bogus.Faker("pt_BR"); // Instância global do Faker

            // Seed Clientes
            if (!context.Clientes.Any())
            {
                var clienteFaker = new Faker<Cliente>("pt_BR")
                    .RuleFor(c => c.NomeResponsavel, f => f.Name.FullName())
                    .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("##########"))
                    .RuleFor(c => c.Email, f => f.Internet.Email())
                    .RuleFor(c => c.CPF, f => f.Person.Cpf())
                    .RuleFor(c => c.RG, f => f.Random.Replace("##.###.###-#"))
                    .RuleFor(c => c.DtNascimento, f => f.Date.Past(60, DateTime.Now.AddYears(-18)))
                    .RuleFor(c => c.CEP, f => f.Address.ZipCode("########"))
                    .RuleFor(c => c.Endereco, f => f.Address.StreetAddress())
                    .RuleFor(c => c.Bairro, f => f.Address.County())
                    .RuleFor(c => c.Cidade, f => f.Address.City())
                    .RuleFor(c => c.CreatedAt, f => f.Date.Past(2));

                var clientes = clienteFaker.Generate(50);
                context.Clientes.AddRange(clientes);
                await context.SaveChangesAsync();
            }

            // Seed Pacientes
            if (!context.Pacientes.Any() && context.Clientes.Any())
            {
                var clientesExistentes = await context.Clientes.ToListAsync();
                var pacienteFaker = new Faker<Paciente>("pt_BR")
                    .RuleFor(p => p.IdCliente, f => f.PickRandom(clientesExistentes).IdCliente)
                    .RuleFor(p => p.NomeCachorro, f => f.Name.FirstName(f.Person.Gender == Bogus.Gender.Male ? Bogus.Data.Gender.Male : Bogus.Data.Gender.Female))
                    .RuleFor(p => p.Estado, f => f.Random.Int(1, 3))
                    .RuleFor(p => p.Problema, f => f.Lorem.Sentence(5))
                    .RuleFor(p => p.TipoAtendimento, f => f.Random.Int(1, 3))
                    .RuleFor(p => p.Peso, f => (float)f.Random.Double(1, 40))
                    .RuleFor(p => p.SinaisVitais, f => f.Lorem.Sentence(10))
                    .RuleFor(p => p.Recomendacoes, f => f.Lorem.Sentence(10))
                    .RuleFor(p => p.CreatedAt, f => f.Date.Past(1));

                var pacientes = pacienteFaker.Generate(100);
                context.Pacientes.AddRange(pacientes);
                await context.SaveChangesAsync();
            }

            // Seed Veterinários
            if (!context.Veterinarios.Any())
            {
                // Criar IdentityUsers para Veterinários e Secretarias, se não existirem
                var vetUser = await userManager.FindByEmailAsync("vet@petmed.com");
                if (vetUser == null)
                {
                    vetUser = new IdentityUser { UserName = "vet@petmed.com", Email = "vet@petmed.com", EmailConfirmed = true };
                    await userManager.CreateAsync(vetUser, "Veterinario@123");
                    await userManager.AddToRoleAsync(vetUser, "Veterinario");
                }
                var secUser = await userManager.FindByEmailAsync("sec@petmed.com");
                if (secUser == null)
                {
                    secUser = new IdentityUser { UserName = "sec@petmed.com", Email = "sec@petmed.com", EmailConfirmed = true };
                    await userManager.CreateAsync(secUser, "Secretaria@123");
                    await userManager.AddToRoleAsync(secUser, "Secretaria");
                }

                // Criar CadastroColaboradores para Vet e Sec IdentityUsers (se não existirem)
                if (!context.CadastroColaboradores.Any(cc => cc.IdentityUserId == vetUser.Id))
                {
                    context.CadastroColaboradores.Add(new CadastroColaborador
                    {
                        Nome = faker.Name.FullName(),
                        Telefone = faker.Phone.PhoneNumber("##########"),
                        Email = vetUser.Email,
                        CPF = faker.Person.Cpf(),
                        RG = faker.Random.Replace("##.###.###-#"),
                        Dtnascimento = faker.Date.Past(35, DateTime.Now.AddYears(-25)),
                        CEP = faker.Address.ZipCode("########"),
                        Endereco = faker.Address.StreetAddress(),
                        Bairro = faker.Address.County(),
                        Cidade = faker.Address.City(),
                        Cargo = 2,
                        TipoUsuario = 2,
                        IdentityUserId = vetUser.Id,
                        CreatedAt = DateTime.UtcNow
                    });
                    await context.SaveChangesAsync();
                }
                if (!context.CadastroColaboradores.Any(cc => cc.IdentityUserId == secUser.Id))
                {
                    context.CadastroColaboradores.Add(new CadastroColaborador
                    {
                        Nome = faker.Name.FullName(),
                        Telefone = faker.Phone.PhoneNumber("##########"),
                        Email = secUser.Email,
                        CPF = faker.Person.Cpf(),
                        RG = faker.Random.Replace("##.###.###-#"),
                        Dtnascimento = faker.Date.Past(30, DateTime.Now.AddYears(-20)),
                        CEP = faker.Address.ZipCode("########"),
                        Endereco = faker.Address.StreetAddress(),
                        Bairro = faker.Address.County(),
                        Cidade = faker.Address.City(),
                        Cargo = 3,
                        TipoUsuario = 3,
                        IdentityUserId = secUser.Id,
                        CreatedAt = DateTime.UtcNow
                    });
                    await context.SaveChangesAsync();
                }

                // Gerar mais 8 veterinários (totalizando 10 com os 2 acima)
                var veterinarioFaker = new Faker<Veterinario>("pt_BR")
                    .RuleFor(v => v.NomeVeterinario, f => f.Name.FullName())
                    .RuleFor(v => v.Especialidade, f => f.Lorem.Word())
                    .RuleFor(v => v.Telefone, f => f.Phone.PhoneNumber("##########"))
                    .RuleFor(v => v.Email, f => f.Internet.Email())
                    .RuleFor(v => v.CreatedAt, f => f.Date.Past(5));

                // Cria colaboradores e usuários Identity para os veterinários adicionais
                for (int i = 0; i < 8; i++)
                {
                    var newVetEmail = $"vet{i + 2}@petmed.com";
                    var newIdentityUser = await userManager.FindByEmailAsync(newVetEmail);
                    if (newIdentityUser == null)
                    {
                        newIdentityUser = new IdentityUser { UserName = newVetEmail, Email = newVetEmail, EmailConfirmed = true };
                        await userManager.CreateAsync(newIdentityUser, "Veterinario@123");
                        await userManager.AddToRoleAsync(newIdentityUser, "Veterinario");
                    }

                    var newColaborador = new CadastroColaborador
                    {
                        Nome = faker.Name.FullName(),
                        Telefone = faker.Phone.PhoneNumber("##########"),
                        Email = newVetEmail,
                        CPF = faker.Person.Cpf(),
                        RG = faker.Random.Replace("##.###.###-#"),
                        Dtnascimento = faker.Date.Past(30, DateTime.Now.AddYears(-20)),
                        CEP = faker.Address.ZipCode("########"),
                        Endereco = faker.Address.StreetAddress(),
                        Bairro = faker.Address.County(),
                        Cidade = faker.Address.City(),
                        Cargo = 2,
                        TipoUsuario = 2,
                        IdentityUserId = newIdentityUser.Id,
                        CreatedAt = DateTime.UtcNow
                    };
                    context.CadastroColaboradores.Add(newColaborador);
                    await context.SaveChangesAsync();

                    context.Veterinarios.Add(new Veterinario
                    {
                        NomeVeterinario = newColaborador.Nome,
                        Especialidade = faker.Lorem.Word(),
                        Telefone = newColaborador.Telefone,
                        Email = newColaborador.Email,
                        IdColaborador = newColaborador.IdColaborador,
                        CreatedAt = DateTime.UtcNow
                    });
                    await context.SaveChangesAsync();
                }
            }

            // Seed ItemEstoque
            if (!context.ItensEstoque.Any())
            {
                var itemEstoqueFaker = new Faker<ItemEstoque>("pt_BR")
                    .RuleFor(i => i.NomeProduto, f => f.Commerce.ProductName() + (f.Random.Bool() ? " (Vacina)" : " (Remédio)"))
                    .RuleFor(i => i.Descricao, f => f.Lorem.Sentence(5))
                    .RuleFor(i => i.Quantidade, f => f.Random.Int(10, 200))
                    .RuleFor(i => i.PrecoCusto, f => (decimal)f.Finance.Amount(5, 50))
                    .RuleFor(i => i.PrecoVenda, f => (decimal)f.Finance.Amount(10, 100))
                    .RuleFor(i => i.UnidadeMedida, f => f.Commerce.Unit())
                    .RuleFor(i => i.DataValidade, f => f.Date.Soon(730)) // Válido pelos próximos 2 anos
                    .RuleFor(i => i.Fornecedor, f => f.Company.CompanyName())
                    .RuleFor(i => i.TransacaoDesejada, f => f.Random.Int(1, 5))
                    .RuleFor(i => i.CreatedAt, f => f.Date.Past(1));

                var itensEstoque = itemEstoqueFaker.Generate(40);
                context.ItensEstoque.AddRange(itensEstoque);
                await context.SaveChangesAsync();
            }

            // Seed Procedimentos
            if (!context.Procedimentos.Any() && context.ItensEstoque.Any())
            {
                var itensEstoqueExistentes = await context.ItensEstoque.ToListAsync();
                var procedimentoFaker = new Faker<Procedimento>("pt_BR")
                    .RuleFor(p => p.NomeProcedimento, f => f.Commerce.ProductAdjective() + " " + f.Commerce.Product())
                    .RuleFor(p => p.Descricao, f => f.Lorem.Sentence(5))
                    .RuleFor(p => p.Valor, f => (decimal)f.Finance.Amount(50, 500))
                    .RuleFor(p => p.IdProduto, f => f.PickRandom(itensEstoqueExistentes).IdProduto)
                    .RuleFor(p => p.CreatedAt, f => f.Date.Past(1));

                var procedimentos = procedimentoFaker.Generate(30);
                context.Procedimentos.AddRange(procedimentos);
                await context.SaveChangesAsync();
            }

            // Seed Vacinas
            if (!context.Vacinas.Any() && context.ItensEstoque.Any() && context.Pacientes.Any())
            {
                var itensEstoqueVacina = await context.ItensEstoque.Where(ie => ie.NomeProduto.ToLower().Contains("vacina")).ToListAsync();
                var pacientesExistentes = await context.Pacientes.ToListAsync();

                if (itensEstoqueVacina.Any() && pacientesExistentes.Any())
                {
                    var vacinaFaker = new Faker<Vacina>("pt_BR")
                        .RuleFor(v => v.NomeVacina, f => f.Commerce.ProductAdjective() + " Vacina " + f.Random.String2(3, "ABC"))
                        .RuleFor(v => v.Descricao, f => f.Lorem.Sentence(5))
                        .RuleFor(v => v.Duracao, f => $"{f.Random.Int(6, 12)} meses")
                        .RuleFor(v => v.IdProduto, f => f.PickRandom(itensEstoqueVacina).IdProduto)
                        .RuleFor(v => v.IdPaciente, f => f.PickRandom(pacientesExistentes).IdPaciente)
                        .RuleFor(v => v.CreatedAt, f => f.Date.Past(1));

                    var vacinas = vacinaFaker.Generate(40);
                    context.Vacinas.AddRange(vacinas);
                    await context.SaveChangesAsync();
                }
            }

            // Seed Valores
            if (!context.Valores.Any() && context.Clientes.Any())
            {
                var clientesExistentes = await context.Clientes.ToListAsync();
                var valorFaker = new Faker<Valor>("pt_BR")
                    .RuleFor(v => v.ValorProcedimento, f => (decimal)f.Finance.Amount(100, 400))
                    .RuleFor(v => v.TipoPagamento, f => f.PickRandom("Dinheiro", "Cartao", "Pix"))
                    .RuleFor(v => v.ValorReceita, f => (decimal)f.Finance.Amount(50, 500))
                    .RuleFor(v => v.ValorSaida, f => (decimal)f.Finance.Amount(20, 200))
                    .RuleFor(v => v.Salario, f => (decimal)f.Finance.Amount(1500, 5000))
                    .RuleFor(v => v.CompraProdutos, f => (decimal)f.Finance.Amount(100, 1000))
                    .RuleFor(v => v.IdCliente, f => f.PickRandom(clientesExistentes).IdCliente)
                    .RuleFor(v => v.CreatedAt, f => f.Date.Past(2));

                var valores = valorFaker.Generate(50);
                context.Valores.AddRange(valores);
                await context.SaveChangesAsync();
            }

            // Seed Agendamentos
            if (!context.Agendamentos.Any() && context.Pacientes.Any() && context.Veterinarios.Any())
            {
                var pacientesExistentes = await context.Pacientes.ToListAsync();
                var veterinariosExistentes = await context.Veterinarios.ToListAsync();

                var agendamentoFaker = new Faker<Agendamento>("pt_BR")
                    .RuleFor(a => a.IdPaciente, f => f.PickRandom(pacientesExistentes).IdPaciente)
                    .RuleFor(a => a.IdVeterinario, f => f.PickRandom(veterinariosExistentes).IdVeterinario)
                    .RuleFor(a => a.DataAgendamento, f => f.Date.Soon(30))
                    .RuleFor(a => a.HoraAgendamento, f => f.Date.Soon(0).SetHours(f.Random.Int(8, 17)).SetMinutes(0).SetSeconds(0))
                    .RuleFor(a => a.Observacoes, f => f.Lorem.Sentence(5))
                    .RuleFor(a => a.CreatedAt, f => f.Date.Past(1));

                var agendamentos = agendamentoFaker.Generate(100);
                context.Agendamentos.AddRange(agendamentos);
                await context.SaveChangesAsync();
            }

            // Seed Serviços
            if (!context.Servico.Any() && context.Agendamentos.Any() && context.ItensEstoque.Any() && context.Valores.Any() && context.Veterinarios.Any())
            {
                var agendamentosExistentes = await context.Agendamentos.ToListAsync();
                var itensEstoqueExistentes = await context.ItensEstoque.ToListAsync();
                var valoresExistentes = await context.Valores.ToListAsync();
                var veterinariosExistentes = await context.Veterinarios.ToListAsync();

                var servicoFaker = new Faker<Servico>("pt_BR")
                    .RuleFor(s => s.TipoVenda, f => f.PickRandom("Consulta", "Procedimento", "Produto"))
                    .RuleFor(s => s.NomeServico, f => f.Commerce.ProductName())
                    .RuleFor(s => s.IdVeterinario, f => f.PickRandom(veterinariosExistentes).IdVeterinario)
                    .RuleFor(s => s.Data, f => f.Date.Past(30))
                    .RuleFor(s => s.Hora, f => f.Date.Past(0).SetHours(f.Random.Int(8, 17)).SetMinutes(0).SetSeconds(0))
                    .RuleFor(s => s.Status, f => f.Random.Int(1, 4))
                    .RuleFor(s => s.PrecoVenda, f => (decimal)f.Finance.Amount(50, 300))
                    .RuleFor(s => s.Descricao, f => f.Lorem.Sentence(5))
                    .RuleFor(s => s.IdAgendamento, f => f.PickRandom(agendamentosExistentes).IdAgendamento)
                    .RuleFor(s => s.IdProduto, f => f.PickRandom(itensEstoqueExistentes).IdProduto)
                    .RuleFor(s => s.IdValor, f => f.PickRandom(valoresExistentes).IdValor)
                    .RuleFor(s => s.CreatedAt, f => f.Date.Past(1));

                var servicos = servicoFaker.Generate(50);
                context.Servico.AddRange(servicos);
                await context.SaveChangesAsync();
            }

            // Seed AgendaVeterinarios
            if (!context.AgendaVeterinarios.Any() && context.Veterinarios.Any() && context.Pacientes.Any())
            {
                var veterinariosExistentes = await context.Veterinarios.ToListAsync();
                var pacientesExistentes = await context.Pacientes.ToListAsync();

                var agendaVetFaker = new Faker<AgendaVeterinario>("pt_BR")
                    .RuleFor(av => av.IdVeterinario, f => f.PickRandom(veterinariosExistentes).IdVeterinario)
                    .RuleFor(av => av.DataInicio, f => f.Date.Soon(30).SetHours(f.Random.Int(8, 12)).SetMinutes(0).SetSeconds(0))
                    .RuleFor(av => av.DataFim, (f, av) => av.DataInicio.AddHours(f.Random.Int(1, 4)))
                    .RuleFor(av => av.IdPaciente, f => f.PickRandom(pacientesExistentes).IdPaciente)
                    .RuleFor(av => av.CreatedAt, f => f.Date.Past(1));

                var agendas = agendaVetFaker.Generate(30);
                context.AgendaVeterinarios.AddRange(agendas);
                await context.SaveChangesAsync();
            }

            // Seed Prontuários
            if (!context.Prontuarios.Any() && context.Agendamentos.Any() && context.Pacientes.Any() && context.Veterinarios.Any())
            {
                var agendamentosExistentes = await context.Agendamentos.ToListAsync();
                var pacientesExistentes = await context.Pacientes.ToListAsync();
                var veterinariosExistentes = await context.Veterinarios.ToListAsync();

                var prontuarioFaker = new Faker<Prontuario>("pt_BR")
                    .RuleFor(pr => pr.IdAgendamento, f => f.PickRandom(agendamentosExistentes).IdAgendamento)
                    .RuleFor(pr => pr.IdVeterinario, f => f.PickRandom(veterinariosExistentes).IdVeterinario)
                    .RuleFor(pr => pr.DataConsulta, (f, pr) => f.Date.Past(30, pr.Agendamento?.DataAgendamento ?? DateTime.Now)) // Consulta no passado, talvez baseada no agendamento
                    .RuleFor(pr => pr.MotivoConsulta, f => f.Lorem.Sentence(3))
                    .RuleFor(pr => pr.Diagnostico, f => f.Lorem.Sentence(8))
                    .RuleFor(pr => pr.Tratamento, f => f.Lorem.Sentence(10))
                    .RuleFor(pr => pr.Peso, f => (float)f.Random.Double(1, 40))
                    .RuleFor(pr => pr.Temperatura, f => f.Random.Int(37, 40))
                    .RuleFor(pr => pr.FrequenciaCardiaca, f => f.Random.Int(60, 120))
                    .RuleFor(pr => pr.FrequenciaRespiratoria, f => f.Random.Int(15, 30))
                    .RuleFor(pr => pr.Observacoes, f => f.Lorem.Sentence(7))
                    .RuleFor(pr => pr.IdPaciente, (f, pr) => pacientesExistentes.FirstOrDefault(p => p.IdPaciente == pr.Agendamento?.IdPaciente)?.IdPaciente ?? f.PickRandom(pacientesExistentes).IdPaciente)
                    .RuleFor(pr => pr.CreatedAt, f => f.Date.Past(1));

                var prontuarios = prontuarioFaker.Generate(80);
                context.Prontuarios.AddRange(prontuarios);
                await context.SaveChangesAsync();
            }
        }
    }
}