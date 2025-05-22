using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Usuario")]
    public class Usuario : BaseModel // A heran�a de BaseModel est� ok, Id ser� um campo comum
    {
        [Key]
        public string Login { get; set; } = null!; // PK da tabela Usuario
        public string Senha { get; set; } = null!;
        public int IdColaborador { get; set; } // FK para CadastroColaborador.IdColaborador

        // Propriedades de navega��o
        public CadastroColaborador CadastroColaborador { get; set; } = null!;
    }
}