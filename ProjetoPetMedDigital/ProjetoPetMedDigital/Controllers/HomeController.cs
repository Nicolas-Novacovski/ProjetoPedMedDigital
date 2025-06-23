using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoPetMedDigital.Models;
using System.Diagnostics;

namespace ProjetoPetMedDigital.Controllers
{
    [Authorize] // Garante que apenas usu�rios logados acessem qualquer parte deste controller
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            // Esta a��o agora simplesmente retorna a View Index.cshtml (nosso novo dashboard).
            // Toda a l�gica de qual bot�o mostrar para cada perfil j� est� no pr�prio dashboard.
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
