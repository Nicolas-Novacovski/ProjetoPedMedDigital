using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjetoPetMedDigital.Models;
using ProjetoPetMedDigital.Models;

namespace ProjetoPetMedDigital.Models
{
    [Table("AgendaVeterinario")]
    public class AgendaVeterinario : BaseModel
    {
        [Key]
        public int IdAgendaVeterinario { get; set; }

        [Required(ErrorMessage = "O veterinário é obrigatório.")]
        [Display(Name = "Veterinário")]
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "A data de início da agenda é obrigatória.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Início da Agenda")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim da agenda é obrigatória.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fim da Agenda")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O paciente é obrigatório.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }

        // Propriedades de navegação - CORRIGIDAS PARA ANULÁVEIS (?)
        public Veterinario? Veterinario { get; set; } // Agora anulável
        public Paciente? Paciente { get; set; }       // Agora anulável
    }
}