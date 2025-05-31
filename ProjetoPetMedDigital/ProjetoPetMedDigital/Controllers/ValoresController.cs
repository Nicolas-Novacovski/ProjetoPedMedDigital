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
    public class ValoresController : Controller
    {
        private readonly PetMedContext _context;

        public ValoresController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Valors
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Valores.Include(v => v.Cliente);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Valors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valor = await _context.Valores
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.IdValor == id);
            if (valor == null)
            {
                return NotFound();
            }

            return View(valor);
        }

        // GET: Valors/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            return View();
        }

        // POST: Valors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdValor,ValorProcedimento,TipoPagamento,ValorReceita,ValorSaida,Salario,CompraProdutos,IdCliente,Id,CreatedAt")] Valor valor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", valor.IdCliente);
            return View(valor);
        }

        // GET: Valors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valor = await _context.Valores.FindAsync(id);
            if (valor == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", valor.IdCliente);
            return View(valor);
        }

        // POST: Valors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdValor,ValorProcedimento,TipoPagamento,ValorReceita,ValorSaida,Salario,CompraProdutos,IdCliente,Id,CreatedAt")] Valor valor)
        {
            if (id != valor.IdValor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValorExists(valor.IdValor))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", valor.IdCliente);
            return View(valor);
        }

        // GET: Valors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valor = await _context.Valores
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.IdValor == id);
            if (valor == null)
            {
                return NotFound();
            }

            return View(valor);
        }

        // POST: Valors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valor = await _context.Valores.FindAsync(id);
            if (valor != null)
            {
                _context.Valores.Remove(valor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValorExists(int id)
        {
            return _context.Valores.Any(e => e.IdValor == id);
        }
    }
}
