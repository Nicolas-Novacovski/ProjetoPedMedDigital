using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Usuario")]
    public class Usuario : BaseModel
    {
        [Key]
        [Required(ErrorMessage = "O Login é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Login deve ter entre 3 e 50 caracteres.")]
        [Display(Name = "Login do Usuário")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [DataType(DataType.Password)] // Para ocultar a senha em formulários
        [Display(Name = "Senha")]
        public string Senha { get; set; } = null!;

        [Required(ErrorMessage = "O ID do Colaborador é obrigatório.")]
        [Display(Name = "ID Colaborador")]
        public int IdColaborador { get; set; }

        // Propriedades de navegação
        public CadastroColaborador CadastroColaborador { get; set; } = null!;
    }
}