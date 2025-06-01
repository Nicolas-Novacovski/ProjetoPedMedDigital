using PetMed_Digital.Models; // Assumindo que BaseModel est� neste namespace
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models // Assumindo este namespace principal
{
    [Table("Prontuario")]
    public class Prontuario : BaseModel
    {
        [Key]
        public int IdProntuario { get; set; }

        [Display(Name = "Agendamento (Opcional)")]
        public int? IdAgendamento { get; set; } // FK anul�vel

        [Display(Name = "Veterin�rio (Opcional)")]
        public int? IdVeterinario { get; set; } // FK anul�vel

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

        // Propriedades de navega��o - VERIFICADO / AJUSTADO PARA ANUL�VEL SE NECESS�RIO
        public Agendamento? Agendamento { get; set; }
        public Veterinario? Veterinario { get; set; }
        public Paciente? Paciente { get; set; } // Tornada anul�vel se n�o estava para evitar erro de ModelState
    }
}