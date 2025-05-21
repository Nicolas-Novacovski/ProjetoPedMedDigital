using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Usuario")]
    public class Usuario : BaseModel // Avalie se esta herança é necessária se Login é a PK
    {
        [Key]
        public string Login { get; set; } = null!; // PK
        public string Senha { get; set; } = null!;
        public int IdColaborador { get; set; }

        // Propriedades de navegação
        public CadastroColaborador CadastroColaborador { get; set; } = null!;
    }
}