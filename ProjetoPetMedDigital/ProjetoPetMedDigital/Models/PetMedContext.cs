using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProjetoPetMedDigital.Models; // Seu namespace principal de modelos

namespace ProjetoPetMedDigital.Models // NAMESPACE CORRETO PARA PetMedContext
{
    public class PetMedContext : IdentityDbContext<IdentityUser>
    {
        public PetMedContext(DbContextOptions<PetMedContext> options) : base(options)
        {
        }

        // Seus DbSets existentes
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<AgendaVeterinario> AgendaVeterinarios { get; set; }
        public DbSet<CadastroColaborador> CadastroColaboradores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Servico> Servico { get; set; } // Ajustado para 'Servico' (singular)
        public DbSet<Vacina> Vacinas { get; set; }
        public DbSet<Valor> Valores { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<ItemEstoque> ItensEstoque { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CRUCIAL: Chame o base.OnModelCreating primeiro para configurar o Identity
            base.OnModelCreating(modelBuilder);

            // Mapeamento de CadastroColaborador para IdentityUser (AspNetUsers)
            modelBuilder.Entity<CadastroColaborador>()
                .HasOne(cc => cc.IdentityUser)
                .WithOne()
                .HasForeignKey<CadastroColaborador>(cc => cc.IdentityUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // *** CORREÇÃO AQUI: Mapeamento da relação 1:1 entre Veterinario e CadastroColaborador ***
            // Definimos que Veterinario é o lado dependente e IdColaborador é a FK em Veterinario.
            // Um Veterinário tem UM CadastroColaborador
            // Um CadastroColaborador PODE TER UM Veterinario (opcional, por isso o ! no WithOne)
            modelBuilder.Entity<Veterinario>()
                .HasOne(v => v.CadastroColaborador) // Veterinario tem um CadastroColaborador
                .WithOne(cc => cc.Veterinario!)     // CadastroColaborador tem um Veterinario (o '!' indica que é obrigatório para o EF)
                .HasForeignKey<Veterinario>(v => v.IdColaborador) // A FK está em Veterinario e é IdColaborador
                .IsRequired()                       // A FK é obrigatória (todo Veterinario deve ter um CadastroColaborador)
                .OnDelete(DeleteBehavior.Cascade);  // Se o CadastroColaborador for deletado, o Veterinario também é (ou Restrict se preferir)


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