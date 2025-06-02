using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjetoPetMedDigital.Models; // Assumindo que BaseModel está neste namespace
using System.Collections.Generic;

namespace ProjetoPetMedDigital.Models // Assumindo este namespace principal
{
    [Table("Agendamento")]
    public class Agendamento : BaseModel
    {
        [Key]
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

        // Propriedades de navegação - CORRIGIDAS PARA ANULÁVEIS (?) se for preciso, ou ajustadas
        public Paciente? Paciente { get; set; }     // Tornada anulável
        public Veterinario? Veterinario { get; set; } // Tornada anulável

        // CORREÇÃO: Nome da classe 'servico' para 'servico' (singular) para consistência.
        // Se a classe 'servico.cs' no seu projeto ainda se chama 'servico', este será um erro de compilação.
        // Você deve renomear a classe 'servico' para 'servico'.
        public List<servico> servico { get; set; } = new List<servico>();
        public Prontuario? Prontuario { get; set; }
    }
}