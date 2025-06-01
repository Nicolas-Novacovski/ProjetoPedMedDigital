using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetMed_Digital.Models; // Para BaseModel

namespace ProjetoPetMedDigital.Models
{
    [Table("AgendaVeterinario")]
    public class AgendaVeterinario : BaseModel
    {
        [Key]
        public int IdAgendaVeterinario { get; set; }

        [Required(ErrorMessage = "O veterin�rio � obrigat�rio.")]
        [Display(Name = "Veterin�rio")]
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "A data de in�cio da agenda � obrigat�ria.")]
        [DataType(DataType.DateTime)] // Ou DataType.Date se for apenas data
        [Display(Name = "In�cio da Agenda")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim da agenda � obrigat�ria.")]
        [DataType(DataType.DateTime)] // Ou DataType.Date se for apenas data
        [Display(Name = "Fim da Agenda")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O paciente � obrigat�rio.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; } 

        // Propriedades de navega��o
        public Veterinario Veterinario { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!;
    }
}