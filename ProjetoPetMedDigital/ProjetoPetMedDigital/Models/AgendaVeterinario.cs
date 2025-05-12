using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("AgendaVeterinario")]
    public class AgendaVeterinario : BaseModel
    {
        [Key]
        public int IdAgenda { get; set; }

        // Chave estrangeira para o Veterinario
        [ForeignKey("Veterinario")]
        public int IdVeterinario { get; set; }
        public Veterinario Veterinario { get; set; } // Navegação para a tabela Veterinario

        // Chave estrangeira para o Paciente
        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } // Navegação para a tabela Paciente

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
