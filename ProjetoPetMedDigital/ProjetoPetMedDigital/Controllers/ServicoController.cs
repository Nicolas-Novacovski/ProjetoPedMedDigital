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
    // Se você renomeou o modelo de 'Servico' para 'Servico', este controller continua sendo 'ServicoController'.
    public class ServicoController : Controller
    {
        private readonly PetMedContext _context;

        public ServicoController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Servico
        public async Task<IActionResult> Index()
        {
            // Adicionado Include para carregar as propriedades de navegação
            var petMedContext = _context.Servico.Include(s => s.Agendamento).Include(s => s.ItemEstoque).Include(s => s.Valor).Include(s => s.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Servico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Servico = await _context.Servico
                .Include(s => s.Agendamento)
                .Include(s => s.ItemEstoque)
                .Include(s => s.Valor)
                .Include(s => s.Veterinario)
                .FirstOrDefaultAsync(m => m.IdServico == id);
            if (Servico == null)
            {
                return NotFound();
            }

            return View(Servico);
        }

        // GET: Servico/Create
        public IActionResult Create()
        {
            // Ajustado SelectList para exibir nomes em vez de IDs
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento"); // Exibir a data do agendamento
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto"); // Exibir o nome do produto
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento"); // Exibir o valor do procedimento
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario"); // Exibir o nome do veterinário
            return View();
        }

        // POST: Servico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServico,TipoVenda,NomeServico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] Servico Servico) // Mude 'Servico' para 'Servico' aqui
        {
            if (ModelState.IsValid)
            {
                _context.Add(Servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Ajustado SelectList para exibir nomes em vez de IDs
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", Servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", Servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", Servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", Servico.IdVeterinario);
            return View(Servico);
        }

        // GET: Servico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Servico = await _context.Servico.FindAsync(id);
            if (Servico == null)
            {
                return NotFound();
            }
            // Ajustado SelectList para exibir nomes em vez de IDs
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", Servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", Servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", Servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", Servico.IdVeterinario);
            return View(Servico);
        }

        // POST: Servico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServico,TipoVenda,NomeServico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] Servico Servico) // Mude 'Servico' para 'Servico' aqui
        {
            if (id != Servico.IdServico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(Servico.IdServico))
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
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", Servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", Servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", Servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", Servico.IdVeterinario);
            return View(Servico);
        }

        // GET: Servico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Servico = await _context.Servico
                .Include(s => s.Agendamento)
                .Include(s => s.ItemEstoque)
                .Include(s => s.Valor)
                .Include(s => s.Veterinario)
                .FirstOrDefaultAsync(m => m.IdServico == id);
            if (Servico == null)
            {
                return NotFound();
            }

            return View(Servico);
        }

        // POST: Servico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Servico = await _context.Servico.FindAsync(id);
            if (Servico != null)
            {
                _context.Servico.Remove(Servico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoExists(int id)
        {
            return _context.Servico.Any(e => e.IdServico == id);
        }
    }
}