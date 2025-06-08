using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Removida: using ProjetoPetMedDigital.Models; - pois BaseModel estar� em ProjetoPetMedDigital.Models
using System.Collections.Generic;

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("Agendamento")]
    public class Agendamento : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAgendamento { get; set; }

        [Required(ErrorMessage = "O paciente � obrigat�rio.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "O veterin�rio � obrigat�rio.")]
        [Display(Name = "Veterin�rio")]
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "A data do agendamento � obrigat�ria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Agendamento")]
        public DateTime DataAgendamento { get; set; }

        [Required(ErrorMessage = "A hora do agendamento � obrigat�ria.")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora do Agendamento")]
        public DateTime HoraAgendamento { get; set; }

        [StringLength(500, ErrorMessage = "Observa��es n�o podem exceder 500 caracteres.")]
        [Display(Name = "Observa��es")]
        public string Observacoes { get; set; } = string.Empty;

        // Propriedades de navega��o
        public Paciente? Paciente { get; set; }
        public Veterinario? Veterinario { get; set; }
        public List<Servico> Servico { get; set; } = new List<Servico>(); // Refer�ncia a Servico (singular)
        public Prontuario? Prontuario { get; set; }
    }
}