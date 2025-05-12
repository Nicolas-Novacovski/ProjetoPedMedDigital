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

        // Chave estrangeira para o Paciente
        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } // Navegação para a tabela Paciente

        public ICollection<Veterinario> Veterinarios { get; set; }
        public ICollection<ItemEstoque> ItensEstoque { get; set; }
    }
}
