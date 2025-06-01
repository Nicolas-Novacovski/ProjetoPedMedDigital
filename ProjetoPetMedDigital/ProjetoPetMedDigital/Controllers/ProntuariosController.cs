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
    public class ProntuariosController : Controller
    {
        private readonly PetMedContext _context;

        public ProntuariosController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Prontuarios
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Prontuarios.Include(p => p.Agendamento).Include(p => p.Paciente).Include(p => p.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Prontuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prontuario = await _context.Prontuarios
                .Include(p => p.Agendamento)
                .Include(p => p.Paciente)
                .Include(p => p.Veterinario)
                .FirstOrDefaultAsync(m => m.IdProntuario == id);
            if (prontuario == null)
            {
                return NotFound();
            }

            return View(prontuario);
        }

        // GET: Prontuarios/Create
        public IActionResult Create()
        {
            // Ajustado SelectList para exibir o ID (ou Nome/Data se preferir)
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento"); // Exibir a data do agendamento
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro"); // Exibir o nome do paciente
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario"); // Exibir o nome do veterinário
            return View();
        }

        // POST: Prontuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProntuario,IdAgendamento,IdVeterinario,DataConsulta,MotivoConsulta,Diagnostico,Tratamento,Peso,Temperatura,FrequenciaCardiaca,FrequenciaRespiratoria,Observacoes,IdPaciente,Id,CreatedAt")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                // *** Exemplo de LÓGICA DE NEGÓCIO AQUI (se desejar implementar) ***
                // Exemplo: Validação customizada - Data de Consulta não pode ser no futuro
                if (prontuario.DataConsulta.Date > DateTime.Today)
                {
                    ModelState.AddModelError("DataConsulta", "A data da consulta não pode ser no futuro.");
                }

                // Se houver erros customizados, retorne a view para exibir a mensagem
                if (!ModelState.IsValid)
                {
                    // Re-popule ViewDatas para dropdowns antes de retornar a View
                    ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", prontuario.IdAgendamento);
                    ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", prontuario.IdPaciente);
                    ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", prontuario.IdVeterinario);
                    return View(prontuario);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Add(prontuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Se ModelState.IsValid foi falso inicialmente (por Data Annotations), re-popule ViewDatas
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", prontuario.IdAgendamento);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", prontuario.IdPaciente);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", prontuario.IdVeterinario);
            return View(prontuario);
        }

        // GET: Prontuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prontuario = await _context.Prontuarios.FindAsync(id);
            if (prontuario == null)
            {
                return NotFound();
            }
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", prontuario.IdAgendamento);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", prontuario.IdPaciente);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", prontuario.IdVeterinario);
            return View(prontuario);
        }

        // POST: Prontuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProntuario,IdAgendamento,IdVeterinario,DataConsulta,MotivoConsulta,Diagnostico,Tratamento,Peso,Temperatura,FrequenciaCardiaca,FrequenciaRespiratoria,Observacoes,IdPaciente,Id,CreatedAt")] Prontuario prontuario)
        {
            if (id != prontuario.IdProntuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prontuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProntuarioExists(prontuario.IdProntuario))
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
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", prontuario.IdAgendamento);
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", prontuario.IdPaciente);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", prontuario.IdVeterinario);
            return View(prontuario);
        }

        // GET: Prontuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prontuario = await _context.Prontuarios
                .Include(p => p.Agendamento)
                .Include(p => p.Paciente)
                .Include(p => p.Veterinario)
                .FirstOrDefaultAsync(m => m.IdProntuario == id);
            if (prontuario == null)
            {
                return NotFound();
            }

            return View(prontuario);
        }

        // POST: Prontuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prontuario = await _context.Prontuarios.FindAsync(id);
            if (prontuario != null)
            {
                _context.Prontuarios.Remove(prontuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProntuarioExists(int id)
        {
            return _context.Prontuarios.Any(e => e.IdProntuario == id);
        }
    }
}