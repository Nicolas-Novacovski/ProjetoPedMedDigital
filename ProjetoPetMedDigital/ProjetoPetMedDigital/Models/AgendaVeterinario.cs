using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Removida: using PetMed_Digital.Models; - pois BaseModel estará em ProjetoPetMedDigital.Models

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("AgendaVeterinario")]
    public class AgendaVeterinario : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        // Propriedades de navegação
        public Veterinario? Veterinario { get; set; }
        public Paciente? Paciente { get; set; }
    }
}