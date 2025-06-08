using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Models;
using Microsoft.AspNetCore.Authorization; // NECESSÁRIO para [Authorize] e [AllowAnonymous]

namespace ProjetoPetMedDigital.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PetMedContext _context;

        public HomeController(ILogger<HomeController> logger, PetMedContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Ação Login que redireciona para a página de login do Identity
        [HttpGet]
        [AllowAnonymous] // Permite acesso mesmo sem estar logado
        public IActionResult Login()
        {
            return Redirect("/Identity/Account/Login");
        }

        // Ação Index (página inicial genérica) - pode ser acessada por todos
        [AllowAnonymous] // Permite acesso mesmo sem estar logado
        public IActionResult Index()
        {
            // Nota: Esta Home/Index pode ser usada como uma página de boas-vindas geral ou
            // ser removida se você quiser que o usuário vá direto para a Home do perfil.
            return View();
        }

        // Ação Privacy - pode ser acessada por todos
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // --- INÍCIO DAS NOVAS AÇÕES HOME POR PERFIL ---

        [Authorize(Roles = "Administrador")] // Apenas usuários com perfil "Administrador"
        public IActionResult AdminHome()
        {
            ViewData["Title"] = "Home - Administrador";
            return View();
        }

        [Authorize(Roles = "Veterinario")] // Apenas usuários com perfil "Veterinario"
        public IActionResult VeterinarioHome()
        {
            ViewData["Title"] = "Home - Veterinário";
            return View();
        }

        [Authorize(Roles = "Secretaria")] // Apenas usuários com perfil "Secretaria"
        public IActionResult SecretariaHome()
        {
            ViewData["Title"] = "Home - Secretária";
            return View();
        }

        // --- FIM DAS NOVAS AÇÕES HOME POR PERFIL ---
    }
}