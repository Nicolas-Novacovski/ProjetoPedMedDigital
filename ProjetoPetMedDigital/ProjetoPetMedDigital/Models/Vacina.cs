using ProjetoPetMedDigital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Vacina")]
    public class Vacina : BaseModel
    {
        [Key]
        public int IdVacina { get; set; }

        [Required(ErrorMessage = "O nome da vacina é obrigatório.")]
        [StringLength(150, ErrorMessage = "Nome da vacina não pode exceder 150 caracteres.")]
        [Display(Name = "Nome da Vacina")]
        public string NomeVacina { get; set; } = null!;

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A duração da vacina é obrigatória.")]
        [StringLength(50, ErrorMessage = "Duração não pode exceder 50 caracteres.")]
        [Display(Name = "Duração")]
        public string Duracao { get; set; } = null!;

        [Required(ErrorMessage = "O produto associado é obrigatório.")]
        [Display(Name = "ID do Produto (Estoque)")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O paciente é obrigatório.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }

        // Propriedades de navegação
        public ItemEstoque ItemEstoque { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!;
    }
}