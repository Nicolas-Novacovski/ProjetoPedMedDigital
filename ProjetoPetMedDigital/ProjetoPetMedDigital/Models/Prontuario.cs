// Removida: using PetMed_Digital.Models; - pois BaseModel estará em ProjetoPetMedDigital.Models
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("Prontuario")]
    public class Prontuario : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProntuario { get; set; }

        [Display(Name = "Agendamento (Opcional)")]
        public int? IdAgendamento { get; set; }

        [Display(Name = "Veterinário (Opcional)")]
        public int? IdVeterinario { get; set; }

        [Required(ErrorMessage = "A data da consulta é obrigatória.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data da Consulta")]
        public DateTime DataConsulta { get; set; }

        [Required(ErrorMessage = "O motivo da consulta é obrigatório.")]
        [StringLength(500, ErrorMessage = "Motivo da consulta não pode exceder 500 caracteres.")]
        [Display(Name = "Motivo da Consulta")]
        public string MotivoConsulta { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "O diagnóstico não pode exceder 1000 caracteres.")]
        [Display(Name = "Diagnóstico")]
        public string Diagnostico { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "O tratamento não pode exceder 1000 caracteres.")]
        [Display(Name = "Tratamento")]
        public string Tratamento { get; set; } = string.Empty;

        [Range(0.0, 200.0, ErrorMessage = "Peso deve ser entre 0 e 200 kg.")]
        [Display(Name = "Peso (kg)")]
        public float? Peso { get; set; }

        [Range(0, 50, ErrorMessage = "Temperatura inválida.")]
        [Display(Name = "Temperatura (°C)")]
        public int? Temperatura { get; set; }

        [Range(0, 300, ErrorMessage = "Frequência cardíaca inválida.")]
        [Display(Name = "Frequência Cardíaca")]
        public int? FrequenciaCardiaca { get; set; }

        [Range(0, 100, ErrorMessage = "Frequência respiratória inválida.")]
        [Display(Name = "Frequência Respiratória")]
        public int? FrequenciaRespiratoria { get; set; }

        [StringLength(1000, ErrorMessage = "Observações não podem exceder 1000 caracteres.")]
        [Display(Name = "Observações")]
        public string Observacoes { get; set; } = string.Empty;

        [Required(ErrorMessage = "O paciente é obrigatório.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }

        // Propriedades de navegação
        public Agendamento? Agendamento { get; set; }
        public Veterinario? Veterinario { get; set; }
        public Paciente? Paciente { get; set; }
    }
}