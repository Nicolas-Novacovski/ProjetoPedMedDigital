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

        // GET: Valores
        public async Task<IActionResult> Index()
        {
            // Adicionado Include para carregar as propriedades de navegação
            var petMedContext = _context.Valores.Include(v => v.Cliente).Include(v => v.Servico);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Valores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valor = await _context.Valores
                .Include(v => v.Cliente)
                .Include(v => v.Servico)
                .FirstOrDefaultAsync(m => m.IdValor == id);
            if (valor == null)
            {
                return NotFound();
            }

            return View(valor);
        }

        // GET: Valores/Create
        public IActionResult Create()
        {
            // Ajustado SelectList para exibir o NomeResponsavel do Cliente e NomeServico
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel");
            ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico"); // Assuming Servico.NomeServico is the display property
            return View();
        }

        // POST: Valores/Create
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
            // Ajustado SelectList para exibir o NomeResponsavel do Cliente e NomeServico
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", valor.IdCliente);
            ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico", valor.Servico?.IdServico); // Use valor.Servico?.IdServico se a FK para Servico for IdServico em Valor
            return View(valor);
        }

        // GET: Valores/Edit/5
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
            // Ajustado SelectList para exibir o NomeResponsavel do Cliente e NomeServico
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", valor.IdCliente);
            ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico", valor.Servico?.IdServico); // Use valor.Servico?.IdServico
            return View(valor);
        }

        // POST: Valores/Edit/5
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
            // Ajustado SelectList para exibir o NomeResponsavel do Cliente e NomeServico
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", valor.IdCliente);
            ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico", valor.Servico?.IdServico); // Use valor.Servico?.IdServico
            return View(valor);
        }

        // GET: Valores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valor = await _context.Valores
                .Include(v => v.Cliente)
                .Include(v => v.Servico)
                .FirstOrDefaultAsync(m => m.IdValor == id);
            if (valor == null)
            {
                return NotFound();
            }

            return View(valor);
        }

        // POST: Valores/Delete/5
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