using PetMed_Digital.Models; // Adicionado para BaseModel
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Usuario")]
    public class Usuario : BaseModel // Se Usuario também tiver CreatedAt e Id (int) como PK, senão remova : BaseModel
    {
        [Key] // Login é a chave primária
        public string Login { get; set; }
        public string Senha { get; set; } // Considerar armazenar hash da senha
        public int IdColaborador { get; set; } // Chave estrangeira para CadastroColaborador.IdColaborador

        // Propriedades de navegação
        public CadastroColaborador CadastroColaborador { get; set; }


}