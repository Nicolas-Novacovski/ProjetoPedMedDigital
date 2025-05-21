using Microsoft.EntityFrameworkCore;
using ProjetoPetMedDigital.Models; // Models do projeto atual
// using PetMed_Digital.Models; // Se BaseModel ou outros estiverem aqui e não forem importados por ProjetoPetMedDigital.Models

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
            base.OnModelCreating(modelBuilder); // É bom chamar o base primeiro

            // Relacionamento Cliente para Paciente (um para muitos)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Pacientes)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.IdCliente);

            // Relacionamento Cliente para Valor (um para muitos) - se aplicável
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Valores)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.IdCliente)
                .OnDelete(DeleteBehavior.Restrict); // Exemplo, ajuste conforme necessidade

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
                .HasForeignKey<Prontuario>(pr => pr.IdAgendamento) // FK em Prontuario
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Agendamento para Servico (um para muitos)
            modelBuilder.Entity<Agendamento>()
                .HasMany(a => a.Servicos)       // Agendamento tem muitos Servicos
                .WithOne(s => s.Agendamento)    // Servico pertence a um Agendamento
                .HasForeignKey(s => s.IdAgendamento) // FK em Servico
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Agendamento para Veterinario (muitos para um)
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Veterinario)         // Agendamento tem um Veterinario
                .WithMany(v => v.Agendamentos)      // Veterinario tem muitos Agendamentos
                .HasForeignKey(a => a.IdVeterinario) // FK em Agendamento
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Veterinario para Prontuario (um para muitos)
            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.Prontuarios)
                .WithOne(pr => pr.Veterinario)
                .HasForeignKey(pr => pr.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Veterinario para AgendaVeterinario (um para muitos)
            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.AgendaVeterinarios)
                .WithOne(av => av.Veterinario)
                .HasForeignKey(av => av.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Veterinario para Servico (um para muitos)
            modelBuilder.Entity<Veterinario>()
                .HasMany(v => v.Servicos)
                .WithOne(s => s.Veterinario)
                .HasForeignKey(s => s.IdVeterinario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ItemEstoque para Vacina (um para um)
            // ItemEstoque é o principal, Vacina é o dependente e usa IdProduto como FK
            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Vacina)          // ItemEstoque tem uma Vacina (opcional)
                .WithOne(v => v.ItemEstoque)      // Vacina está associada a um ItemEstoque
                .HasForeignKey<Vacina>(v => v.IdProduto) // FK em Vacina
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Servico para Valor (um para um)
            // Assumindo que Servico.IdValor é FK para Valor.IdValor (PK de Valor)
            modelBuilder.Entity<Servico>()
                .HasOne(s => s.Valor)          // Servico tem um Valor
                .WithOne(v => v.Servico)       // Valor está associado a um Servico
                .HasForeignKey<Servico>(s => s.IdValor) // FK em Servico para Valor.IdValor
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ItemEstoque para Procedimento (um para um)
            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Procedimento)    // ItemEstoque tem um Procedimento (opcional)
                .WithOne(p => p.ItemEstoque)      // Procedimento está associado a um ItemEstoque
                .HasForeignKey<Procedimento>(p => p.IdProduto) // FK em Procedimento
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento ItemEstoque para Servico (um para um, se um ItemEstoque específico é para um Serviço)
            // Servico.IdProduto é FK para ItemEstoque.IdProduto
            modelBuilder.Entity<ItemEstoque>()
                .HasOne(ie => ie.Servico)         // ItemEstoque tem um Servico (opcional)
                .WithOne(s => s.ItemEstoque)      // Servico está associado a um ItemEstoque
                .HasForeignKey<Servico>(s => s.IdProduto) // FK em Servico
                .OnDelete(DeleteBehavior.Restrict);


            // Relacionamento CadastroColaborador para Usuario (um para um)
            // CadastroColaborador é principal, Usuario é dependente
            modelBuilder.Entity<CadastroColaborador>()
                .HasOne(cc => cc.Usuario)
                .WithOne(u => u.CadastroColaborador)
                .HasForeignKey<Usuario>(u => u.IdColaborador) // FK em Usuario
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento CadastroColaborador para Veterinario (um para um)
            // CadastroColaborador é principal, Veterinario é dependente
            modelBuilder.Entity<CadastroColaborador>()
                .HasOne(cc => cc.Veterinario)         // CadastroColaborador tem um Veterinario (opcional)
                .WithOne(v => v.CadastroColaborador)  // Veterinario está associado a um CadastroColaborador
                .HasForeignKey<Veterinario>(v => v.IdColaborador) // FK em Veterinario
                .OnDelete(DeleteBehavior.Restrict);

            // Configurações adicionais para Prontuario (já cobertas parcialmente acima, mas centralizando)
            modelBuilder.Entity<Prontuario>(entity =>
            {
                // A relação com Agendamento já foi definida a partir de Agendamento.
                // entity.HasOne(p => p.Agendamento)
                //       .WithOne(a => a.Prontuario)
                //       .HasForeignKey<Prontuario>(p => p.IdAgendamento)
                //       .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Veterinario)
                      .WithMany(v => v.Prontuarios)
                      .HasForeignKey(p => p.IdVeterinario)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Paciente)
                      .WithMany(pa => pa.Prontuarios)
                      .HasForeignKey(p => p.IdPaciente)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Chave primária para Usuario é Login (string)
            modelBuilder.Entity<Usuario>()
               .HasKey(u => u.Login);
        }
    }
}