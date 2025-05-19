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
        public string NomeVacina { get; set; }
        public string Descricao { get; set; }
        public string Duracao { get; set; }
        public int IdProduto { get; set; } // Chave estrangeira para ItemEstoque
        public int IdPaciente { get; set; } // Chave estrangeira para Paciente

        // Propriedades de navegação
        public ItemEstoque ItemEstoque { get; set; }
        public Paciente Paciente { get; set; }
    }
}