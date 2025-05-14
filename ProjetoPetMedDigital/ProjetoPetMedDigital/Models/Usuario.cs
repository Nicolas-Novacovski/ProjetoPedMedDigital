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
        public int IdColaborador { get; set; }
    }
}