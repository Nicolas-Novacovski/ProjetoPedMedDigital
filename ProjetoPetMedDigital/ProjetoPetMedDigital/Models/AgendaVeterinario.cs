using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Removida: using PetMed_Digital.Models; - pois BaseModel estar� em ProjetoPetMedDigital.Models

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("AgendaVeterinario")]
    public class AgendaVeterinario : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAgendaVeterinario { get; set; }

        [Required(ErrorMessage = "O veterin�rio � obrigat�rio.")]
        [Display(Name = "Veterin�rio")]
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "A data de in�cio da agenda � obrigat�ria.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "In�cio da Agenda")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim da agenda � obrigat�ria.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fim da Agenda")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O paciente � obrigat�rio.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }

        // Propriedades de navega��o
        public Veterinario? Veterinario { get; set; }
        public Paciente? Paciente { get; set; }
    }
}