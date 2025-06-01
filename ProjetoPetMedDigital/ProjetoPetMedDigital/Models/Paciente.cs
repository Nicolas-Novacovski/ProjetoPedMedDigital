using PetMed_Digital.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Paciente")]
    public class Paciente : BaseModel
    {
        [Key]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "O cliente responsável é obrigatório.")]
        [Display(Name = "Cliente Responsável")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "O nome do animal é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do animal não pode exceder 100 caracteres.")]
        [Display(Name = "Nome do Animal")]
        public string NomeCachorro { get; set; } = null!;

        [Required(ErrorMessage = "O estado do animal é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um estado válido para o animal.")]
        [Display(Name = "Estado do Animal")]
        public int Estado { get; set; }

        [StringLength(500, ErrorMessage = "O problema não pode exceder 500 caracteres.")]
        [Display(Name = "Problema Principal")]
        public string Problema { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de atendimento é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo de atendimento válido.")]
        [Display(Name = "Tipo de Atendimento")]
        public int TipoAtendimento { get; set; }

        [Required(ErrorMessage = "O peso é obrigatório.")]
        [Range(0.01, 200.0, ErrorMessage = "O peso deve ser um valor positivo entre 0.01 e 200 kg.")]
        [Display(Name = "Peso (kg)")]
        public float Peso { get; set; }

        [StringLength(500, ErrorMessage = "Sinais vitais não podem exceder 500 caracteres.")]
        [Display(Name = "Sinais Vitais")]
        public string SinaisVitais { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Recomendações não podem exceder 500 caracteres.")]
        [Display(Name = "Recomendações")]
        public string Recomendacoes { get; set; } = string.Empty;

        // Propriedades de navegação
        public Cliente Cliente { get; set; } = null!;
        public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
        public List<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();
        public List<Vacina> Vacinas { get; set; } = new List<Vacina>();
    }
}