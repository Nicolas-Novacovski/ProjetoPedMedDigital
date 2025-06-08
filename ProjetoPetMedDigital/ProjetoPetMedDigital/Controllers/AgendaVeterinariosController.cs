using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var petMedContext = _context.AgendaVeterinarios.Include(a => a.Veterinario).Include(a => a.Paciente);
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
                .Include(a => a.Paciente)
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
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario");
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro");
            return View();
        }

        // POST: AgendaVeterinarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAgendaVeterinario,IdVeterinario,DataInicio,DataFim,IdPaciente,Id,CreatedAt")] AgendaVeterinario agendaVeterinario)
        {
            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Validação: Data de início não pode ser no passado
                if (agendaVeterinario.DataInicio < DateTime.Now)
                {
                    ModelState.AddModelError("DataInicio", "A data e hora de início não podem ser no passado.");
                }

                // Validação: Data de fim não pode ser anterior à data de início
                if (agendaVeterinario.DataFim < agendaVeterinario.DataInicio)
                {
                    ModelState.AddModelError("DataFim", "A data e hora de fim não podem ser anteriores à data de início.");
                }


                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", agendaVeterinario.IdVeterinario);
                    ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", agendaVeterinario.IdPaciente);
                    return View(agendaVeterinario);
                }

                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Add(agendaVeterinario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", agendaVeterinario.IdVeterinario);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", agendaVeterinario.IdPaciente);
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
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", agendaVeterinario.IdVeterinario);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", agendaVeterinario.IdPaciente);
            return View(agendaVeterinario);
        }

        // POST: AgendaVeterinarios/Edit/5
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
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Validação: Data de início não pode ser no passado (igual ao Create)
                if (agendaVeterinario.DataInicio < DateTime.Now)
                {
                    ModelState.AddModelError("DataInicio", "A data e hora de início não podem ser no passado.");
                }

                // Validação: Data de fim não pode ser anterior à data de início (igual ao Create)
                if (agendaVeterinario.DataFim < agendaVeterinario.DataInicio)
                {
                    ModelState.AddModelError("DataFim", "A data e hora de fim não podem ser anteriores à data de início.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", agendaVeterinario.IdVeterinario);
                    ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", agendaVeterinario.IdPaciente);
                    return View(agendaVeterinario);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

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
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", agendaVeterinario.IdVeterinario);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", agendaVeterinario.IdPaciente);
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
                .Include(a => a.Paciente)
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