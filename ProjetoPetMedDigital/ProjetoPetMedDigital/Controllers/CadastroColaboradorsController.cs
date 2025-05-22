using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Data;
using ProjetoPetMedDigital.Models;

namespace ProjetoPetMedDigital.Controllers
{
    public class CadastroColaboradorsController : Controller
    {
        private readonly PetMedContext _context;

        public CadastroColaboradorsController(PetMedContext context)
        {
            _context = context;
        }

        // GET: CadastroColaboradors
        public async Task<IActionResult> Index()
        {
            return View(await _context.CadastroColaboradores.ToListAsync());
        }

        // GET: CadastroColaboradors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroColaborador = await _context.CadastroColaboradores
                .FirstOrDefaultAsync(m => m.IdColaborador == id);
            if (cadastroColaborador == null)
            {
                return NotFound();
            }

            return View(cadastroColaborador);
        }

        // GET: CadastroColaboradors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CadastroColaboradors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdColaborador,Nome,Telefone,Email,CPF,RG,Dtnascimento,CEP,Endereco,Bairro,Cidade,Cargo,TipoUsuario,UsuarioId,Login,Id,CreatedAt")] CadastroColaborador cadastroColaborador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cadastroColaborador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cadastroColaborador);
        }

        // GET: CadastroColaboradors/Edit/5
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
            return View(cadastroColaborador);
        }

        // POST: CadastroColaboradors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdColaborador,Nome,Telefone,Email,CPF,RG,Dtnascimento,CEP,Endereco,Bairro,Cidade,Cargo,TipoUsuario,UsuarioId,Login,Id,CreatedAt")] CadastroColaborador cadastroColaborador)
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
            return View(cadastroColaborador);
        }

        // GET: CadastroColaboradors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroColaborador = await _context.CadastroColaboradores
                .FirstOrDefaultAsync(m => m.IdColaborador == id);
            if (cadastroColaborador == null)
            {
                return NotFound();
            }

            return View(cadastroColaborador);
        }

        // POST: CadastroColaboradors/Delete/5
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
