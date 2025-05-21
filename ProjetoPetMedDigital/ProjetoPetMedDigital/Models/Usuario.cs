using PetMed_Digital.Models; // Adicionado para BaseModel
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Usuario")]
    public class Usuario : BaseModel // Se Usuario tamb�m tiver CreatedAt e Id (int) como PK, sen�o remova : BaseModel
    {
        [Key] // Login � a chave prim�ria
        public string Login { get; set; }
        public string Senha { get; set; } // Considerar armazenar hash da senha
        public int IdColaborador { get; set; } // Chave estrangeira para CadastroColaborador.IdColaborador

        // Propriedades de navega��o
        public CadastroColaborador CadastroColaborador { get; set; }


}