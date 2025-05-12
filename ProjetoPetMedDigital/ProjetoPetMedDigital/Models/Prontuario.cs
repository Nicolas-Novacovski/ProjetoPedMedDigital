using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Prontuario")]
    public class Prontuario : BaseModel
    {
        [Key]
        public int IdProntuario { get; set; }

        // Chave estrangeira para o Agendamento
        [ForeignKey("Agendamento")]
        public int IdAgendamento { get; set; }
        public Agendamento Agendamento { get; set; } // Navegação para a tabela Agendamento

        // Chave estrangeira para o Veterinario
        [ForeignKey("Veterinario")]
        public int IdVeterinario { get; set; }
        public Veterinario Veterinario { get; set; } // Navegação para a tabela Veterinario

        public DateTime DataConsulta { get; set; }
        public string MotivoConsulta { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamento { get; set; }
        public float Peso { get; set; }
        public int Temperatura { get; set; }
        public int FrequenciaCardiaca { get; set; }
        public int FrequenciaRespiatoria { get; set; }
        public string Observacoes { get; set; }

        // Chave estrangeira para o Paciente
        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } // Navegação para a tabela Paciente
    }
}
