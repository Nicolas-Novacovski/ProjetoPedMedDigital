using System;
using System.Collections.Generic;
using System.Diagnostics; // Adicionado para Activity
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore; // Adicionado
using ProjetoPetMedDigital.Data;      // Adicionado
using ProjetoPetMedDigital.Models;   // Adicionado

namespace ProjetoPetMedDigital.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PetMedContext _context; // Declaração do DbContext

        // O construtor agora recebe ILogger e PetMedContext por injeção de dependência
        public HomeController(ILogger<HomeController> logger, PetMedContext context)
        {
            _logger = logger;
            _context = context; // Atribui o DbContext
        }

        // GET: Exibe a página de Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Processa o formulário de Login
        [HttpPost]
        [ValidateAntiForgeryToken] // Boa prática para segurança em formulários POST
        public async Task<IActionResult> Login(string usuario, string senha) // Parâmetros do formulário
        {
            // Verifica as credenciais no banco de dados 'Usuarios'
            var user = await _context.Usuarios
                                     .FirstOrDefaultAsync(u => u.Login == usuario && u.Senha == senha);

            if (user != null)
            {
                // **AQUI: Implementação de Autenticação Real**
                // Para um projeto real, você usaria ASP.NET Core Identity ou autenticação baseada em cookies/JWT.
                // Exemplo simplificado de redirecionamento para mostrar que a conexão funcionou:
                return RedirectToAction("Index", "Home"); // Redireciona para a Home se o login for bem-sucedido
            }
            else
            {
                // Se as credenciais estiverem incorretas
                // Usamos TempData para passar uma mensagem de erro para a View
                TempData["ErrorMessage"] = "Usuário ou senha incorretos!";
                return View(); // Retorna para a View de Login para exibir a mensagem
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}