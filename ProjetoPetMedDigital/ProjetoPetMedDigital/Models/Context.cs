using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Models;

namespace ProjetoPetMedDigital.Data
{
    public class PetMedContext : DbContext
    {
        public PetMedContext(DbContextOptions<PetMedContext> options) : base(options)
        {
        }

        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<AgendaVeterinario> AgendaVeterinarios { get; set; }
        public DbSet<CadastroColaborador> CadastroColaboradores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vacina> Vacinas { get; set; }
        public DbSet<Valor> Valores { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<ItemEstoque> ItensEstoque { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de chave primária (já feitas por convenção com o atributo [Key]

            // Relacionamento Cliente para Paciente (um para muitos)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Pacientes)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.IdCliente);

            // Relacionamento Paciente para Agendamento (um para muitos)
            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Agendamentos)
                .WithOne(a => a.Paciente)
                .HasForeignKey(a => a.IdPaciente);

            // Relacionamento Paciente para Prontuario (um para muitos)
            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Prontuarios)
                .WithOne(pr => pr.Paciente)
                .HasForeignKey(pr => pr.IdPaciente);

            // Relacionamento Paciente para Vacina (um para muitos)
            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Vacinas)
                .WithOne(v => v.Paciente)
                .HasForeignKey(v => v.IdPaciente);

            // Relacionamento Agendamento para Prontuario (um para um)
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Prontuario)
                .WithOne(pr => pr.Agendamento)
                .HasForeignKey<Prontuario>(pr => pr.IdAgendamento)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Relacionamento Agendamento para Servico (um para muitos)
            modelBuilder.Entity<Agendamento>()
                .HasMany(a => a.Servicos)
                .WithOne(s => s.Agendamento)
                .HasForeignKey(s => s.IdAgendamento)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Relacionamento Agendamento para Veterinario (um para muitos)
            modelBuilder.Entity<Agendamento>()
                .HasMany(a => a.Veterinarios)
                .WithOne(v => v.Agendamento)
                .HasForeignKey(v => v.IdAgendamento)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Relacionamento Veterinario para Prontuario (um para muitos)
            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.Prontuarios)
                .WithOne(pr => pr.Veterinario)
                .HasForeignKey(pr => pr.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Relacionamento Veterinario para AgendaVeterinario (um para muitos)
            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.AgendaVeterinarios)
                .WithOne(av => av.Veterinario)
                .HasForeignKey(av => av.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Relacionamento Vacina para ItemEstoque (um para um)
            modelBuilder.Entity<Vacina>()
                .HasOne(v => v.ItemEstoque)
                .WithOne(ie => ie.Vacina)
                .HasForeignKey<ItemEstoque>(ie => ie.IdProduto)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Relacionamento Servico para Valor (um para um)
            modelBuilder.Entity<Servico>()
                .HasOne(s => s.Valor)
                .WithOne(v => v.Servico)
                .HasForeignKey<Valor>(v => v.IdValor)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Relacionamento Procedimento para ItemEstoque (um para um)
            modelBuilder.Entity<Procedimento>()
                .HasOne(p => p.ItemEstoque)
                .WithOne(ie => ie.Procedimento)
                .HasForeignKey<ItemEstoque>(ie => ie.IdProduto)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Relacionamento CadastroColaborador para Usuario (um para um)
            modelBuilder.Entity<CadastroColaborador>()
                .HasOne(cc => cc.Usuario)
                .WithOne(u => u.CadastroColaborador)
                .HasForeignKey<Usuario>(u => u.IdColaborador)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Relacionamento Veterinario para CadastroColaborador (um para um)
            modelBuilder.Entity<Veterinario>()
                .HasOne(v => v.CadastroColaborador)
                .WithOne(cc => cc.Veterinario)
                .HasForeignKey<CadastroColaborador>(cc => cc.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Configuração da entidade Prontuario
            modelBuilder.Entity<Prontuario>()
                .HasOne(p => p.Agendamento)
                .WithOne(a => a.Prontuario)
                .HasForeignKey<Prontuario>(p => p.IdAgendamento)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            modelBuilder.Entity<Prontuario>()
                .HasOne(p => p.Veterinario)
                .WithMany(v => v.Prontuarios)
                .HasForeignKey(p => p.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            modelBuilder.Entity<Prontuario>()
                .HasOne(p => p.Paciente)
                .WithMany(pa => pa.Prontuarios)
                .HasForeignKey(p => p.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            // Configuração da entidade ItemEstoque
            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Vacina)
                .WithOne(v => v.ItemEstoque)
                .HasForeignKey<Vacina>(v => v.IdProduto)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Procedimento)
                .WithOne(p => p.ItemEstoque)
                .HasForeignKey<Procedimento>(p => p.IdProduto)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Servico)
                .WithOne(s => s.ItemEstoque)
                .HasForeignKey<Servico>(s => s.IdProduto)
                .OnDelete(DeleteBehavior.Restrict); // Adicionado para evitar exclusão em cascata

            base.OnModelCreating(modelBuilder);
        }
    }
}