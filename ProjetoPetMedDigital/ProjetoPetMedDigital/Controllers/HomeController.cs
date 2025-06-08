using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Models;
using Microsoft.AspNetCore.Authorization; // NECESS�RIO para [Authorize] e [AllowAnonymous]

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

        // GET: A��o Login que redireciona para a p�gina de login do Identity
        [HttpGet]
        [AllowAnonymous] // Permite acesso mesmo sem estar logado
        public IActionResult Login()
        {
            return Redirect("/Identity/Account/Login");
        }

        // A��o Index (p�gina inicial gen�rica) - pode ser acessada por todos
        [AllowAnonymous] // Permite acesso mesmo sem estar logado
        public IActionResult Index()
        {
            // Nota: Esta Home/Index pode ser usada como uma p�gina de boas-vindas geral ou
            // ser removida se voc� quiser que o usu�rio v� direto para a Home do perfil.
            return View();
        }

        // A��o Privacy - pode ser acessada por todos
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

        // --- IN�CIO DAS NOVAS A��ES HOME POR PERFIL ---

        [Authorize(Roles = "Administrador")] // Apenas usu�rios com perfil "Administrador"
        public IActionResult AdminHome()
        {
            ViewData["Title"] = "Home - Administrador";
            return View();
        }

        [Authorize(Roles = "Veterinario")] // Apenas usu�rios com perfil "Veterinario"
        public IActionResult VeterinarioHome()
        {
            ViewData["Title"] = "Home - Veterin�rio";
            return View();
        }

        [Authorize(Roles = "Secretaria")] // Apenas usu�rios com perfil "Secretaria"
        public IActionResult SecretariaHome()
        {
            ViewData["Title"] = "Home - Secret�ria";
            return View();
        }

        // --- FIM DAS NOVAS A��ES HOME POR PERFIL ---
    }
}