using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Models; // Certifique-se que este using está correto

namespace ProjetoPetMedDigital.Controllers
{
    public class VeterinariosController : Controller
    {
        private readonly PetMedContext _context;

        public VeterinariosController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Veterinarios
        public async Task<IActionResult> Index()
        {
            // Inclua CadastroColaborador aqui se for exibir Nome dele na Index
            var petMedContext = _context.Veterinarios.Include(v => v.CadastroColaborador);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Veterinarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinarios
                .Include(v => v.CadastroColaborador) // Incluído para carregar o colaborador
                .FirstOrDefaultAsync(m => m.IdVeterinario == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // GET: Veterinarios/Create
        public IActionResult Create()
        {
            // Adicionado SelectList para IdColaborador
            ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome");
            return View();
        }

        // POST: Veterinarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVeterinario,NomeVeterinario,Especialidade,Telefone,Email,IdColaborador,Id,CreatedAt")] Veterinario veterinario)
        {
            // Nota: Se a propriedade CRM foi adicionada ao modelo Veterinario.cs,
            // você precisa incluí-la no [Bind] acima e nas validações abaixo.
            // Exemplo: [Bind("...,Email,CRM,IdColaborador,...")]

            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Validação: Email já existe (para novos registros)
                if (await _context.Veterinarios.AnyAsync(v => v.Email == veterinario.Email))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está cadastrado para outro veterinário.");
                }

                // Validação: CRM já existe (se você adicionou a propriedade CRM no modelo Veterinario.cs)
                // if (await _context.Veterinarios.AnyAsync(v => v.CRM == veterinario.CRM))
                // {
                //     ModelState.AddModelError("CRM", "Este CRM já está cadastrado.");
                // }


                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    // Re-popule ViewDatas para dropdowns antes de retornar a View
                    ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome", veterinario.IdColaborador);
                    return View(veterinario);
                }

                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Add(veterinario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Re-popule ViewDatas se ModelState.IsValid foi falso por Data Annotations
            ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome", veterinario.IdColaborador);
            return View(veterinario);
        }

        // GET: Veterinarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            // Adicionado SelectList para IdColaborador
            ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome", veterinario.IdColaborador);
            return View(veterinario);
        }

        // POST: Veterinarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVeterinario,NomeVeterinario,Especialidade,Telefone,Email,IdColaborador,Id,CreatedAt")] Veterinario veterinario)
        {
            // Nota: Se a propriedade CRM foi adicionada ao modelo Veterinario.cs,
            // você precisa incluí-la no [Bind] acima e nas validações abaixo.
            // Exemplo: [Bind("...,Email,CRM,IdColaborador,...")]

            if (id != veterinario.IdVeterinario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Validação: Email já existe (para registros existentes, verifica se não é o próprio)
                if (await _context.Veterinarios.AnyAsync(c => c.Email == veterinario.Email && c.IdVeterinario != veterinario.IdVeterinario))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está cadastrado para outro veterinário.");
                }

                // Validação: CRM já existe (se você adicionou a propriedade CRM no modelo Veterinario.cs)
                /*
                if (await _context.Veterinarios.AnyAsync(v => v.CRM == veterinario.CRM && v.IdVeterinario != veterinario.IdVeterinario))
                {
                    ModelState.AddModelError("CRM", "Este CRM já está cadastrado para outro veterinário.");
                }
                */

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome", veterinario.IdColaborador);
                    return View(veterinario);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

                try
                {
                    _context.Update(veterinario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarioExists(veterinario.IdVeterinario))
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
            ViewData["IdColaborador"] = new SelectList(_context.CadastroColaboradores, "IdColaborador", "Nome", veterinario.IdColaborador);
            return View(veterinario);
        }

        // GET: Veterinarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinarios
                .Include(v => v.CadastroColaborador) // Incluído para carregar o colaborador
                .FirstOrDefaultAsync(m => m.IdVeterinario == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario != null)
            {
                _context.Veterinarios.Remove(veterinario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarioExists(int id)
        {
            return _context.Veterinarios.Any(e => e.IdVeterinario == id);
        }
    }
}