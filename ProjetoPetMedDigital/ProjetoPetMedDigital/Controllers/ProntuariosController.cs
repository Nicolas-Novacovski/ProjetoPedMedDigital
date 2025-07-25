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
    [Authorize(Roles = "Administrador,Veterinario")] // Admin e Veterinario podem gerenciar prontuários
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
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Validação: Data de Consulta não pode ser no futuro
                if (prontuario.DataConsulta.Date > DateTime.Today)
                {
                    ModelState.AddModelError("DataConsulta", "A data da consulta não pode ser no futuro.");
                }

                // Validação: Peso do animal deve ser positivo (além do Range na Model)
                if (prontuario.Peso.HasValue && prontuario.Peso.Value <= 0)
                {
                    ModelState.AddModelError("Peso", "O peso deve ser um valor positivo.");
                }

                // Se alguma validação customizada falhou, retorne a View para exibir a mensagem de erro
                if (!ModelState.IsValid)
                {
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
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Validação: Data de Consulta não pode ser no futuro (igual ao Create)
                if (prontuario.DataConsulta.Date > DateTime.Today)
                {
                    ModelState.AddModelError("DataConsulta", "A data da consulta não pode ser no futuro.");
                }

                // Validação: Peso do animal deve ser positivo (igual ao Create)
                if (prontuario.Peso.HasValue && prontuario.Peso.Value <= 0)
                {
                    ModelState.AddModelError("Peso", "O peso do animal deve ser um valor positivo.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", prontuario.IdAgendamento);
                    ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "NomeCachorro", prontuario.IdPaciente);
                    ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", prontuario.IdVeterinario);
                    return View(prontuario);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

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

        // GET: Prontuarios/Delete/5 (Apenas Administrador)
        [Authorize(Roles = "Administrador")]
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

        // POST: Prontuarios/Delete/5 (Apenas Administrador)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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