using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetMed_Digital.Models; // Adicionado para BaseModel

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
        public int IdPaciente { get; set; } // Se uma agenda é para um paciente específico

        // Propriedades de navegação
        public Veterinario Veterinario { get; set; }
        public Paciente Paciente { get; set; }
    }
}