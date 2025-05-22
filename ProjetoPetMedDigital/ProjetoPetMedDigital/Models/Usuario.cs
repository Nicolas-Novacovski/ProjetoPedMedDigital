using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Usuario")]
    public class Usuario : BaseModel // A herança de BaseModel está ok, Id será um campo comum
    {
        [Key]
        public string Login { get; set; } = null!; // PK da tabela Usuario
        public string Senha { get; set; } = null!;
        public int IdColaborador { get; set; } // FK para CadastroColaborador.IdColaborador

        // Propriedades de navegação
        public CadastroColaborador CadastroColaborador { get; set; } = null!;
    }
}