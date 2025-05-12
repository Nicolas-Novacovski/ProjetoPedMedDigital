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

        // Chave estrangeira para o CadastroColaborador
        [ForeignKey("CadastroColaborador")]
        public int IdColaborador { get; set; }
        public CadastroColaborador CadastroColaborador { get; set; } // Navegação para a tabela CadastroColaborador
    }
}
