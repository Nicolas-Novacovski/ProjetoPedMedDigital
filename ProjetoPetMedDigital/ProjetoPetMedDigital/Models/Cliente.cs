// Removida: using ProjetoPetMedDigital.Models; - pois BaseModel estará em ProjetoPetMedDigital.Models
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("Cliente")]
    public class Cliente : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "O nome do responsável é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        [Display(Name = "Nome do Responsável")]
        public string NomeResponsavel { get; set; } = null!;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF deve ter entre 11 e 14 caracteres (com/sem pontuação).")]
        [Display(Name = "CPF")]
        public string CPF { get; set; } = null!;

        [Required(ErrorMessage = "O RG é obrigatório.")]
        [StringLength(20, ErrorMessage = "RG não pode ter mais de 20 caracteres.")]
        [Display(Name = "RG")]
        public string RG { get; set; } = null!;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DtNascimento { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "CEP deve ter entre 8 e 9 caracteres (com/sem hífen).")]
        [Display(Name = "CEP")]
        public string CEP { get; set; } = null!;

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(200, ErrorMessage = "O endereço não pode exceder 200 caracteres.")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; } = null!;

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O bairro não pode exceder 100 caracteres.")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; } = null!;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade não pode exceder 100 caracteres.")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; } = null!;

        // Propriedades de navegação
        public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
        public List<Valor> Valores { get; set; } = new List<Valor>();
    }
}