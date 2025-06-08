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
    [Authorize(Roles = "Administrador,Secretaria,Veterinario")] // Admin e Secretaria podem tudo, Veterinario talvez só possa ver a Index e Details
    public class AgendamentosController : Controller
    {
        private readonly PetMedContext _context;

        public AgendamentosController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Agendamentos
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Agendamentos.Include(a => a.Paciente).Include(a => a.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Agendamentos/Details/5
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

        // GET: Agendamentos/Create (Apenas Admin e Secretaria)
        [Authorize(Roles = "Administrador,Secretaria")]
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro");
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario");
            return View();
        }

        // POST: Agendamentos/Create (Apenas Admin e Secretaria)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Secretaria")]
        public async Task<IActionResult> Create([Bind("IdAgendamento,IdPaciente,IdVeterinario,DataAgendamento,HoraAgendamento,Observacoes,Id,CreatedAt")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                // ... (Sua lógica de negócio) ...
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", agendamento.IdPaciente);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", agendamento.IdVeterinario);
            return View(agendamento);
        }

        // GET: Agendamentos/Edit/5 (Apenas Admin e Secretaria)
        [Authorize(Roles = "Administrador,Secretaria")]
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
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", agendamento.IdPaciente);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", agendamento.IdVeterinario);
            return View(agendamento);
        }

        // POST: Agendamentos/Edit/5 (Apenas Admin e Secretaria)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Secretaria")]
        public async Task<IActionResult> Edit(int id, [Bind("IdAgendamento,IdPaciente,IdVeterinario,DataAgendamento,HoraAgendamento,Observacoes,Id,CreatedAt")] Agendamento agendamento)
        {
            if (id != agendamento.IdAgendamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // ... (Sua lógica de negócio) ...
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
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", agendamento.IdPaciente);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", agendamento.IdVeterinario);
            return View(agendamento);
        }

        // GET: Agendamentos/Delete/5 (Apenas Administrador)
        [Authorize(Roles = "Administrador")]
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

        // POST: Agendamentos/Delete/5 (Apenas Administrador)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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