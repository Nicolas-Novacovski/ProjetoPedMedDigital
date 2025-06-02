// Removida: using PetMed_Digital.Models; - pois BaseModel estar� em ProjetoPetMedDigital.Models
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

        [Display(Name = "Veterin�rio (Opcional)")]
        public int? IdVeterinario { get; set; }

        [Required(ErrorMessage = "A data da consulta � obrigat�ria.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data da Consulta")]
        public DateTime DataConsulta { get; set; }

        [Required(ErrorMessage = "O motivo da consulta � obrigat�rio.")]
        [StringLength(500, ErrorMessage = "Motivo da consulta n�o pode exceder 500 caracteres.")]
        [Display(Name = "Motivo da Consulta")]
        public string MotivoConsulta { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "O diagn�stico n�o pode exceder 1000 caracteres.")]
        [Display(Name = "Diagn�stico")]
        public string Diagnostico { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "O tratamento n�o pode exceder 1000 caracteres.")]
        [Display(Name = "Tratamento")]
        public string Tratamento { get; set; } = string.Empty;

        [Range(0.0, 200.0, ErrorMessage = "Peso deve ser entre 0 e 200 kg.")]
        [Display(Name = "Peso (kg)")]
        public float? Peso { get; set; }

        [Range(0, 50, ErrorMessage = "Temperatura inv�lida.")]
        [Display(Name = "Temperatura (�C)")]
        public int? Temperatura { get; set; }

        [Range(0, 300, ErrorMessage = "Frequ�ncia card�aca inv�lida.")]
        [Display(Name = "Frequ�ncia Card�aca")]
        public int? FrequenciaCardiaca { get; set; }

        [Range(0, 100, ErrorMessage = "Frequ�ncia respirat�ria inv�lida.")]
        [Display(Name = "Frequ�ncia Respirat�ria")]
        public int? FrequenciaRespiratoria { get; set; }

        [StringLength(1000, ErrorMessage = "Observa��es n�o podem exceder 1000 caracteres.")]
        [Display(Name = "Observa��es")]
        public string Observacoes { get; set; } = string.Empty;

        [Required(ErrorMessage = "O paciente � obrigat�rio.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }

        // Propriedades de navega��o
        public Agendamento? Agendamento { get; set; }
        public Veterinario? Veterinario { get; set; }
        public Paciente? Paciente { get; set; }
    }
}