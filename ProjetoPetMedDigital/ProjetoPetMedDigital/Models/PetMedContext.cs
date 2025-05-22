using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Models;
using PetMed_Digital.Models; // Adicionado para BaseModel se necessário para outras entidades

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
            base.OnModelCreating(modelBuilder);

            // As configurações de chave estrangeira e relações estão corretas
            // baseadas nas propriedades de navegação e FKs que você definiu.

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
                .HasMany(a => a.Servicos)
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
                .HasMany(v => v.Servicos)
                .WithOne(s => s.Veterinario)
                .HasForeignKey(s => s.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemEstoque com relações One-to-One onde IdProduto é a PK e a FK
            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Vacina)
                .WithOne(v => v.ItemEstoque)
                .HasForeignKey<Vacina>(v => v.IdProduto) // Vacina.IdProduto é FK e PK de Vacina
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Procedimento)
                .WithOne(p => p.ItemEstoque)
                .HasForeignKey<Procedimento>(p => p.IdProduto) // Procedimento.IdProduto é FK e PK de Procedimento
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Servico)
                .WithOne(s => s.ItemEstoque)
                .HasForeignKey<Servico>(s => s.IdProduto) // Servico.IdProduto é FK e PK de Servico
                .OnDelete(DeleteBehavior.Restrict);

            // Relação entre Servico e Valor (Servico tem uma FK para Valor)
            modelBuilder.Entity<Servico>()
                .HasOne(s => s.Valor)
                .WithOne(v => v.Servico) // Valor tem uma navegação para Servico
                .HasForeignKey<Servico>(s => s.IdValor) // Servico.IdValor é a FK para Valor.IdValor
                .OnDelete(DeleteBehavior.Restrict);


            // Relação CadastroColaborador e Usuario (Um para Um, com FK em Usuario)
            modelBuilder.Entity<CadastroColaborador>()
                .HasOne(cc => cc.Usuario)
                .WithOne(u => u.CadastroColaborador)
                .HasForeignKey<Usuario>(u => u.IdColaborador) // Usuario.IdColaborador é a FK para CadastroColaborador.IdColaborador
                .OnDelete(DeleteBehavior.Restrict);

            // Relação CadastroColaborador e Veterinario (Um para Um, com FK em Veterinario)
            modelBuilder.Entity<CadastroColaborador>()
                .HasOne(cc => cc.Veterinario)
                .WithOne(v => v.CadastroColaborador)
                .HasForeignKey<Veterinario>(v => v.IdColaborador) // Veterinario.IdColaborador é a FK para CadastroColaborador.IdColaborador
                .OnDelete(DeleteBehavior.Restrict);

            // Configuração para Prontuario, com as FKs explicitamente definidas
            modelBuilder.Entity<Prontuario>(entity =>
            {
                entity.HasOne(p => p.Veterinario)
                      .WithMany(v => v.Prontuarios)
                      .HasForeignKey(p => p.IdVeterinario)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Paciente)
                      .WithMany(pa => pa.Prontuarios)
                      .HasForeignKey(p => p.IdPaciente)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Chave Primária composta ou alternativa para Usuario (já estava assim, mantido)
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Login);
        }
    }
}