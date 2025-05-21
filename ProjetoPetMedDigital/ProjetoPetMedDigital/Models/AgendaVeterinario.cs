using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetMed_Digital.Models;

namespace ProjetoPetMedDigital.Models
{
    [Table("AgendaVeterinario")]
    public class AgendaVeterinario : BaseModel
    {
        [Key]
        public int IdAgenda { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int IdPaciente { get; set; }

        // Propriedades de navega��o
        public Veterinario Veterinario { get; set; } = null!; // EF Core preencher�
        public Paciente Paciente { get; set; } = null!; // EF Core preencher�
    }
}