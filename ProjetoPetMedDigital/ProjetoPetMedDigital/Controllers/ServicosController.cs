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
    public class ServicosController : Controller
    {
        private readonly PetMedContext _context;

        public ServicosController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Servicoes
        public async Task<IActionResult> Index()
        {
            var petMedContext = _context.Servicos.Include(s => s.Agendamento).Include(s => s.ItemEstoque).Include(s => s.Valor).Include(s => s.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Servicoes/Details/5
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

        // GET: Servicoes/Create
        public IActionResult Create()
        {
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "IdAgendamento");
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto");
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "IdValor");
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario");
            return View();
        }

        // POST: Servicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServico,TipoVenda,NomeServico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] Serviços servico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "IdAgendamento", servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto", servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "IdValor", servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario", servico.IdVeterinario);
            return View(servico);
        }

        // GET: Servicoes/Edit/5
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
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "IdAgendamento", servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto", servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "IdValor", servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario", servico.IdVeterinario);
            return View(servico);
        }

        // POST: Servicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServico,TipoVenda,NomeServico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] Serviços servico)
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
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "IdAgendamento", servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "IdProduto", servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "IdValor", servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "IdVeterinario", servico.IdVeterinario);
            return View(servico);
        }

        // GET: Servicoes/Delete/5
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

        // POST: Servicoes/Delete/5
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
