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
    // Se você renomeou o modelo de 'Serviços' para 'Servico', este controller continua sendo 'ServicosController'.
    public class ServicosController : Controller
    {
        private readonly PetMedContext _context;

        public ServicosController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
            // Adicionado Include para carregar as propriedades de navegação
            var petMedContext = _context.Servicos.Include(s => s.Agendamento).Include(s => s.ItemEstoque).Include(s => s.Valor).Include(s => s.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos
                .Include(s => s.Agendamento)
                .Include(s => s.ItemEstoque)
                .Include(s => s.Valor)
                .Include(s => s.Veterinario)
                .FirstOrDefaultAsync(m => m.IdServico == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // GET: Servicos/Create
        public IActionResult Create()
        {
            // Ajustado SelectList para exibir nomes em vez de IDs
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento"); // Exibir a data do agendamento
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto"); // Exibir o nome do produto
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento"); // Exibir o valor do procedimento
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario"); // Exibir o nome do veterinário
            return View();
        }

        // POST: Servicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServico,TipoVenda,NomeServico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] Servico servico) // Mude 'Serviços' para 'Servico' aqui
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

        // GET: Servicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos.FindAsync(id);
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

        // POST: Servicos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServico,TipoVenda,NomeServico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] Servico servico) // Mude 'Serviços' para 'Servico' aqui
        {
            if (id != servico.IdServico)
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
                    if (!ServicoExists(servico.IdServico))
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

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos
                .Include(s => s.Agendamento)
                .Include(s => s.ItemEstoque)
                .Include(s => s.Valor)
                .Include(s => s.Veterinario)
                .FirstOrDefaultAsync(m => m.IdServico == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico != null)
            {
                _context.Servicos.Remove(servico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoExists(int id)
        {
            return _context.Servicos.Any(e => e.IdServico == id);
        }
    }
}