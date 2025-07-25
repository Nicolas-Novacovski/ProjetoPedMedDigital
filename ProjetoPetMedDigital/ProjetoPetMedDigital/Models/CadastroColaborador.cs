using ProjetoPetMedDigital.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity; 

namespace ProjetoPetMedDigital.Models
{
    [Table("CadastroColaborador")]
    public class CadastroColaborador : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdColaborador { get; set; }

        [Required(ErrorMessage = "O nome � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "O nome n�o pode exceder 100 caracteres.")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O telefone � obrigat�rio.")]
        [Phone(ErrorMessage = "Formato de telefone inv�lido.")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inv�lido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O CPF � obrigat�rio.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF deve ter entre 11 e 14 caracteres (com/sem pontua��o).")]
        [Display(Name = "CPF")]
        public string CPF { get; set; } = null!;

        [Required(ErrorMessage = "O RG � obrigat�rio.")]
        [StringLength(15, ErrorMessage = "RG n�o pode exceder 15 caracteres.")]
        [Display(Name = "RG")]
        public string RG { get; set; } = null!;

        [Required(ErrorMessage = "A data de nascimento � obrigat�ria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime Dtnascimento { get; set; }

        [Required(ErrorMessage = "O CEP � obrigat�rio.")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "CEP deve ter entre 8 e 9 caracteres (com/sem h�fen).")]
        [Display(Name = "CEP")]
        public string CEP { get; set; } = null!;

        [Required(ErrorMessage = "O endere�o � obrigat�rio.")]
        [StringLength(200, ErrorMessage = "O endere�o n�o pode exceder 200 caracteres.")]
        [Display(Name = "Endere�o")]
        public string Endereco { get; set; } = null!;

        [Required(ErrorMessage = "O bairro � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "O bairro n�o pode exceder 100 caracteres.")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; } = null!;

        [Required(ErrorMessage = "A cidade � obrigat�ria.")]
        [StringLength(100, ErrorMessage = "A cidade n�o pode exceder 100 caracteres.")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; } = null!;

        [Required(ErrorMessage = "O cargo � obrigat�rio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um cargo v�lido.")]
        [Display(Name = "Cargo")]
        public int Cargo { get; set; }

        [Required(ErrorMessage = "O tipo de usu�rio � obrigat�rio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo de usu�rio v�lido.")]
        [Display(Name = "Tipo de Usu�rio")]
        public int TipoUsuario { get; set; }
        [Required(ErrorMessage = "O ID do usu�rio Identity � obrigat�rio.")]
        [Display(Name = "ID do Usu�rio (Identity)")]
        public string IdentityUserId { get; set; } = null!;

        public IdentityUser? IdentityUser { get; set; }

        public Veterinario? Veterinario { get; set; }
    }
}