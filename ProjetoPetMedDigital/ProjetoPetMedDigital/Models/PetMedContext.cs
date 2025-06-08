using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // NECESSÁRIO
using Microsoft.AspNetCore.Identity; // NECESSÁRIO

namespace ProjetoPetMedDigital.Models // <<< NAMESPACE CORRETO PARA ESTE ARQUIVO (PetMedContext)
{
    public class PetMedContext : IdentityDbContext<IdentityUser>
    {
        public PetMedContext(DbContextOptions<PetMedContext> options) : base(options)
        {
        }

        // SEUS DBSETS EXISTENTES
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<AgendaVeterinario> AgendaVeterinarios { get; set; }
        public DbSet<CadastroColaborador> CadastroColaboradores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Servico> Servico { get; set; } // <<< DbSet é 'Servico' (singular)
        // REMOVIDO: public DbSet<Usuario> Usuarios { get; set; } // REMOVIDO AQUI

        public DbSet<Vacina> Vacinas { get; set; }
        public DbSet<Valor> Valores { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<ItemEstoque> ItensEstoque { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // CRÍTICO: Chame o base.OnModelCreating primeiro para Identity

            // Mapeamento de CadastroColaborador para IdentityUser (AspNetUsers)
            modelBuilder.Entity<CadastroColaborador>()
                .HasOne(cc => cc.IdentityUser) // Propriedade de navegação em CadastroColaborador
                .WithOne()
                .HasForeignKey<CadastroColaborador>(cc => cc.IdentityUserId) // A FK é IdentityUserId
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Mantenha todos os seus outros mapeamentos existentes aqui
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Pacientes)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.IdCliente);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Valores)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Agendamentos)
                .WithOne(a => a.Paciente)
                .HasForeignKey(a => a.IdPaciente);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Prontuarios)
                .WithOne(pr => pr.Paciente)
                .HasForeignKey(pr => pr.IdPaciente);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Vacinas)
                .WithOne(v => v.Paciente)
                .HasForeignKey(v => v.IdPaciente);

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Prontuario)
                .WithOne(pr => pr.Agendamento)
                .HasForeignKey<Prontuario>(pr => pr.IdAgendamento)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agendamento>()
                .HasMany(a => a.Servico)
                .WithOne(s => s.Agendamento)
                .HasForeignKey(s => s.IdAgendamento)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Veterinario)
                .WithMany(v => v.Agendamentos)
                .HasForeignKey(a => a.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.Prontuarios)
                .WithOne(pr => pr.Veterinario)
                .HasForeignKey(pr => pr.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.AgendaVeterinarios)
                .WithOne(av => av.Veterinario)
                .HasForeignKey(av => av.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.Servico)
                .WithOne(s => s.Veterinario)
                .HasForeignKey(s => s.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Vacina!)
                .WithOne(v => v.ItemEstoque)
                .HasForeignKey<Vacina>(v => v.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Servico>()
                .HasOne(s => s.Valor!)
                .WithOne(v => v.Servico)
                .HasForeignKey<Servico>(s => s.IdValor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Procedimento!)
                .WithOne(p => p.ItemEstoque)
                .HasForeignKey<Procedimento>(p => p.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Servico!)
                .WithOne(s => s.ItemEstoque)
                .HasForeignKey<Servico>(s => s.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}