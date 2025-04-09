using Microsoft.EntityFrameworkCore;
using PetMed_Digital.Models;

namespace ProjetoPetMedDigital.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<CadastroAnimal> CadastroAnimais { get; set; }
        public DbSet<CadastroDonoDoAnimal> CadastroDonosDosAnimais { get; set; }
        public DbSet<Clinica> Clinica { get; set; }
    }
}
