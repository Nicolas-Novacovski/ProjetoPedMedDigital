using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Valor")]
    public class Valor : BaseModel
    {
        [Key]
        public int IdValor { get; set; }

        [Required(ErrorMessage = "O valor do procedimento � obrigat�rio.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Valor do procedimento deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor do Procedimento")]
        public decimal ValorProcedimento { get; set; }

        [Required(ErrorMessage = "O tipo de pagamento � obrigat�rio.")]
        [StringLength(50, ErrorMessage = "Tipo de pagamento n�o pode exceder 50 caracteres.")]
        [Display(Name = "Tipo de Pagamento")]
        public string TipoPagamento { get; set; } = null!;

        [Required(ErrorMessage = "O valor da receita � obrigat�rio.")]
        [Range(0.00, (double)decimal.MaxValue, ErrorMessage = "Valor da receita inv�lido.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor da Receita")]
        public decimal ValorReceita { get; set; }

        [Required(ErrorMessage = "O valor de sa�da � obrigat�rio.")]
        [Range(0.00, (double)decimal.MaxValue, ErrorMessage = "Valor de sa�da inv�lido.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor de Sa�da")]
        public decimal ValorSaida { get; set; }

        [Required(ErrorMessage = "O sal�rio � obrigat�rio.")]
        [Range(0.00, (double)decimal.MaxValue, ErrorMessage = "Sal�rio inv�lido.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Sal�rio")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "O valor de compra de produtos � obrigat�rio.")]
        [Range(0.00, (double)decimal.MaxValue, ErrorMessage = "Valor de compra de produtos inv�lido.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Compra de Produtos")]
        public decimal CompraProdutos { get; set; }

        [Required(ErrorMessage = "O cliente � obrigat�rio.")]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        // Propriedades de navega��o
        public Servi�os Servico { get; set; } = null!;
        public Cliente Cliente { get; set; } = null!;
    }
}