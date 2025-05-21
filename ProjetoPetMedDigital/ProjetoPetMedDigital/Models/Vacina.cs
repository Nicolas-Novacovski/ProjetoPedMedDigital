using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Vacina")]
    public class Vacina : BaseModel
    {
        [Key]
        public int IdVacina { get; set; }
        public string NomeVacina { get; set; } = null!;
        public string Descricao { get; set; } = string.Empty;
        public string Duracao { get; set; } = null!;
        public int IdProduto { get; set; }
        public int IdPaciente { get; set; }

        // Propriedades de navegação
        public ItemEstoque ItemEstoque { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!;
    }
}