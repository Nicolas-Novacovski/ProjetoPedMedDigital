using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Data;
using ProjetoPetMedDigital.Models; // Certifique-se que este using está correto

namespace ProjetoPetMedDigital.Controllers
{
    // O nome da classe do Controller geralmente é o plural do modelo.
    // Se você renomeou o modelo de 'servico' para 'servico', este controller continua sendo 'servicoController'.
    public class servicoController : Controller
    {
        private readonly PetMedContext _context;

        public servicoController(PetMedContext context)
        {
            _context = context;
        }

        // GET: servico
        public async Task<IActionResult> Index()
        {
            // Adicionado Include para carregar as propriedades de navegação
            var petMedContext = _context.servico.Include(s => s.Agendamento).Include(s => s.ItemEstoque).Include(s => s.Valor).Include(s => s.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: servico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.servico
                .Include(s => s.Agendamento)
                .Include(s => s.ItemEstoque)
                .Include(s => s.Valor)
                .Include(s => s.Veterinario)
                .FirstOrDefaultAsync(m => m.Idservico == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // GET: servico/Create
        public IActionResult Create()
        {
            // Ajustado SelectList para exibir nomes em vez de IDs
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento"); // Exibir a data do agendamento
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto"); // Exibir o nome do produto
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento"); // Exibir o valor do procedimento
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario"); // Exibir o nome do veterinário
            return View();
        }

        // POST: servico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idservico,TipoVenda,Nomeservico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] servico servico) // Mude 'servico' para 'servico' aqui
        {
            if (ModelState.IsValid)
            {
                _context.Add(servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Ajustado SelectList para exibir nomes em vez de IDs
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", servico.IdVeterinario);
            return View(servico);
        }

        // GET: servico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.servico.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            // Ajustado SelectList para exibir nomes em vez de IDs
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", servico.IdVeterinario);
            return View(servico);
        }

        // POST: servico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idservico,TipoVenda,Nomeservico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] servico servico) // Mude 'servico' para 'servico' aqui
        {
            if (id != servico.Idservico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!servicoExists(servico.Idservico))
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
            // Ajustado SelectList para exibir nomes em vez de IDs
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", servico.IdVeterinario);
            return View(servico);
        }

        // GET: servico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.servico
                .Include(s => s.Agendamento)
                .Include(s => s.ItemEstoque)
                .Include(s => s.Valor)
                .Include(s => s.Veterinario)
                .FirstOrDefaultAsync(m => m.Idservico == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: servico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servico = await _context.servico.FindAsync(id);
            if (servico != null)
            {
                _context.servico.Remove(servico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool servicoExists(int id)
        {
            return _context.servico.Any(e => e.Idservico == id);
        }
    }
}