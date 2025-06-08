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
    public class ClientesController : Controller
    {
        private readonly PetMedContext _context;

        public ClientesController(PetMedContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,NomeResponsavel,Telefone,Email,CPF,RG,DtNascimento,CEP,Endereco,Bairro,Cidade,Id,CreatedAt")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO ***

                // Validação: Idade mínima (ex: 18 anos)
                if (cliente.DtNascimento > DateTime.Now.AddYears(-18))
                {
                    ModelState.AddModelError("DtNascimento", "O cliente deve ter no mínimo 18 anos.");
                }

                // Validação: CPF já existe
                if (await _context.Clientes.AnyAsync(c => c.CPF == cliente.CPF && c.IdCliente != cliente.IdCliente))
                {
                    ModelState.AddModelError("CPF", "Este CPF já está cadastrado.");
                }

                // Validação: Email já existe
                if (await _context.Clientes.AnyAsync(c => c.Email == cliente.Email && c.IdCliente != cliente.IdCliente))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    return View(cliente);
                }

                // *** FIM DA LÓGICA DE NEGÓCIO ***

                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,NomeResponsavel,Telefone,Email,CPF,RG,DtNascimento,CEP,Endereco,Bairro,Cidade,Id,CreatedAt")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // *** INÍCIO DA LÓGICA DE NEGÓCIO PARA EDIT ***
                // Validação: Idade mínima (igual ao Create)
                if (cliente.DtNascimento > DateTime.Now.AddYears(-18))
                {
                    ModelState.AddModelError("DtNascimento", "O cliente deve ter no mínimo 18 anos.");
                }

                // Validação: CPF já existe (igual ao Create)
                if (await _context.Clientes.AnyAsync(c => c.CPF == cliente.CPF && c.IdCliente != cliente.IdCliente))
                {
                    ModelState.AddModelError("CPF", "Este CPF já está cadastrado.");
                }

                // Validação: Email já existe (igual ao Create)
                if (await _context.Clientes.AnyAsync(c => c.Email == cliente.Email && c.IdCliente != cliente.IdCliente))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                }

                // Se alguma validação customizada falhou, retorne a View
                if (!ModelState.IsValid)
                {
                    return View(cliente);
                }
                // *** FIM DA LÓGICA DE NEGÓCIO PARA EDIT ***

                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}