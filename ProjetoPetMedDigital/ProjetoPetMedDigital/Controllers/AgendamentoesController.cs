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
    public class AgendamentoesController : Controller
    {
        private readonly PetMedContext _context;

        public AgendamentoesController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Agendamentoes
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Agendamentos.Include(a => a.Paciente).Include(a => a.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Agendamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.Paciente)
                .Include(a => a.Veterinario)
                .FirstOrDefaultAsync(m => m.IdAgendamento == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // GET: Agendamentoes/Create
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente");
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario");
            return View();
        }

        // POST: Agendamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAgendamento,IdPaciente,IdVeterinario,DataAgendamento,HoraAgendamento,Observacoes,Id,CreatedAt")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", agendamento.IdPaciente);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario", agendamento.IdVeterinario);
            return View(agendamento);
        }

        // GET: Agendamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", agendamento.IdPaciente);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario", agendamento.IdVeterinario);
            return View(agendamento);
        }

        // POST: Agendamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAgendamento,IdPaciente,IdVeterinario,DataAgendamento,HoraAgendamento,Observacoes,Id,CreatedAt")] Agendamento agendamento)
        {
            if (id != agendamento.IdAgendamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.IdAgendamento))
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
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "IdPaciente", agendamento.IdPaciente);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario", agendamento.IdVeterinario);
            return View(agendamento);
        }

        // GET: Agendamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.Paciente)
                .Include(a => a.Veterinario)
                .FirstOrDefaultAsync(m => m.IdAgendamento == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // POST: Agendamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int id)
        {
            return _context.Agendamentos.Any(e => e.IdAgendamento == id);
        }
    }
}
