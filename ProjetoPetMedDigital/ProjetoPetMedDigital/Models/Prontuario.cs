using PetMed_Digital.Models; // Adicionado para BaseModel
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Prontuario")]
    public class Prontuario : BaseModel
    {
        [Key]
        public int IdProntuario { get; set; }
        public int? IdAgendamento { get; set; } // FK para Agendamento (relação 1-1)
        public int? IdVeterinario { get; set; }
        public DateTime DataConsulta { get; set; }
        public string MotivoConsulta { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamento { get; set; }
        public float? Peso { get; set; }
        public int? Temperatura { get; set; } // Considerar decimal se necessário
        public int? FrequenciaCardiaca { get; set; }
        public int? FrequenciaRespiratoria { get; set; } // Corrigido: Respiatoria -> Respiratoria
        public string Observacoes { get; set; }
        public int IdPaciente { get; set; }

        // Propriedades de navegação
        public Agendamento Agendamento { get; set; }
        public Veterinario Veterinario { get; set; }
        public Paciente Paciente { get; set; }
    }
}