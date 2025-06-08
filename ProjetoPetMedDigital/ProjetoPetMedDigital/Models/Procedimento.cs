// Removida: using ProjetoPetMedDigital.Models; - pois BaseModel estar� em ProjetoPetMedDigital.Models
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("Procedimento")]
    public class Procedimento : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProcedimento { get; set; }

        [Required(ErrorMessage = "O nome do procedimento � obrigat�rio.")]
        [StringLength(150, ErrorMessage = "Nome do procedimento n�o pode exceder 150 caracteres.")]
        [Display(Name = "Nome do Procedimento")]
        public string NomeProcedimento { get; set; } = null!;

        [StringLength(500, ErrorMessage = "A descri��o n�o pode exceder 500 caracteres.")]
        [Display(Name = "Descri��o")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor do procedimento � obrigat�rio.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "O valor do procedimento deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O produto associado � obrigat�rio.")]
        [Display(Name = "ID do Produto")]
        public int IdProduto { get; set; }

        // Propriedades de navega��o
        public ItemEstoque? ItemEstoque { get; set; }
    }
}