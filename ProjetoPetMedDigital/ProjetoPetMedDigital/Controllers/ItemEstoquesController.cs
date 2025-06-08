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
    [Authorize(Roles = "Administrador,Secretaria")] // Apenas Admin e Secretaria podem gerenciar estoque
    public class ItemEstoquesController : Controller
    {
        private readonly PetMedContext _context;

        public ItemEstoquesController(PetMedContext context)
        {
            _context = context;
        }

        // GET: ItemEstoques
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.ItensEstoque.Include(i => i.Vacina).Include(i => i.Procedimento).Include(i => i.Servico);
            return View(await petMedContext.ToListAsync());
        }

        // GET: ItemEstoques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemEstoque = await _context.ItensEstoque
                .Include(i => i.Vacina)
                .Include(i => i.Procedimento)
                .Include(i => i.Servico)
                .FirstOrDefaultAsync(m => m.IdProduto == id);
            if (itemEstoque == null)
            {
                return NotFound();
            }

            return View(itemEstoque);
        }

        // GET: ItemEstoques/Create
        public IActionResult Create()
        {
            // Se precisar de dropdowns, adicione aqui (ex: se Vacina, Procedimento ou Servico tivessem dropdowns)
            return View();
        }

        // POST: ItemEstoques/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduto,NomeProduto,Descricao,Quantidade,PrecoCusto,PrecoVenda,UnidadeMedida,DataValidade,Fornecedor,TransacaoDesejada,Id,CreatedAt")] ItemEstoque itemEstoque)
        {
            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Validação: Data de validade não pode ser no passado (se fornecida)
                if (itemEstoque.DataValidade.HasValue && itemEstoque.DataValidade.Value.Date < DateTime.Today)
                {
                    ModelState.AddModelError("DataValidade", "A data de validade não pode ser no passado.");
                }

                // Validação: Preço de venda não pode ser menor que preço de custo
                if (itemEstoque.PrecoVenda.HasValue && itemEstoque.PrecoCusto.HasValue &&
                    itemEstoque.PrecoVenda.Value < itemEstoque.PrecoCusto.Value)
                {
                    ModelState.AddModelError("PrecoVenda", "O preço de venda não pode ser menor que o preço de custo.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    // Re-popule os ViewDatas para dropdowns aqui, se for o caso
                    return View(itemEstoque);
                }

                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Add(itemEstoque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemEstoque);
        }

        // GET: ItemEstoques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemEstoque = await _context.ItensEstoque.FindAsync(id);
            if (itemEstoque == null)
            {
                return NotFound();
            }
            return View(itemEstoque);
        }

        // POST: ItemEstoques/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduto,NomeProduto,Descricao,Quantidade,PrecoCusto,PrecoVenda,UnidadeMedida,DataValidade,Fornecedor,TransacaoDesejada,Id,CreatedAt")] ItemEstoque itemEstoque)
        {
            if (id != itemEstoque.IdProduto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Validação: Data de validade não pode ser no passado (se fornecida)
                if (itemEstoque.DataValidade.HasValue && itemEstoque.DataValidade.Value.Date < DateTime.Today)
                {
                    ModelState.AddModelError("DataValidade", "A data de validade não pode ser no passado.");
                }

                // Validação: Preço de venda não pode ser menor que preço de custo
                if (itemEstoque.PrecoVenda.HasValue && itemEstoque.PrecoCusto.HasValue &&
                    itemEstoque.PrecoVenda.Value < itemEstoque.PrecoCusto.Value)
                {
                    ModelState.AddModelError("PrecoVenda", "O preço de venda não pode ser menor que o preço de custo.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    return View(itemEstoque);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

                try
                {
                    _context.Update(itemEstoque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemEstoqueExists(itemEstoque.IdProduto))
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
            return View(itemEstoque);
        }

        // GET: ItemEstoques/Delete/5 (Apenas Administrador)
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemEstoque = await _context.ItensEstoque
                .Include(i => i.Vacina)
                .Include(i => i.Procedimento)
                .Include(i => i.Servico)
                .FirstOrDefaultAsync(m => m.IdProduto == id);
            if (itemEstoque == null)
            {
                return NotFound();
            }

            return View(itemEstoque);
        }

        // POST: ItemEstoques/Delete/5 (Apenas Administrador)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemEstoque = await _context.ItensEstoque.FindAsync(id);
            if (itemEstoque != null)
            {
                _context.ItensEstoque.Remove(itemEstoque);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemEstoqueExists(int id)
        {
            return _context.ItensEstoque.Any(e => e.IdProduto == id);
        }
    }
}