using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetMed_Digital.Models;
using System.Collections.Generic;

namespace ProjetoPetMedDigital.Models
{
    [Table("Agendamento")]
    public class Agendamento : BaseModel
    {
        [Key]
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
        public Paciente Paciente { get; set; } = null!;
        public Veterinario Veterinario { get; set; } = null!;

        public List<Servi�os> Servicos { get; set; } = new List<Servi�os>();
        public Prontuario? Prontuario { get; set; }
    }
}