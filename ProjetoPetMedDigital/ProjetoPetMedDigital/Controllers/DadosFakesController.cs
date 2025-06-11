// ProjetoPetMedDigital/ProjetoPetMedDigital/Controllers/DadosFakesController.cs

using Microsoft.AspNetCore.Mvc;
using ProjetoPetMedDigital.Models;
using Bogus;
using System.Linq;
using System.Threading.Tasks;
using System; // Adicionado para DateTime

namespace ProjetoPetMedDigital.Controllers
{
    public class DadosFakesController : Controller
    {
        private readonly PetMedContext _context;

        public DadosFakesController(PetMedContext context)
        {
            _context = context;
        }

        // GET: /DadosFakes/Index
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GerarDados(int quantidadeClientes = 10, int pacientesPorCliente = 2)
        {
            if (quantidadeClientes <= 0 || pacientesPorCliente <= 0)
            {
                TempData["MensagemErro"] = "A quantidade de clientes e pacientes deve ser maior que zero.";
                return RedirectToAction(nameof(Index));
            }

            // Limpa dados existentes para evitar duplicatas em testes. USE COM MUITA CAUTELA EM AMBIENTES DE PRODUÇÃO!
            // Se você descomentar, todos os clientes e pacientes existentes serão DELETADOS.
            // _context.Pacientes.RemoveRange(_context.Pacientes);
            // _context.Clientes.RemoveRange(_context.Clientes);
            // await _context.SaveChangesAsync();

            var clientesFaker = new Faker<Cliente>("pt_BR")
                .RuleFor(c => c.NomeResponsavel, f => f.Name.FullName()) // Propriedade correta: NomeResponsavel
                .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", "9")) // Formato de telefone com 9 dígitos
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.NomeResponsavel.Split(' ')[0], c.NomeResponsavel.Split(' ')[1])) // Propriedade correta: Email
                .RuleFor(c => c.CPF, f => f.Random.Replace("###########")) // Apenas números para CPF
                .RuleFor(c => c.RG, f => f.Random.Replace("##########")) // Apenas números para RG
                .RuleFor(c => c.DtNascimento, f => f.Date.Past(50, DateTime.Now.AddYears(-18))) // Data de nascimento entre 18 e 50 anos atrás
                .RuleFor(c => c.CEP, f => f.Address.ZipCode("########")) // CEP com 8 dígitos
                .RuleFor(c => c.Endereco, f => f.Address.StreetAddress()) // Endereço
                .RuleFor(c => c.Bairro, f => f.Address.County()) // Bairro
                .RuleFor(c => c.Cidade, f => f.Address.City()); // Cidade

            var clientesGerados = clientesFaker.Generate(quantidadeClientes);

            foreach (var cliente in clientesGerados)
            {
                // Adiciona o cliente ao contexto antes de gerar os pacientes para que o IdCliente seja gerado
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync(); // Salva para ter o Id do Cliente

                var pacientesFaker = new Faker<Paciente>("pt_BR")
                    .RuleFor(p => p.NomeCachorro, f => f.Name.FirstName()) // Propriedade correta: NomeCachorro
                    .RuleFor(p => p.Estado, f => f.PickRandom(new[] { 1, 2 })) // Exemplo: 1 para 'Ativo', 2 para 'Inativo' (ajuste conforme seus enums/lógica)
                    .RuleFor(p => p.Problema, f => f.Lorem.Sentence(5)) // Problema principal
                    .RuleFor(p => p.TipoAtendimento, f => f.PickRandom(new[] { 1, 2 })) // Exemplo: 1 para 'Consulta', 2 para 'Emergência'
                    .RuleFor(p => p.Peso, f => f.Random.Float(0.5f, 50.0f)) // Peso em float
                    .RuleFor(p => p.SinaisVitais, f => f.Lorem.Sentence(10)) // Sinais vitais
                    .RuleFor(p => p.Recomendacoes, f => f.Lorem.Sentence(15)) // Recomendações
                    .RuleFor(p => p.IdCliente, cliente.IdCliente); // Associa o paciente ao IdCliente recém-criado

                var pacientesGerados = pacientesFaker.Generate(pacientesPorCliente);
                _context.Pacientes.AddRange(pacientesGerados);
            }

            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = $"{quantidadeClientes} clientes e {quantidadeClientes * pacientesPorCliente} pacientes foram gerados com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}