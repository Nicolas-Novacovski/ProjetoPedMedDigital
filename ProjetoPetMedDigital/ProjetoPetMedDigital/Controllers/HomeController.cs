using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Models;
using Microsoft.AspNetCore.Authorization; // NECESS�RIO para [Authorize]

namespace ProjetoPetMedDigital.Controllers
{
    // [Authorize] // Se voc� quiser que o usu�rio precise estar logado para ver a Home
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PetMedContext _context;

        public HomeController(ILogger<HomeController> logger, PetMedContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous] // Permite acesso mesmo sem estar logado
        public IActionResult Login()
        {
            return Redirect("/Identity/Account/Login");
        }

        [AllowAnonymous] // Permite acesso mesmo sem estar logado
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous] // Permite acesso mesmo sem estar logado
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous] // Se a p�gina de erro deve ser acess�vel publicamente
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}