using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Data;
using ProjetoPetMedDigital.Models; // Certifique-se que este using está correto

namespace ProjetoPetMedDigital.Controllers
{
    public class CadastroColaboradoresController : Controller
    {
        private readonly PetMedContext _context;

        public CadastroColaboradoresController(PetMedContext context)
        {
            _context = context;
        }

        // GET: CadastroColaboradores
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.CadastroColaboradores.Include(c => c.Usuario).Include(c => c.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: CadastroColaboradores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroColaborador = await _context.CadastroColaboradores
                .Include(c => c.Usuario)
                .Include(c => c.Veterinario)
                .FirstOrDefaultAsync(m => m.IdColaborador == id);
            if (cadastroColaborador == null)
            {
                return NotFound();
            }

            return View(cadastroColaborador);
        }

        // GET: CadastroColaboradores/Create
        public IActionResult Create()
        {
            ViewData["Login"] = new SelectList(_context.Usuarios, "Login", "Login"); // SelectList para o Login do Usuário
            return View();
        }

        // POST: CadastroColaboradores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdColaborador,Nome,Telefone,Email,CPF,RG,Dtnascimento,CEP,Endereco,Bairro,Cidade,Cargo,TipoUsuario,Login,Id,CreatedAt")] CadastroColaborador cadastroColaborador)
        //                                                                                                                  ^ Removido "UsuarioId" do Bind
        {
            if (ModelState.IsValid)
            {
                _context.Add(cadastroColaborador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Login"] = new SelectList(_context.Usuarios, "Login", "Login", cadastroColaborador.Login);
            return View(cadastroColaborador);
        }

        // GET: CadastroColaboradores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroColaborador = await _context.CadastroColaboradores.FindAsync(id);
            if (cadastroColaborador == null)
            {
                return NotFound();
            }
            ViewData["Login"] = new SelectList(_context.Usuarios, "Login", "Login", cadastroColaborador.Login);
            return View(cadastroColaborador);
        }

        // POST: CadastroColaboradores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdColaborador,Nome,Telefone,Email,CPF,RG,Dtnascimento,CEP,Endereco,Bairro,Cidade,Cargo,TipoUsuario,Login,Id,CreatedAt")] CadastroColaborador cadastroColaborador)
        //                                                                                                               ^ Removido "UsuarioId" do Bind
        {
            if (id != cadastroColaborador.IdColaborador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cadastroColaborador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CadastroColaboradorExists(cadastroColaborador.IdColaborador))
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
            ViewData["Login"] = new SelectList(_context.Usuarios, "Login", "Login", cadastroColaborador.Login);
            return View(cadastroColaborador);
        }

        // GET: CadastroColaboradores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroColaborador = await _context.CadastroColaboradores
                .Include(c => c.Usuario)
                .Include(c => c.Veterinario)
                .FirstOrDefaultAsync(m => m.IdColaborador == id);
            if (cadastroColaborador == null)
            {
                return NotFound();
            }

            return View(cadastroColaborador);
        }

        // POST: CadastroColaboradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cadastroColaborador = await _context.CadastroColaboradores.FindAsync(id);
            if (cadastroColaborador != null)
            {
                _context.CadastroColaboradores.Remove(cadastroColaborador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CadastroColaboradorExists(int id)
        {
            return _context.CadastroColaboradores.Any(e => e.IdColaborador == id);
        }
    }
}