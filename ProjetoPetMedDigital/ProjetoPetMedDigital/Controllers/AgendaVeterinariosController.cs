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
    public class AgendaVeterinariosController : Controller
    {
        private readonly PetMedContext _context;

        public AgendaVeterinariosController(PetMedContext context)
        {
            _context = context;
        }

        // GET: AgendaVeterinarios
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.AgendaVeterinarios.Include(a => a.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: AgendaVeterinarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendaVeterinario = await _context.AgendaVeterinarios
                .Include(a => a.Veterinario)
                .FirstOrDefaultAsync(m => m.IdAgendaVeterinario == id);
            if (agendaVeterinario == null)
            {
                return NotFound();
            }

            return View(agendaVeterinario);
        }

        // GET: AgendaVeterinarios/Create
        public IActionResult Create()
        {
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario");
            return View();
        }

        // POST: AgendaVeterinarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAgendaVeterinario,IdVeterinario,DataInicio,DataFim,IdPaciente,Id,CreatedAt")] AgendaVeterinario agendaVeterinario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendaVeterinario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario", agendaVeterinario.IdVeterinario);
            return View(agendaVeterinario);
        }

        // GET: AgendaVeterinarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendaVeterinario = await _context.AgendaVeterinarios.FindAsync(id);
            if (agendaVeterinario == null)
            {
                return NotFound();
            }
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario", agendaVeterinario.IdVeterinario);
            return View(agendaVeterinario);
        }

        // POST: AgendaVeterinarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAgendaVeterinario,IdVeterinario,DataInicio,DataFim,IdPaciente,Id,CreatedAt")] AgendaVeterinario agendaVeterinario)
        {
            if (id != agendaVeterinario.IdAgendaVeterinario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendaVeterinario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendaVeterinarioExists(agendaVeterinario.IdAgendaVeterinario))
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
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario", agendaVeterinario.IdVeterinario);
            return View(agendaVeterinario);
        }

        // GET: AgendaVeterinarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendaVeterinario = await _context.AgendaVeterinarios
                .Include(a => a.Veterinario)
                .FirstOrDefaultAsync(m => m.IdAgendaVeterinario == id);
            if (agendaVeterinario == null)
            {
                return NotFound();
            }

            return View(agendaVeterinario);
        }

        // POST: AgendaVeterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agendaVeterinario = await _context.AgendaVeterinarios.FindAsync(id);
            if (agendaVeterinario != null)
            {
                _context.AgendaVeterinarios.Remove(agendaVeterinario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendaVeterinarioExists(int id)
        {
            return _context.AgendaVeterinarios.Any(e => e.IdAgendaVeterinario == id);
        }
    }
}
