using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjetoPetMedDigital.Models; // Assumindo que BaseModel est� neste namespace
using System.Collections.Generic;

namespace ProjetoPetMedDigital.Models // Assumindo este namespace principal
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

        // Propriedades de navega��o - CORRIGIDAS PARA ANUL�VEIS (?) se for preciso, ou ajustadas
        public Paciente? Paciente { get; set; }     // Tornada anul�vel
        public Veterinario? Veterinario { get; set; } // Tornada anul�vel

        // CORRE��O: Nome da classe 'servico' para 'servico' (singular) para consist�ncia.
        // Se a classe 'servico.cs' no seu projeto ainda se chama 'servico', este ser� um erro de compila��o.
        // Voc� deve renomear a classe 'servico' para 'servico'.
        public List<servico> servico { get; set; } = new List<servico>();
        public Prontuario? Prontuario { get; set; }
    }
}