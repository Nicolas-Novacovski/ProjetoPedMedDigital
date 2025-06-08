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
    [Authorize(Roles = "Administrador,Veterinario,Secretaria")] // Admin, Vet e Secretaria podem gerenciar pacientes
    public class PacientesController : Controller
    {
        private readonly PetMedContext _context;

        public PacientesController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Pacientes.Include(p => p.Cliente);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel");
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPaciente,IdCliente,NomeCachorro,Estado,Problema,TipoAtendimento,Peso,SinaisVitais,Recomendacoes,Id,CreatedAt")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Validação: Peso deve ser positivo
                if (paciente.Peso <= 0)
                {
                    ModelState.AddModelError("Peso", "O peso do animal deve ser um valor positivo.");
                }

                // Validação: Nome do cachorro não pode ser igual a "Cachorro Genérico" (exemplo)
                if (paciente.NomeCachorro.Equals("Cachorro Genérico", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("NomeCachorro", "Por favor, digite um nome específico para o animal.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", paciente.IdCliente);
                    return View(paciente);
                }

                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", paciente.IdCliente);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", paciente.IdCliente);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaciente,IdCliente,NomeCachorro,Estado,Problema,TipoAtendimento,Peso,SinaisVitais,Recomendacoes,Id,CreatedAt")] Paciente paciente)
        {
            if (id != paciente.IdPaciente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Validação: Peso deve ser positivo (igual ao Create)
                if (paciente.Peso <= 0)
                {
                    ModelState.AddModelError("Peso", "O peso do animal deve ser um valor positivo.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", paciente.IdCliente);
                    return View(paciente);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.IdPaciente))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "NomeResponsavel", paciente.IdCliente);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5 (Apenas Administrador)
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5 (Apenas Administrador)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.IdPaciente == id);
        }
    }
}