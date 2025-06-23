using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoPetMedDigital.Models;
using System.Diagnostics;

namespace ProjetoPetMedDigital.Controllers
{
    [Authorize] // Garante que apenas usuários logados acessem qualquer parte deste controller
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            // Esta ação agora simplesmente retorna a View Index.cshtml (nosso novo dashboard).
            // Toda a lógica de qual botão mostrar para cada perfil já está no próprio dashboard.
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
