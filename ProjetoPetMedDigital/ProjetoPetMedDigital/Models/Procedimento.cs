// Removida: using ProjetoPetMedDigital.Models; - pois BaseModel estará em ProjetoPetMedDigital.Models
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

        [Required(ErrorMessage = "O nome do procedimento é obrigatório.")]
        [StringLength(150, ErrorMessage = "Nome do procedimento não pode exceder 150 caracteres.")]
        [Display(Name = "Nome do Procedimento")]
        public string NomeProcedimento { get; set; } = null!;

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor do procedimento é obrigatório.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "O valor do procedimento deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O produto associado é obrigatório.")]
        [Display(Name = "ID do Produto")]
        public int IdProduto { get; set; }

        // Propriedades de navegação
        public ItemEstoque? ItemEstoque { get; set; }
    }
}