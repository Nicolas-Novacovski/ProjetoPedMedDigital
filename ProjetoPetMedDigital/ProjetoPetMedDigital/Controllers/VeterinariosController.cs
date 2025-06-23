using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ProjetoPetMedDigital.Models;

namespace ProjetoPetMedDigital.Controllers
{
    [Authorize] // Garante que apenas usuários logados acessem
    public class VeterinariosController : Controller
    {
        private readonly PetMedContext _context;

        public VeterinariosController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Veterinarios
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Veterinarios.Include(v => v.CadastroColaborador);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Veterinarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinarios
                .Include(v => v.CadastroColaborador)
                .FirstOrDefaultAsync(m => m.IdVeterinario == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // GET: Veterinarios/Create
        [Authorize(Roles = "Administrador")] // Apenas Admin pode criar
        public IActionResult Create()
        {
            // CORREÇÃO APLICADA AQUI: O nome do campo de ID foi corrigido.
            ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome");
            return View();
        }

        // POST: Veterinarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("IdVeterinario,NomeVeterinario,Especialidade,Telefone,Email,CRMV,IdColaborador")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veterinario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Se o modelo for inválido, recarrega a lista para a view
            ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome", veterinario.IdColaborador);
            return View(veterinario);
        }

        // GET: Veterinarios/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome", veterinario.IdColaborador);
            return View(veterinario);
        }

        // POST: Veterinarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("IdVeterinario,NomeVeterinario,Especialidade,Telefone,Email,CRMV,IdColaborador,Id,CreatedAt")] Veterinario veterinario)
        {
            if (id != veterinario.IdVeterinario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarioExists(veterinario.IdVeterinario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome", veterinario.IdColaborador);
            return View(veterinario);
        }

        // GET: Veterinarios/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinarios
                .Include(v => v.CadastroColaborador)
                .FirstOrDefaultAsync(m => m.IdVeterinario == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario != null)
            {
                _context.Veterinarios.Remove(veterinario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarioExists(int id)
        {
            return _context.Veterinarios.Any(e => e.IdVeterinario == id);
        }
    }
}
