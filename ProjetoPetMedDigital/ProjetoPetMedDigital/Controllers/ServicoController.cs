using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Models; // Certifique-se que este using está correto
using Microsoft.AspNetCore.Authorization; // NECESSÁRIO para [Authorize]

namespace ProjetoPetMedDigital.Controllers
{
    [Authorize(Roles = "Administrador,Secretaria")] // Exemplo: Apenas Admin e Secretaria podem gerenciar serviços
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
            var petMedContext = _context.Servico.Include(s => s.Agendamento).Include(s => s.ItemEstoque).Include(s => s.Valor).Include(s => s.Veterinario);
            return View(await petMedContext.ToListAsync());
        }

        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
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
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento");
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto");
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento");
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario");
            return View();
        }

        // POST: Servicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServico,TipoVenda,NomeServico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Validação: Data e Hora do serviço não podem ser no passado
                DateTime dataHoraServico = servico.Data.Date + servico.Hora.TimeOfDay;
                if (dataHoraServico < DateTime.Now)
                {
                    ModelState.AddModelError("Data", "A data e hora do serviço não podem ser no passado.");
                }

                // Validação: Preço de venda deve ser maior que zero (além do Range na Model)
                if (servico.PrecoVenda <= 0)
                {
                    ModelState.AddModelError("PrecoVenda", "O preço de venda deve ser maior que zero.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", servico.IdAgendamento);
                    ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", servico.IdProduto);
                    ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", servico.IdValor);
                    ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", servico.IdVeterinario);
                    return View(servico);
                }

                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Servico.Add(servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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

            var servico = await _context.Servico.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", servico.IdVeterinario);
            return View(servico);
        }

        // POST: Servicos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServico,TipoVenda,NomeServico,IdVeterinario,Data,Hora,Status,PrecoVenda,Descricao,IdAgendamento,IdProduto,IdValor,Id,CreatedAt")] Servico servico)
        {
            if (id != servico.IdServico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Validação: Data e Hora do serviço não podem ser no passado (igual ao Create)
                DateTime dataHoraServico = servico.Data.Date + servico.Hora.TimeOfDay;
                if (dataHoraServico < DateTime.Now)
                {
                    ModelState.AddModelError("Data", "A data e hora do serviço não podem ser no passado.");
                }

                // Validação: Preço de venda deve ser maior que zero (igual ao Create)
                if (servico.PrecoVenda <= 0)
                {
                    ModelState.AddModelError("PrecoVenda", "O preço de venda deve ser maior que zero.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", servico.IdAgendamento);
                    ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", servico.IdProduto);
                    ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", servico.IdValor);
                    ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", servico.IdVeterinario);
                    return View(servico);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

                try
                {
                    _context.Servico.Update(servico);
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
            ViewData["IdAgendamento"] = new SelectList(_context.Agendamentos, "IdAgendamento", "DataAgendamento", servico.IdAgendamento);
            ViewData["IdProduto"] = new SelectList(_context.ItensEstoque, "IdProduto", "NomeProduto", servico.IdProduto);
            ViewData["IdValor"] = new SelectList(_context.Valores, "IdValor", "ValorProcedimento", servico.IdValor);
            ViewData["IdVeterinario"] = new SelectList(_context.Veterinarios, "IdVeterinario", "NomeVeterinario", servico.IdVeterinario);
            return View(servico);
        }

        // GET: Servicos/Delete/5 (Apenas Administrador)
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
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

        // POST: Servicos/Delete/5 (Apenas Administrador)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servico = await _context.Servico.FindAsync(id);
            if (servico != null)
            {
                _context.Servico.Remove(servico);
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