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
    public class VacinasController : Controller
    {
        private readonly PetMedContext _context;

        public VacinasController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Vacinas
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Vacinas.Include(v => v.ItemEstoque).Include(v => v.Paciente);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Vacinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacina = await _context.Vacinas
                .Include(v => v.ItemEstoque)
                .Include(v => v.Paciente)
                .FirstOrDefaultAsync(m => m.IdVacina == id);
            if (vacina == null)
            {
                return NotFound();
            }

            return View(vacina);
        }

        // GET: Vacinas/Create
        public IActionResult Create()
        {
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente");
            return View();
        }

        // POST: Vacinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVacina,NomeVacina,Descricao,Duracao,IdProduto,IdPaciente,Id,CreatedAt")] Vacina vacina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto", vacina.IdProduto);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", vacina.IdPaciente);
            return View(vacina);
        }

        // GET: Vacinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacina = await _context.Vacinas.FindAsync(id);
            if (vacina == null)
            {
                return NotFound();
            }
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto", vacina.IdProduto);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", vacina.IdPaciente);
            return View(vacina);
        }

        // POST: Vacinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVacina,NomeVacina,Descricao,Duracao,IdProduto,IdPaciente,Id,CreatedAt")] Vacina vacina)
        {
            if (id != vacina.IdVacina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacinaExists(vacina.IdVacina))
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
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto", vacina.IdProduto);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", vacina.IdPaciente);
            return View(vacina);
        }

        // GET: Vacinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacina = await _context.Vacinas
                .Include(v => v.ItemEstoque)
                .Include(v => v.Paciente)
                .FirstOrDefaultAsync(m => m.IdVacina == id);
            if (vacina == null)
            {
                return NotFound();
            }

            return View(vacina);
        }

        // POST: Vacinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacina = await _context.Vacinas.FindAsync(id);
            if (vacina != null)
            {
                _context.Vacinas.Remove(vacina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacinaExists(int id)
        {
            return _context.Vacinas.Any(e => e.IdVacina == id);
        }
    }
}
