using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // NECESSÁRIO
using Microsoft.AspNetCore.Authorization; // NECESSÁRIO

using ProjetoPetMedDigital.Models;

namespace ProjetoPetMedDigital.Controllers
{
    [Authorize(Roles = "Administrador")] // Apenas Administradores podem gerenciar usuários
    public class UsuariosController : Controller
    {
        private readonly PetMedContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsuariosController(PetMedContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Usuarios (Lista todos os IdentityUsers)
        public async Task<IActionResult> Index()
        {
            var usuariosComColaboradores = await _context.Users
                .GroupJoin(_context.CadastroColaboradores,
                    user => user.Id,
                    colaborador => colaborador.IdentityUserId,
                    (user, colaboradores) => new { User = user, Colaborador = colaboradores.FirstOrDefault() })
                .ToListAsync();

            return View(usuariosComColaboradores);
        }

        // GET: Usuarios/Details/id (Detalhes de um IdentityUser)
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser == null)
            {
                return NotFound();
            }

            var colaboradorAssociado = await _context.CadastroColaboradores.FirstOrDefaultAsync(cc => cc.IdentityUserId == id);
            ViewBag.ColaboradorAssociado = colaboradorAssociado;

            return View(identityUser);
        }

        // Redireciona para as páginas Identity para Create/Edit
        public IActionResult Create() { return Redirect("/Identity/Account/Register"); }
        public async Task<IActionResult> Edit(string id) { return Redirect("/Identity/Account/Manage"); }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }
            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser == null) { return NotFound(); }

            var colaboradorAssociado = await _context.CadastroColaboradores.FirstOrDefaultAsync(cc => cc.IdentityUserId == id);
            ViewBag.ColaboradorAssociado = colaboradorAssociado;

            return View(identityUser);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser != null)
            {
                var result = await _userManager.DeleteAsync(identityUser);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Erro ao excluir usuário Identity.");
                    return View(identityUser);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}