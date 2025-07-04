﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using ProjetoPetMedDigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoPetMedDigital.Controllers
{
    [Authorize(Roles = "Administrador,Secretaria")] // Apenas Admin e Secretaria podem gerenciar colaboradores
    public class CadastroColaboradoresController : Controller
    {
        private readonly PetMedContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CadastroColaboradoresController(PetMedContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CadastroColaboradores
        public async Task<IActionResult> Index()
        {
            var cadastroColaboradores = await _context.CadastroColaboradores
                                                          .Include(c => c.IdentityUser)
                                                          .Include(c => c.Veterinario)
                                                          .ToListAsync();
            return View(cadastroColaboradores);
        }

        // GET: CadastroColaboradores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroColaborador = await _context.CadastroColaboradores
                .Include(c => c.IdentityUser)
                .Include(c => c.Veterinario)
                .FirstOrDefaultAsync(m => m.IdColaborador == id);
            if (cadastroColaborador == null)
            {
                return NotFound();
            }

            return View(cadastroColaborador);
        }

        // GET: CadastroColaboradores/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: CadastroColaboradores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdColaborador,Nome,Telefone,Email,CPF,RG,Dtnascimento,CEP,Endereco,Bairro,Cidade,Cargo,TipoUsuario,IdentityUserId,Id,CreatedAt")] CadastroColaborador cadastroColaborador)
        {
            if (ModelState.IsValid)
            {

                if (!ModelState.IsValid)
                {
                    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "UserName", cadastroColaborador.IdentityUserId);
                    return View(cadastroColaborador);
                }

                _context.Add(cadastroColaborador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "UserName", cadastroColaborador.IdentityUserId);
            return View(cadastroColaborador);
        }

        // GET: CadastroColaboradores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroColaborador = await _context.CadastroColaboradores.FindAsync(id);
            if (cadastroColaborador == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "UserName", cadastroColaborador.IdentityUserId);
            return View(cadastroColaborador);
        }

        // POST: CadastroColaboradores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdColaborador,Nome,Telefone,Email,CPF,RG,Dtnascimento,CEP,Endereco,Bairro,Cidade,Cargo,TipoUsuario,IdentityUserId,Id,CreatedAt")] CadastroColaborador cadastroColaborador)
        {
            if (id != cadastroColaborador.IdColaborador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (!ModelState.IsValid)
                {
                    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "UserName", cadastroColaborador.IdentityUserId);
                    return View(cadastroColaborador);
                }

                try
                {
                    _context.Update(cadastroColaborador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CadastroColaboradorExists(cadastroColaborador.IdColaborador))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "UserName", cadastroColaborador.IdentityUserId);
            return View(cadastroColaborador);
        }

        // GET: CadastroColaboradores/Delete/5 (Apenas Administrador)
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroColaborador = await _context.CadastroColaboradores
                .Include(c => c.IdentityUser)
                .Include(c => c.Veterinario)
                .FirstOrDefaultAsync(m => m.IdColaborador == id);
            if (cadastroColaborador == null)
            {
                return NotFound();
            }

            return View(cadastroColaborador);
        }

        // POST: CadastroColaboradores/Delete/5 (Apenas Administrador)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cadastroColaborador = await _context.CadastroColaboradores.FindAsync(id);
            if (cadastroColaborador != null)
            {
                _context.CadastroColaboradores.Remove(cadastroColaborador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CadastroColaboradorExists(int id)
        {
            return _context.CadastroColaboradores.Any(e => e.IdColaborador == id);
        }
    }
}