using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Vacina")]
    public class Vacina : BaseModel
    {
        [Key]
        public int IdVacina { get; set; }
        public string NomeVacina { get; set; }
        public string Descricao { get; set; }
        public string Duracao { get; set; }
        public int IdPaciente { get; set; }
    }
}