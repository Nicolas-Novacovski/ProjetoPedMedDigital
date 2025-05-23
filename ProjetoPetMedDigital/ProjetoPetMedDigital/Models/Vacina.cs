using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Vacina")]
    public class Vacina : BaseModel
    {
        [Key]
        public int IdVacina { get; set; } // PK da tabela Vacina
        public string NomeVacina { get; set; } = null!;
        public string Descricao { get; set; } = string.Empty;
        public string Duracao { get; set; } = null!;
        public int IdProduto { get; set; } // FK para ItemEstoque.IdProduto
        public int IdPaciente { get; set; } // FK para Paciente.IdPaciente

        // Propriedades de navega��o
        public ItemEstoque ItemEstoque { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!;
    }
}