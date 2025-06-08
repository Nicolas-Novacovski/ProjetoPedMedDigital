using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using ProjetoPetMedDigital.Models;
using Microsoft.AspNetCore.Authorization; // NECESSÁRIO

namespace ProjetoPetMedDigital.Controllers
{
    [Authorize(Roles = "Administrador,Secretaria")] // Apenas Admin e Secretaria podem gerenciar valores
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel");
            ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico");
            return View();
        }

        // POST: Valores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdValor,ValorProcedimento,TipoPagamento,ValorReceita,ValorSaida,Salario,CompraProdutos,IdCliente,Id,CreatedAt")] Valor valor)
        {
            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Validação: Valores monetários devem ser positivos (além do Range na Model, que já faz isso)
                if (valor.ValorProcedimento < 0 || valor.ValorReceita < 0 || valor.ValorSaida < 0 || valor.Salario < 0 || valor.CompraProdutos < 0)
                {
                    ModelState.AddModelError("", "Nenhum valor monetário pode ser negativo."); // Erro genérico para o modelo
                }

                // Exemplo: Tipo de Pagamento deve ser um valor específico
                if (valor.TipoPagamento != "Dinheiro" && valor.TipoPagamento != "Cartao" && valor.TipoPagamento != "Pix")
                {
                    ModelState.AddModelError("TipoPagamento", "Tipo de pagamento inválido. Use 'Dinheiro', 'Cartao' ou 'Pix'.");
                }


                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", valor.IdCliente);
                    ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico", valor.Servico?.IdServico);
                    return View(valor);
                }

                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Add(valor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", valor.IdCliente);
            ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico", valor.Servico?.IdServico);
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", valor.IdCliente);
            ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico", valor.Servico?.IdServico);
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
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Validação: Valores monetários devem ser positivos (igual ao Create)
                if (valor.ValorProcedimento < 0 || valor.ValorReceita < 0 || valor.ValorSaida < 0 || valor.Salario < 0 || valor.CompraProdutos < 0)
                {
                    ModelState.AddModelError("", "Nenhum valor monetário pode ser negativo.");
                }

                // Exemplo: Tipo de Pagamento deve ser um valor específico (igual ao Create)
                if (valor.TipoPagamento != "Dinheiro" && valor.TipoPagamento != "Cartao" && valor.TipoPagamento != "Pix")
                {
                    ModelState.AddModelError("TipoPagamento", "Tipo de pagamento inválido. Use 'Dinheiro', 'Cartao' ou 'Pix'.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", valor.IdCliente);
                    ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico", valor.Servico?.IdServico);
                    return View(valor);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", valor.IdCliente);
            ViewData["IdServico"] = new SelectList(_context.Servico, "IdServico", "NomeServico", valor.Servico?.IdServico);
            return View(valor);
        }

        // GET: Valores/Delete/5 (Apenas Administrador)
        [Authorize(Roles = "Administrador")]
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

        // POST: Valores/Delete/5 (Apenas Administrador)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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