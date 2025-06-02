// Removida: using PetMed_Digital.Models; - pois BaseModel estar� em ProjetoPetMedDigital.Models
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("Usuario")]
    public class Usuario : BaseModel
    {
        [Key]
        [Required(ErrorMessage = "O Login � obrigat�rio.")]
        [StringLength(450, MinimumLength = 3, ErrorMessage = "Login deve ter entre 3 e 450 caracteres.")] // Mantenha 450 para PK string
        [Display(Name = "Login do Usu�rio")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "A Senha � obrigat�ria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no m�nimo 6 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; } = null!;

        [Required(ErrorMessage = "O ID do Colaborador � obrigat�rio.")]
        [Display(Name = "ID Colaborador")]
        public int IdColaborador { get; set; }

        // Propriedades de navega��o
        public CadastroColaborador? CadastroColaborador { get; set; }
    }
}