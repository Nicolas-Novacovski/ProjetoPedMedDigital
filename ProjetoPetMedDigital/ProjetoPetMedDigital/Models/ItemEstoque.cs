// Removida: using ProjetoPetMedDigital.Models; - pois BaseModel estar� em ProjetoPetMedDigital.Models
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("ItemEstoque")]
    public class ItemEstoque : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O nome do produto � obrigat�rio.")]
        [StringLength(150, ErrorMessage = "Nome do produto n�o pode exceder 150 caracteres.")]
        [Display(Name = "Nome do Produto")]
        public string NomeProduto { get; set; } = null!;

        [StringLength(500, ErrorMessage = "A descri��o n�o pode exceder 500 caracteres.")]
        [Display(Name = "Descri��o")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Quantidade")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser um n�mero positivo.")]
        public int? Quantidade { get; set; }

        [Display(Name = "Pre�o de Custo")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Pre�o de custo deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        public decimal? PrecoCusto { get; set; }

        [Display(Name = "Pre�o de Venda")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Pre�o de venda deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        public decimal? PrecoVenda { get; set; }

        [Required(ErrorMessage = "A unidade de medida � obrigat�ria.")]
        [StringLength(50, ErrorMessage = "Unidade de medida n�o pode exceder 50 caracteres.")]
        [Display(Name = "Unidade de Medida")]
        public string UnidadeMedida { get; set; } = null!;

        [DataType(DataType.Date)]
        [Display(Name = "Data de Validade")]
        public DateTime? DataValidade { get; set; }

        [Required(ErrorMessage = "O fornecedor � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "Fornecedor n�o pode exceder 100 caracteres.")]
        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; } = null!;

        [Display(Name = "Transacao Desejada")]
        public int? TransacaoDesejada { get; set; }

        // Propriedades de navega��o
        public Vacina? Vacina { get; set; }
        public Procedimento? Procedimento { get; set; }
        public Servico? Servico { get; set; } // Refer�ncia a Servico (singular)
    }
}