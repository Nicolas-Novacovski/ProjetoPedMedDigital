using ProjetoPetMedDigital.Models; // Assumindo que BaseModel est� neste namespace
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models // Assumindo este namespace principal
{
    [Table("Vacina")]
    public class Vacina : BaseModel
    {
        [Key]
        public int IdVacina { get; set; }

        [Required(ErrorMessage = "O nome da vacina � obrigat�rio.")]
        [StringLength(150, ErrorMessage = "Nome da vacina n�o pode exceder 150 caracteres.")]
        [Display(Name = "Nome da Vacina")]
        public string NomeVacina { get; set; } = null!;

        [StringLength(500, ErrorMessage = "A descri��o n�o pode exceder 500 caracteres.")]
        [Display(Name = "Descri��o")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A dura��o da vacina � obrigat�ria.")]
        [StringLength(50, ErrorMessage = "Dura��o n�o pode exceder 50 caracteres.")]
        [Display(Name = "Dura��o")]
        public string Duracao { get; set; } = null!;

        [Required(ErrorMessage = "O produto associado � obrigat�rio.")]
        [Display(Name = "ID do Produto (Estoque)")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O paciente � obrigat�rio.")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }

        // Propriedades de navega��o - CORRIGIDAS PARA ANUL�VEIS (?)
        public ItemEstoque? ItemEstoque { get; set; } // Tornada anul�vel
        public Paciente? Paciente { get; set; }       // Tornada anul�vel
    }
}