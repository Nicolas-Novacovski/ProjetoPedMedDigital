using PetMed_Digital.Models;
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
        public int? IdAgendamento { get; set; }
        public int? IdVeterinario { get; set; }
        public DateTime DataConsulta { get; set; }
        public string MotivoConsulta { get; set; } = string.Empty;
        public string Diagnostico { get; set; } = string.Empty;
        public string Tratamento { get; set; } = string.Empty;
        public float? Peso { get; set; }
        public int? Temperatura { get; set; }
        public int? FrequenciaCardiaca { get; set; }
        public int? FrequenciaRespiratoria { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public int IdPaciente { get; set; }

        // Propriedades de navegação
        public Agendamento? Agendamento { get; set; } // FK IdAgendamento é anulável
        public Veterinario? Veterinario { get; set; } // FK IdVeterinario é anulável
        public Paciente Paciente { get; set; } = null!; // FK IdPaciente não é anulável
    }
}