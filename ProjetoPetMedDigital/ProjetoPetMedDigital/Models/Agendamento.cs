using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Removida: using ProjetoPetMedDigital.Models; - pois BaseModel estará em ProjetoPetMedDigital.Models
using System.Collections.Generic;

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("Agendamento")]
    public class Agendamento : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAgendamento { get; set; }

        [Required(ErrorMessage = "O paciente é obrigatório.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "O veterinário é obrigatório.")]
        [Display(Name = "Veterinário")]
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "A data do agendamento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Agendamento")]
        public DateTime DataAgendamento { get; set; }

        [Required(ErrorMessage = "A hora do agendamento é obrigatória.")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora do Agendamento")]
        public DateTime HoraAgendamento { get; set; }

        [StringLength(500, ErrorMessage = "Observações não podem exceder 500 caracteres.")]
        [Display(Name = "Observações")]
        public string Observacoes { get; set; } = string.Empty;

        // Propriedades de navegação
        public Paciente? Paciente { get; set; }
        public Veterinario? Veterinario { get; set; }
        public List<Servico> Servico { get; set; } = new List<Servico>(); // Referência a Servico (singular)
        public Prontuario? Prontuario { get; set; }
    }
}