﻿using System;
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
    public class ProcedimentoesController : Controller
    {
        private readonly PetMedContext _context;

        public ProcedimentoesController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Procedimentoes
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Procedimentos.Include(p => p.ItemEstoque);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Procedimentoes/Details/5
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

        // GET: Procedimentoes/Create
        public IActionResult Create()
        {
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto");
            return View();
        }

        // POST: Procedimentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProcedimento,NomeProcedimento,Descricao,Valor,IdProduto,Id,CreatedAt")] Procedimento procedimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procedimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto", procedimento.IdProduto);
            return View(procedimento);
        }

        // GET: Procedimentoes/Edit/5
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
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto", procedimento.IdProduto);
            return View(procedimento);
        }

        // POST: Procedimentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto", procedimento.IdProduto);
            return View(procedimento);
        }

        // GET: Procedimentoes/Delete/5
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

        // POST: Procedimentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
