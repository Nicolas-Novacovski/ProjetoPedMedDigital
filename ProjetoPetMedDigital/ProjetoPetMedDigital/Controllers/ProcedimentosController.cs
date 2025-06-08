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
    [Authorize(Roles = "Administrador,Veterinario")] // Apenas Admin e Veterinario podem gerenciar procedimentos
    public class ProcedimentosController : Controller
    {
        private readonly PetMedContext _context;

        public ProcedimentosController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Procedimentos
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Procedimentos.Include(p => p.ItemEstoque);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Procedimentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimento = await _context.Procedimentos
                .Include(p => p.ItemEstoque)
                .FirstOrDefaultAsync(m => m.IdProcedimento == id);
            if (procedimento == null)
            {
                return NotFound();
            }

            return View(procedimento);
        }

        // GET: Procedimentos/Create
        public IActionResult Create()
        {
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto");
            return View();
        }

        // POST: Procedimentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProcedimento,NomeProcedimento,Descricao,Valor,IdProduto,Id,CreatedAt")] Procedimento procedimento)
        {
            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Exemplo: Verificar se o ItemEstoque associado não está vencido (se relevante para o procedimento)
                var itemEstoqueAssociado = await _context.ItensEstoque.FirstOrDefaultAsync(i => i.IdProduto == procedimento.IdProduto);
                if (itemEstoqueAssociado != null && itemEstoqueAssociado.DataValidade.HasValue && itemEstoqueAssociado.DataValidade.Value.Date < DateTime.Today)
                {
                    ModelState.AddModelError("IdProduto", "O produto associado a este procedimento está vencido.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", procedimento.IdProduto);
                    return View(procedimento);
                }

                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Add(procedimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", procedimento.IdProduto);
            return View(procedimento);
        }

        // GET: Procedimentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimento = await _context.Procedimentos.FindAsync(id);
            if (procedimento == null)
            {
                return NotFound();
            }
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", procedimento.IdProduto);
            return View(procedimento);
        }

        // POST: Procedimentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProcedimento,NomeProcedimento,Descricao,Valor,IdProduto,Id,CreatedAt")] Procedimento procedimento)
        {
            if (id != procedimento.IdProcedimento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Exemplo: Verificar se o ItemEstoque associado não está vencido (igual ao Create)
                var itemEstoqueAssociado = await _context.ItensEstoque.FirstOrDefaultAsync(i => i.IdProduto == procedimento.IdProduto);
                if (itemEstoqueAssociado != null && itemEstoqueAssociado.DataValidade.HasValue && itemEstoqueAssociado.DataValidade.Value.Date < DateTime.Today)
                {
                    ModelState.AddModelError("DataValidade", "A data de validade não pode ser no passado.");
                }

                // Validação: Preço de venda não pode ser menor que preço de custo
                if (itemEstoqueAssociado != null && itemEstoqueAssociado.PrecoVenda.HasValue && itemEstoqueAssociado.PrecoCusto.HasValue &&
                    itemEstoqueAssociado.PrecoVenda.Value < itemEstoqueAssociado.PrecoCusto.Value)
                {
                    ModelState.AddModelError("PrecoVenda", "O preço de venda não pode ser menor que o preço de custo do produto associado.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", procedimento.IdProduto);
                    return View(procedimento);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

                try
                {
                    _context.Update(procedimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimentoExists(procedimento.IdProcedimento))
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
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", procedimento.IdProduto);
            return View(procedimento);
        }

        // GET: Procedimentos/Delete/5 (Apenas Administrador)
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimento = await _context.Procedimentos
                .Include(p => p.ItemEstoque)
                .FirstOrDefaultAsync(m => m.IdProcedimento == id);
            if (procedimento == null)
            {
                return NotFound();
            }

            return View(procedimento);
        }

        // POST: Procedimentos/Delete/5 (Apenas Administrador)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procedimento = await _context.Procedimentos.FindAsync(id);
            if (procedimento != null)
            {
                _context.Procedimentos.Remove(procedimento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimentoExists(int id)
        {
            return _context.Procedimentos.Any(e => e.IdProcedimento == id);
        }
    }
}