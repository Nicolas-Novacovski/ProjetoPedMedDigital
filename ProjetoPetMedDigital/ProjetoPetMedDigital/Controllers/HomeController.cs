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
        private readonly PetMedContext _context; // Declara��o do DbContext

        // O construtor agora recebe ILogger e PetMedContext por inje��o de depend�ncia
        public HomeController(ILogger<HomeController> logger, PetMedContext context)
        {
            _logger = logger;
            _context = context; // Atribui o DbContext
        }

        // GET: Exibe a p�gina de Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Processa o formul�rio de Login
        [HttpPost]
        [ValidateAntiForgeryToken] // Boa pr�tica para seguran�a em formul�rios POST
        public async Task<IActionResult> Login(string usuario, string senha) // Par�metros do formul�rio
        {
            // Verifica as credenciais no banco de dados 'Usuarios'
            var user = await _context.Usuarios
                                     .FirstOrDefaultAsync(u => u.Login == usuario && u.Senha == senha);

            if (user != null)
            {
                // **AQUI: Implementa��o de Autentica��o Real**
                // Para um projeto real, voc� usaria ASP.NET Core Identity ou autentica��o baseada em cookies/JWT.
                // Exemplo simplificado de redirecionamento para mostrar que a conex�o funcionou:
                return RedirectToAction("Index", "Home"); // Redireciona para a Home se o login for bem-sucedido
            }
            else
            {
                // Se as credenciais estiverem incorretas
                // Usamos TempData para passar uma mensagem de erro para a View
                TempData["ErrorMessage"] = "Usu�rio ou senha incorretos!";
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