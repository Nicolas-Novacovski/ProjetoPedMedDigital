using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Usuario")]
    public class Usuario : BaseModel
    {
        [Key]
        public string Login { get; set; }
        public string Senha { get; set; }
        public int IdColaborador { get; set; } // Chave estrangeira para CadastroColaborador

        // Propriedades de navegação
        public CadastroColaborador CadastroColaborador { get; set; }
    }
}