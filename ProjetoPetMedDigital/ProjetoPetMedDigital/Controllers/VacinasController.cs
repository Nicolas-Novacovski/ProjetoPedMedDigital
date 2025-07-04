﻿using System;
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
    [Authorize(Roles = "Administrador,Veterinario")] // Apenas Admin e Veterinario podem gerenciar vacinas
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
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro");
            return View();
        }

        // POST: Vacinas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVacina,NomeVacina,Descricao,Duracao,IdProduto,IdPaciente,Id,CreatedAt")] Vacina vacina)
        {
            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Exemplo: Verificar se há estoque suficiente do produto
                var itemEstoque = await _context.ItensEstoque.FirstOrDefaultAsync(i => i.IdProduto == vacina.IdProduto);
                if (itemEstoque == null || (itemEstoque.Quantidade.HasValue && itemEstoque.Quantidade.Value <= 0))
                {
                    ModelState.AddModelError("IdProduto", "Produto sem estoque ou não encontrado.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", vacina.IdProduto);
                    ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", vacina.IdPaciente);
                    return View(vacina);
                }

                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Add(vacina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", vacina.IdProduto);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", vacina.IdPaciente);
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
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", vacina.IdProduto);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", vacina.IdPaciente);
            return View(vacina);
        }

        // POST: Vacinas/Edit/5
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
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Exemplo: Verificar se há estoque suficiente do produto (igual ao Create)
                var itemEstoque = await _context.ItensEstoque.FirstOrDefaultAsync(i => i.IdProduto == vacina.IdProduto);
                if (itemEstoque == null || (itemEstoque.Quantidade.HasValue && itemEstoque.Quantidade.Value <= 0))
                {
                    ModelState.AddModelError("IdProduto", "Produto sem estoque ou não encontrado.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", vacina.IdProduto);
                    ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", vacina.IdPaciente);
                    return View(vacina);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

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
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", vacina.IdProduto);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", vacina.IdPaciente);
            return View(vacina);
        }

        // GET: Vacinas/Delete/5 (Apenas Administrador)
        [Authorize(Roles = "Administrador")]
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

        // POST: Vacinas/Delete/5 (Apenas Administrador)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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