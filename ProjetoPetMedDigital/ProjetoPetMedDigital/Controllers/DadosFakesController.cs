using Microsoft.AspNetCore.Mvc;
using ProjetoPetMedDigital.Models;
using Bogus;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic; // Necessário para List
using Microsoft.AspNetCore.Identity; // Necessário para IdentityUser
using Microsoft.EntityFrameworkCore; // Necessário para ToListAsync()

namespace ProjetoPetMedDigital.Controllers
{
    public class DadosFakesController : Controller
    {
        private readonly PetMedContext _context;
        private readonly UserManager<IdentityUser> _userManager; // Para gerenciar usuários do Identity

        public DadosFakesController(PetMedContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /DadosFakes/Index
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GerarDados(int quantidadeClientes = 100, int pacientesPorCliente = 3,
                                                    int quantidadeVeterinarios = 10, int quantidadeServicos = 20,
                                                    int quantidadeProcedimentos = 30,
                                                    int quantidadeItensEstoque = 150, // Aumentado para garantir IDs suficientes
                                                    int quantidadeColaboradores = 20)
        {
            if (quantidadeClientes <= 0 || pacientesPorCliente <= 0 || quantidadeVeterinarios <= 0 ||
                quantidadeServicos <= 0 || quantidadeProcedimentos <= 0 || quantidadeItensEstoque <= 0 ||
                quantidadeColaboradores <= 0)
            {
                TempData["MensagemErro"] = "Todas as quantidades devem ser maiores que zero.";
                return RedirectToAction(nameof(Index));
            }

            await LimparDadosExistentes();

            var clientesIds = new List<int>();
            var pacientesIds = new List<int>();
            var veterinariosIds = new List<int>();
            var servicosIds = new List<int>();
            var procedimentosIds = new List<int>();
            var itensEstoqueIds = new List<int>();
            var colaboradoresIdentityIds = new List<string>(); // IDs de IdentityUser são strings
            var agendamentosIds = new List<int>(); // IDs de Agendamento
            var valoresIds = new List<int>();

            List<CadastroColaborador> colaboradoresGerados = new List<CadastroColaborador>();
            List<Veterinario> veterinariosGerados = new List<Veterinario>();
            List<Cliente> clientesGerados = new List<Cliente>();
            List<Paciente> pacientesGeradosTotal = new List<Paciente>();
            List<ItemEstoque> itensEstoqueGerados = new List<ItemEstoque>();
            List<Valor> valoresGerados = new List<Valor>();
            List<Agendamento> agendamentosGerados = new List<Agendamento>();
            List<Servico> servicosGerados = new List<Servico>(); // Mantenha como List<Servico>
            List<Procedimento> procedimentosGerados = new List<Procedimento>(); // Inicialize aqui
            List<Vacina> vacinasGeradas = new List<Vacina>(); // Inicialize aqui
            List<Prontuario> prontuariosGerados = new List<Prontuario>();
            List<AgendaVeterinario> agendasGeradas = new List<AgendaVeterinario>();

            var f = new Faker("pt_BR");

            var cadastroColaboradorFaker = new Faker<CadastroColaborador>("pt_BR");
            var clientesFaker = new Faker<Cliente>("pt_BR");
            var pacientesFaker = new Faker<Paciente>("pt_BR");
            var veterinariosFaker = new Faker<Veterinario>("pt_BR");
            var itemEstoqueFaker = new Faker<ItemEstoque>("pt_BR");
            var valorFaker = new Faker<Valor>("pt_BR");
            var agendamentosFaker = new Faker<Agendamento>("pt_BR");
            var servicosFaker = new Faker<Servico>("pt_BR"); // Inicialize aqui
            var procedimentosFaker = new Faker<Procedimento>("pt_BR"); // Inicialize aqui
            var vacinasFaker = new Faker<Vacina>("pt_BR"); // Inicialize aqui
            var prontuariosFaker = new Faker<Prontuario>("pt_BR");
            var agendaVeterinarioFaker = new Faker<AgendaVeterinario>("pt_BR");


            // --- Ordem de Geração Baseada em Dependências ---

            // 1. Gerar CadastroColaborador (e IdentityUser associado)
            cadastroColaboradorFaker // Configurar as regras após a inicialização
                .RuleFor(cc => cc.Nome, fkr => fkr.Name.FullName())
                .RuleFor(cc => cc.Telefone, fkr => fkr.Phone.PhoneNumber("## ! ####-####").Replace("!", fkr.Random.Char('2', '9').ToString()))
                .RuleFor(cc => cc.Email, fkr => fkr.Internet.Email()) // Simplificado para não usar (fkr, cc)
                .RuleFor(cc => cc.CPF, fkr => fkr.Random.Replace("###########")) // Corrigido para CPF (maiúscula)
                .RuleFor(cc => cc.RG, fkr => fkr.Random.Replace("##########")) // Corrigido para RG (maiúscula)
                .RuleFor(cc => cc.Dtnascimento, fkr => fkr.Date.Past(50, DateTime.Now.AddYears(-18))) // Corrigido para Dtnascimento (n minúsculo)
                .RuleFor(cc => cc.CEP, fkr => fkr.Address.ZipCode("########"))
                .RuleFor(cc => cc.Endereco, fkr => fkr.Address.StreetAddress())
                .RuleFor(cc => cc.Bairro, fkr => fkr.Address.County())
                .RuleFor(cc => cc.Cidade, fkr => fkr.Address.City())
                .RuleFor(cc => cc.Cargo, fkr => fkr.PickRandom(new[] { 1, 2, 3, 4 })) // Corrigido para int (ex: 1=Recepcionista, 2=Gerente, 3=Auxiliar, 4=Administrativo)
                .RuleFor(cc => cc.TipoUsuario, fkr => fkr.PickRandom(new[] { 1, 2 })); // Adicionado: 1=Funcionário, 2=Admin
                                                                                       // Propriedade Salario removida, não está no modelo fornecido.

            foreach (var _ in Enumerable.Range(0, quantidadeColaboradores))
            {
                var newColaborador = cadastroColaboradorFaker.Generate();

                // Gerar um IdentityUser e associá-lo
                var identityUser = new IdentityUser { UserName = newColaborador.Email, Email = newColaborador.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(identityUser, "Senha@123"); // Senha padrão para testes
                if (result.Succeeded)
                {
                    newColaborador.IdentityUserId = identityUser.Id;
                    _context.CadastroColaboradores.Add(newColaborador);
                    await _context.SaveChangesAsync();
                    colaboradoresGerados.Add(newColaborador);
                    colaboradoresIdentityIds.Add(newColaborador.IdentityUserId); // Coletar o IdentityUserId (string)
                }
                else
                {
                    // Log error or handle failure
                    Console.WriteLine($"Erro ao criar IdentityUser para {newColaborador.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }


            // 2. Gerar Veterinarios (depende de CadastroColaborador)
            if (colaboradoresGerados.Any())
            {
                veterinariosFaker // Configurar as regras após a inicialização
                    .RuleFor(v => v.NomeVeterinario, fkr => fkr.Name.FullName())
                    .RuleFor(v => v.Especialidade, fkr => fkr.PickRandom(new[] { "Clínico Geral", "Cirurgião", "Dermatologista", "Cardiologista", "Ortopedista" }))
                    .RuleFor(v => v.Telefone, fkr => fkr.Phone.PhoneNumber("## ! ####-####").Replace("!", fkr.Random.Char('2', '9').ToString()))
                    .RuleFor(v => v.Email, (fkr, v) => fkr.Internet.Email(v.NomeVeterinario.Split(' ')[0], v.NomeVeterinario.Split(' ')[1]));

                var veterinariosToAdd = new List<Veterinario>();
                var usedColaboradorLocalIds = new HashSet<int>(); // Para garantir que um colaborador seja associado a apenas um veterinário (IdColaborador é int)

                foreach (var _ in Enumerable.Range(0, quantidadeVeterinarios))
                {
                    var availableColaboradoresLocalIds = colaboradoresGerados
                        .Where(cc => !usedColaboradorLocalIds.Contains(cc.IdColaborador))
                        .Select(cc => cc.IdColaborador)
                        .ToList();

                    if (!availableColaboradoresLocalIds.Any()) break;

                    var selectedColaboradorId = f.PickRandom(availableColaboradoresLocalIds); // Usar a instância f genérica para PickRandom
                    usedColaboradorLocalIds.Add(selectedColaboradorId);

                    var newVeterinario = veterinariosFaker.Generate();
                    newVeterinario.IdColaborador = selectedColaboradorId; // Atribui o IdColaborador
                    veterinariosToAdd.Add(newVeterinario);
                }
                _context.Veterinarios.AddRange(veterinariosToAdd);
                await _context.SaveChangesAsync();
                veterinariosIds.AddRange(veterinariosToAdd.Select(v => v.IdVeterinario));
            }


            // 3. Gerar Clientes
            clientesFaker // Configurar as regras após a inicialização
                .RuleFor(c => c.NomeResponsavel, fkr => fkr.Name.FullName())
                .RuleFor(c => c.Telefone, fkr => fkr.Phone.PhoneNumber("## ! ####-####").Replace("!", fkr.Random.Char('2', '9').ToString()))
                .RuleFor(c => c.Email, (fkr, c) => fkr.Internet.Email(c.NomeResponsavel.Split(' ')[0], c.NomeResponsavel.Split(' ')[1]))
                .RuleFor(c => c.CPF, fkr => fkr.Random.Replace("###########"))
                .RuleFor(c => c.RG, fkr => fkr.Random.Replace("##########"))
                .RuleFor(c => c.DtNascimento, fkr => fkr.Date.Past(50, DateTime.Now.AddYears(-18)))
                .RuleFor(c => c.CEP, fkr => fkr.Address.ZipCode("########"))
                .RuleFor(c => c.Endereco, fkr => fkr.Address.StreetAddress() + ", " + fkr.Address.BuildingNumber())
                .RuleFor(c => c.Bairro, fkr => fkr.Address.County())
                .RuleFor(c => c.Cidade, fkr => fkr.Address.City());

            clientesGerados = clientesFaker.Generate(quantidadeClientes);
            foreach (var cliente in clientesGerados)
            {
                cliente.IdCliente = 0; // Garante que o IdCliente é 0 para inserção auto-incrementada
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                clientesIds.Add(cliente.IdCliente);
            }

            // 4. Gerar Pacientes (depende de Clientes)
            if (clientesIds.Any())
            {
                pacientesFaker // Configurar as regras após a inicialização
                    .RuleFor(p => p.NomeCachorro, fkr => fkr.Name.FirstName())
                    .RuleFor(p => p.Estado, fkr => fkr.PickRandom(new[] { 1, 2, 3 })) // 1: Ativo, 2: Inativo, 3: Falecido (ajuste conforme seu Enum)
                    .RuleFor(p => p.Problema, fkr => fkr.Hacker.Phrase())
                    .RuleFor(p => p.TipoAtendimento, fkr => fkr.PickRandom(new[] { 1, 2, 3 })) // 1: Consulta, 2: Emergência, 3: Retorno
                    .RuleFor(p => p.Peso, fkr => fkr.Random.Float(0.5f, 50.0f))
                    .RuleFor(p => p.SinaisVitais, fkr => fkr.Lorem.Sentence(10))
                    .RuleFor(p => p.Recomendacoes, fkr => fkr.Lorem.Sentence(15));

                foreach (var clienteId in clientesIds)
                {
                    var pacientesParaCliente = pacientesFaker.Clone()
                        .RuleFor(p => p.IdCliente, clienteId)
                        .Generate(pacientesPorCliente);
                    _context.Pacientes.AddRange(pacientesParaCliente);
                    pacientesGeradosTotal.AddRange(pacientesParaCliente); // Adicionar à lista total
                }
                await _context.SaveChangesAsync();
                pacientesIds.AddRange(pacientesGeradosTotal.Select(p => p.IdPaciente));
            }


            // 5. Gerar ItemEstoque
            itemEstoqueFaker // Configurar as regras após a inicialização
                .RuleFor(i => i.NomeProduto, fkr => fkr.Commerce.ProductName())
                .RuleFor(i => i.Descricao, fkr => fkr.Commerce.ProductDescription())
                .RuleFor(i => i.Quantidade, fkr => fkr.Random.Int(1, 100)) // Usando fkr.Random.Int
                .RuleFor(i => i.PrecoCusto, fkr => fkr.Finance.Amount(5, 500, 2))
                .RuleFor(i => i.PrecoVenda, fkr => fkr.Finance.Amount(10, 1000, 2))
                .RuleFor(i => i.UnidadeMedida, fkr => fkr.PickRandom(new[] { "Unidade", "Kg", "Litro", "Caixa" }))
                .RuleFor(i => i.DataValidade, fkr => fkr.Date.Future(2))
                .RuleFor(i => i.Fornecedor, fkr => fkr.Company.CompanyName());
            // TransacaoDesejada (int?) - pode ser null, ou gerar aleatoriamente se tiver um significado.

            itensEstoqueGerados = itemEstoqueFaker.Generate(quantidadeItensEstoque);
            foreach (var item in itensEstoqueGerados)
            {
                item.IdProduto = 0; // Garante que o IdProduto é 0 para inserção auto-incrementada
                _context.ItensEstoque.Add(item);
            }
            await _context.SaveChangesAsync();
            itensEstoqueIds.AddRange(itensEstoqueGerados.Select(i => i.IdProduto)); // ID é IdProduto


            // Crie um pool único e mutável de itens de estoque para consumo exclusivo pelos 1:1 obrigatórios
            var remainingItemEstoqueIdsPool = new List<int>(itensEstoqueIds);


            // 6. Gerar Valores (depende de Clientes)
            if (clientesIds.Any())
            {
                valorFaker // Configurar as regras após a inicialização
                    .RuleFor(v => v.ValorProcedimento, fkr => fkr.Finance.Amount(50, 1000, 2))
                    .RuleFor(v => v.TipoPagamento, fkr => fkr.PickRandom(new[] { "Cartão de Crédito", "Dinheiro", "Pix", "Débito" }))
                    .RuleFor(v => v.ValorReceita, fkr => fkr.Finance.Amount(10, 500, 2))
                    .RuleFor(v => v.ValorSaida, fkr => fkr.Finance.Amount(5, 200, 2))
                    .RuleFor(v => v.Salario, fkr => fkr.Finance.Amount(1500, 5000, 2))
                    .RuleFor(v => v.CompraProdutos, fkr => fkr.Finance.Amount(100, 1000, 2))
                    .RuleFor(v => v.IdCliente, fkr => fkr.PickRandom(clientesIds));

                valoresGerados = valorFaker.Generate(quantidadeClientes * 2); // Ex: 2 valores por cliente
                _context.Valores.AddRange(valoresGerados);
                await _context.SaveChangesAsync();
                valoresIds.AddRange(valoresGerados.Select(v => v.IdValor));
            }

            // 7. Gerar Agendamentos (depende de Pacientes e Veterinarios)
            if (pacientesIds.Any() && veterinariosIds.Any())
            {
                agendamentosFaker // Configurar as regras após a inicialização
                    .RuleFor(a => a.IdPaciente, fkr => fkr.PickRandom(pacientesIds))
                    .RuleFor(a => a.IdVeterinario, fkr => fkr.PickRandom(veterinariosIds))
                    .RuleFor(a => a.DataAgendamento, fkr => fkr.Date.Soon(30).Date) // Apenas a parte da data
                    .RuleFor(a => a.HoraAgendamento, fkr => fkr.Date.Soon(30)) // Apenas a parte da hora será relevante
                    .RuleFor(a => a.Observacoes, fkr => fkr.Lorem.Sentence(5)); // Observacoes

                agendamentosGerados = agendamentosFaker.Generate(quantidadeClientes * pacientesPorCliente);
                _context.Agendamentos.AddRange(agendamentosGerados);
                await _context.SaveChangesAsync();
                agendamentosIds.AddRange(agendamentosGerados.Select(a => a.IdAgendamento));
            }


            // 8. Gerar Serviços (depende de Veterinario, ItemEstoque, Agendamento, Valor)
            // IMPORTANTE: Para o relacionamento 1:1 Servico-Valor (FK em Servico), cada Servico precisa de um IdValor ÚNICO.
            // Para o relacionamento 1:1 Servico-ItemEstoque (FK em Servico), cada Servico precisa de um IdProduto ÚNICO.
            // Consume IDs de ItemEstoque do pool global.
            if (veterinariosIds.Any() && remainingItemEstoqueIdsPool.Any() && agendamentosIds.Any() && valoresIds.Any())
            {
                servicosFaker // Configurar as regras após a inicialização
                    .RuleFor(s => s.TipoVenda, fkr => fkr.PickRandom(new[] { "Serviço", "Produto" }))
                    .RuleFor(s => s.NomeServico, fkr => fkr.Commerce.ProductAdjective() + " " + fkr.Commerce.Product())
                    .RuleFor(s => s.IdVeterinario, fkr => fkr.PickRandom(veterinariosIds))
                    .RuleFor(s => s.Data, fkr => fkr.Date.Soon(30).Date) // Apenas a parte da data
                    .RuleFor(s => s.Hora, fkr => fkr.Date.Soon(30)) // Apenas a parte da hora
                    .RuleFor(s => s.Status, fkr => fkr.PickRandom(new[] { 1, 2, 3 })) // 1: Pendente, 2: Concluído, 3: Cancelado
                    .RuleFor(s => s.PrecoVenda, fkr => fkr.Finance.Amount(20, 500, 2))
                    .RuleFor(s => s.Descricao, fkr => fkr.Lorem.Sentence(8))
                    .RuleFor(s => s.IdAgendamento, fkr => fkr.PickRandom(agendamentosIds));
                // IdValor e IdProduto serão atribuídos no loop para garantir unicidade

                var servicosToProcess = new List<Servico>();
                var availableValorIds = new List<int>(valoresIds); // Copia mutável dos Ids de Valor

                // Limita a quantidade de serviços ao mínimo de IDs disponíveis de Valor e do pool de ItensEstoque
                int servicesToCreate = Math.Min(quantidadeServicos, Math.Min(availableValorIds.Count, remainingItemEstoqueIdsPool.Count));

                for (int i = 0; i < servicesToCreate; i++)
                {
                    var selectedValorId = f.PickRandom(availableValorIds); // Seleciona um IdValor único
                    availableValorIds.Remove(selectedValorId); // Remove o IdValor usado da lista

                    var selectedItemId = f.PickRandom(remainingItemEstoqueIdsPool); // Seleciona um IdProduto único do pool
                    remainingItemEstoqueIdsPool.Remove(selectedItemId); // Remove o IdProduto usado do pool

                    var newServico = servicosFaker.Generate();
                    newServico.IdValor = selectedValorId; // Atribui o IdValor único
                    newServico.IdProduto = selectedItemId; // Atribui o IdProduto único
                    servicosToProcess.Add(newServico);
                }
                servicosGerados.AddRange(servicosToProcess); // Adiciona à lista geral de serviços gerados

                _context.Servico.AddRange(servicosGerados);
                await _context.SaveChangesAsync();
                servicosIds.AddRange(servicosGerados.Select(s => s.IdServico));
            }

            // 9. Gerar Procedimentos (depende de ItemEstoque)
            // IMPORTANTE: Para o relacionamento 1:1 Procedimento-ItemEstoque (FK em Procedimento), cada Procedimento precisa de um IdProduto ÚNICO.
            // Consume IDs de ItemEstoque do pool global.
            if (remainingItemEstoqueIdsPool.Any()) // Certifica que ainda há IDs no pool
            {
                procedimentosFaker // Configurar as regras após a inicialização
                    .RuleFor(p => p.NomeProcedimento, fkr => fkr.Commerce.ProductAdjective() + " " + fkr.Commerce.Product())
                    .RuleFor(p => p.Descricao, fkr => fkr.Lorem.Sentence(10))
                    .RuleFor(p => p.Valor, fkr => fkr.Finance.Amount(10, 300, 2));
                // IdProduto será atribuído no loop para garantir unicidade

                var procedimentosToProcess = new List<Procedimento>();

                // Limita a quantidade de procedimentos ao número de IDs disponíveis de ItemEstoque no pool
                int proceduresToCreate = Math.Min(quantidadeProcedimentos, remainingItemEstoqueIdsPool.Count);

                for (int i = 0; i < proceduresToCreate; i++)
                {
                    var selectedItemId = f.PickRandom(remainingItemEstoqueIdsPool); // Seleciona um IdProduto único do pool
                    remainingItemEstoqueIdsPool.Remove(selectedItemId); // Remove o IdProduto usado do pool

                    var newProcedimento = procedimentosFaker.Generate();
                    newProcedimento.IdProduto = selectedItemId; // Atribui o IdProduto único
                    procedimentosToProcess.Add(newProcedimento);
                }
                procedimentosGerados.AddRange(procedimentosToProcess); // Adiciona à lista geral de procedimentos gerados

                _context.Procedimentos.AddRange(procedimentosGerados);
                await _context.SaveChangesAsync();
                procedimentosIds.AddRange(procedimentosGerados.Select(p => p.IdProcedimento));
            }


            // 10. Gerar Vacinas (depende de Pacientes, ItemEstoque)
            // IMPORTANTE: Para o relacionamento 1:1 Vacina-ItemEstoque (FK em Vacina), cada Vacina precisa de um IdProduto ÚNICO.
            // Consume IDs de ItemEstoque do pool global.
            if (pacientesIds.Any() && remainingItemEstoqueIdsPool.Any()) // Certifica que ainda há IDs no pool
            {
                vacinasFaker // Configurar as regras após a inicialização
                    .RuleFor(v => v.NomeVacina, fkr => fkr.PickRandom(new[] { "Raiva", "V8", "V10", "Gripe Canina", "Leishmaniose" }))
                    .RuleFor(v => v.Descricao, fkr => fkr.Lorem.Sentence(5))
                    .RuleFor(v => v.Duracao, fkr => fkr.Random.Int(1, 3) + " ano(s)")
                    .RuleFor(v => v.IdPaciente, fkr => fkr.PickRandom(pacientesIds));
                // IdProduto será atribuído no loop para garantir unicidade

                var vacinasToProcess = new List<Vacina>();

                // Limita a quantidade de vacinas ao número de IDs disponíveis de ItemEstoque no pool
                int vacinasToCreate = Math.Min(quantidadeClientes * pacientesPorCliente, remainingItemEstoqueIdsPool.Count);

                for (int i = 0; i < vacinasToCreate; i++)
                {
                    var selectedItemId = f.PickRandom(remainingItemEstoqueIdsPool); // Seleciona um IdProduto único do pool
                    remainingItemEstoqueIdsPool.Remove(selectedItemId); // Remove o IdProduto usado do pool

                    var newVacina = vacinasFaker.Generate();
                    newVacina.IdProduto = selectedItemId; // Atribui o IdProduto único
                    vacinasToProcess.Add(newVacina);
                }
                vacinasGeradas.AddRange(vacinasToProcess); // Adiciona à lista geral de vacinas geradas

                _context.Vacinas.AddRange(vacinasGeradas);
                await _context.SaveChangesAsync();
            }


            // 11. Gerar Prontuarios (depende de Pacientes, Veterinarios, Agendamentos)
            if (pacientesIds.Any() && veterinariosIds.Any() && agendamentosIds.Any())
            {
                // Para IdVeterinario e IdAgendamento que são int? (nullable), permita nulls.
                var possibleVeterinarioIds = veterinariosIds.Select(id => (int?)id).ToList();
                possibleVeterinarioIds.Add(null);
                var possibleAgendamentoIds = agendamentosIds.Select(id => (int?)id).ToList();
                possibleAgendamentoIds.Add(null);

                prontuariosFaker // Configurar as regras após a inicialização
                    .RuleFor(p => p.IdPaciente, fkr => fkr.PickRandom(pacientesIds))
                    .RuleFor(p => p.IdVeterinario, fkr => fkr.PickRandom(possibleVeterinarioIds)) // Pode ser nulo
                    .RuleFor(p => p.IdAgendamento, fkr => fkr.PickRandom(possibleAgendamentoIds)) // Pode ser nulo
                    .RuleFor(p => p.DataConsulta, fkr => fkr.Date.Past(1))
                    .RuleFor(p => p.MotivoConsulta, fkr => fkr.Lorem.Sentence(10))
                    .RuleFor(p => p.Diagnostico, fkr => fkr.Lorem.Sentence(10))
                    .RuleFor(p => p.Tratamento, fkr => fkr.Lorem.Sentence(15))
                    .RuleFor(p => p.Peso, fkr => fkr.Random.Float(0.1f, 100.0f))
                    .RuleFor(p => p.Temperatura, fkr => fkr.Random.Int(35, 40))
                    .RuleFor(p => p.FrequenciaCardiaca, fkr => fkr.Random.Int(60, 180))
                    .RuleFor(p => p.FrequenciaRespiratoria, fkr => fkr.Random.Int(10, 40))
                    .RuleFor(p => p.Observacoes, fkr => fkr.Lorem.Paragraph(2));

                prontuariosGerados = prontuariosFaker.Generate(quantidadeClientes * pacientesPorCliente);
                _context.Prontuarios.AddRange(prontuariosGerados);
                await _context.SaveChangesAsync();
            }

            // 12. Gerar AgendaVeterinario (depende de Veterinarios e Pacientes)
            // Lógica de geração de DataInicio e DataFim: DataInicio antes de DataFim
            if (veterinariosIds.Any() && pacientesIds.Any())
            {
                agendaVeterinarioFaker // Configurar as regras após a inicialização
                    .RuleFor(av => av.IdVeterinario, fkr => fkr.PickRandom(veterinariosIds))
                    .RuleFor(av => av.IdPaciente, fkr => fkr.PickRandom(pacientesIds))
                    .RuleFor(av => av.DataInicio, fkr => fkr.Date.Soon(60)) // Gerar DataInicio (DateTime)
                    .RuleFor(av => av.DataFim, (fkr, av) => av.DataInicio.AddHours(fkr.Random.Int(1, 8))); // DataFim é após DataInicio (usando fkr.Random.Int)
                                                                                                           // Propriedade Disponivel foi removida do modelo.

                agendasGeradas = agendaVeterinarioFaker.Generate(quantidadeVeterinarios * 5); // 5 agendas por veterinário
                _context.AgendaVeterinarios.AddRange(agendasGeradas);
                await _context.SaveChangesAsync();
            }

            TempData["MensagemSucesso"] = $"Dados fakes gerados com sucesso: " +
                                           $"{colaboradoresGerados.Count} colaboradores, " +
                                           $"{veterinariosGerados.Count} veterinários, " +
                                           $"{clientesGerados.Count} clientes, " +
                                           $"{pacientesGeradosTotal.Count} pacientes, " +
                                           $"{itensEstoqueGerados.Count} itens de estoque, " +
                                           $"{valoresGerados.Count} valores, " +
                                           $"{agendamentosGerados.Count} agendamentos, " +
                                           $"{servicosGerados.Count} serviços, " +
                                           $"{procedimentosGerados.Count} procedimentos, " +
                                           $"{vacinasGeradas.Count} vacinas, " +
                                           $"{prontuariosGerados.Count} prontuários, e " +
                                           $"{agendasGeradas.Count} agendas de veterinários.";
            return RedirectToAction(nameof(Index));
        }

        private async Task LimparDadosExistentes()
        {
            // Ordem inversa de dependência para evitar erros de chave estrangeira
            _context.AgendaVeterinarios.RemoveRange(await _context.AgendaVeterinarios.ToListAsync());
            _context.Prontuarios.RemoveRange(await _context.Prontuarios.ToListAsync());
            _context.Vacinas.RemoveRange(await _context.Vacinas.ToListAsync());
            _context.Procedimentos.RemoveRange(await _context.Procedimentos.ToListAsync());
            _context.Servico.RemoveRange(await _context.Servico.ToListAsync());
            _context.Agendamentos.RemoveRange(await _context.Agendamentos.ToListAsync());
            _context.Valores.RemoveRange(await _context.Valores.ToListAsync());
            _context.ItensEstoque.RemoveRange(await _context.ItensEstoque.ToListAsync());
            _context.Pacientes.RemoveRange(await _context.Pacientes.ToListAsync());
            _context.Clientes.RemoveRange(await _context.Clientes.ToListAsync());
            _context.Veterinarios.RemoveRange(await _context.Veterinarios.ToListAsync());
            _context.CadastroColaboradores.RemoveRange(await _context.CadastroColaboradores.ToListAsync());

            // Remover IdentityUsers associados aos CadastroColaboradores
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                await _userManager.DeleteAsync(user);
            }

            await _context.SaveChangesAsync();
            TempData["MensagemAlerta"] = "Dados existentes foram limpos.";
        }
    }
}