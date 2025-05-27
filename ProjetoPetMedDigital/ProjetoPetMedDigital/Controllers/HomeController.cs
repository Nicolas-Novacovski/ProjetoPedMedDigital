using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoPetMedDigital.Models; // Supondo que ErrorViewModel est� aqui
// Adicione ILogger se ainda n�o estiver globalmente acess�vel ou via DI
// using Microsoft.Extensions.Logging;


namespace ProjetoPetMedDigital.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
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
