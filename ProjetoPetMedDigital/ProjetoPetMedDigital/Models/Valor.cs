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

        [Required(ErrorMessage = "O valor do procedimento é obrigatório.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Valor do procedimento deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor do Procedimento")]
        public decimal ValorProcedimento { get; set; }

        [Required(ErrorMessage = "O tipo de pagamento é obrigatório.")]
        [StringLength(50, ErrorMessage = "Tipo de pagamento não pode exceder 50 caracteres.")]
        [Display(Name = "Tipo de Pagamento")]
        public string TipoPagamento { get; set; } = null!;

        [Required(ErrorMessage = "O valor da receita é obrigatório.")]
        [Range(0.00, (double)decimal.MaxValue, ErrorMessage = "Valor da receita inválido.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor da Receita")]
        public decimal ValorReceita { get; set; }

        [Required(ErrorMessage = "O valor de saída é obrigatório.")]
        [Range(0.00, (double)decimal.MaxValue, ErrorMessage = "Valor de saída inválido.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor de Saída")]
        public decimal ValorSaida { get; set; }

        [Required(ErrorMessage = "O salário é obrigatório.")]
        [Range(0.00, (double)decimal.MaxValue, ErrorMessage = "Salário inválido.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Salário")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "O valor de compra de produtos é obrigatório.")]
        [Range(0.00, (double)decimal.MaxValue, ErrorMessage = "Valor de compra de produtos inválido.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Compra de Produtos")]
        public decimal CompraProdutos { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatório.")]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        // Propriedades de navegação
        public Serviços Servico { get; set; } = null!;
        public Cliente Cliente { get; set; } = null!;
    }
}