using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Agendamento")]
    public class Agendamento : BaseModel
    {
        [Key]
        public int IdAgendamento { get; set; }

        // Chave estrangeira para o Paciente
        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } // Navegação para a tabela Paciente

        // Chave estrangeira para o Veterinario
        [ForeignKey("Veterinario")]
        public int IdVeterinario { get; set; }
        public Veterinario Veterinario { get; set; } // Navegação para a tabela Veterinario

        // Chave estrangeira para o Servico
        [ForeignKey("Servico")]
        public int IdServico { get; set; }
        public Servico Servico { get; set; } // Navegação para a tabela Servico

        public DateTime DataAgendamento { get; set; }
        public DateTime HoraAgendamento { get; set; }
        public string Observacoes { get; set; }

        // Chave estrangeira para o Valor
        [ForeignKey("Valor")]
        public int IdValor { get; set; }
        public Valor Valor { get; set; } // Navegação para a tabela Valor

        public ICollection<Prontuario> Prontuarios { get; set; }
    }
}
