using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Agendamento")]
    public class Agendamento : PetMed_Digital.Models.BaseModel
    {
        [Key]
        public int IdAgendamento { get; set; }
        public int IdPaciente { get; set; }
        public int IdVeterinario { get; set; }
        public int IdServico { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime HoraAgendamento { get; set; }
        public string Observacoes { get; set; }
        public int IdProntuario { get; set; } // Parece redundante, pois Prontuario tem FK para Agendamento

        // Propriedades de navegação
        public Paciente Paciente { get; set; }
        public Veterinario Veterinario { get; set; }
        public Servico Servico { get; set; }
        public Prontuario Prontuario { get; set; }
    }
}