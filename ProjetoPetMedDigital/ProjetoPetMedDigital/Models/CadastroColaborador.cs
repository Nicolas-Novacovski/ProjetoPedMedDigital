using ProjetoPetMedDigital.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("CadastroColaborador")]
    public class CadastroColaborador : BaseModel
    {
        [Key]
        public int IdColaborador { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; } = null!;

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
        [StringLength(15, ErrorMessage = "RG não pode exceder 15 caracteres.")]
        [Display(Name = "RG")]
        public string RG { get; set; } = null!;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime Dtnascimento { get; set; }

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

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um cargo válido.")]
        [Display(Name = "Cargo")]
        public int Cargo { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um tipo de usuário válido.")]
        [Display(Name = "Tipo de Usuário")]
        public int TipoUsuario { get; set; }


        [Required(ErrorMessage = "O Login é obrigatório.")]
        [StringLength(50, ErrorMessage = "Login não pode exceder 50 caracteres.")] // Mantido 50, se for a FK para Usuario.Login
        [Display(Name = "Login do Usuário")]
        public string Login { get; set; } = null!; // Chave estrangeira para Usuario.Login

        // Propriedades de navegação - CORRIGIDA PARA ANULÁVEL (?)
        public Usuario? Usuario { get; set; } // AGORA É ANULÁVEL
        public Veterinario? Veterinario { get; set; }
    }
}