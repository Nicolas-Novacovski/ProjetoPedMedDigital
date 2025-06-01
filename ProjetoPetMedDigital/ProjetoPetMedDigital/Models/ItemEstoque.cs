using PetMed_Digital.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("ItemEstoque")]
    public class ItemEstoque : BaseModel
    {
        [Key]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(150, ErrorMessage = "Nome do produto não pode exceder 150 caracteres.")]
        [Display(Name = "Nome do Produto")]
        public string NomeProduto { get; set; } = null!;

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Quantidade")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser um número positivo.")]
        public int? Quantidade { get; set; }

        [Display(Name = "Preço de Custo")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Preço de custo deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        public decimal? PrecoCusto { get; set; }

        [Display(Name = "Preço de Venda")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Preço de venda deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        public decimal? PrecoVenda { get; set; }

        [Required(ErrorMessage = "A unidade de medida é obrigatória.")]
        [StringLength(50, ErrorMessage = "Unidade de medida não pode exceder 50 caracteres.")]
        [Display(Name = "Unidade de Medida")]
        public string UnidadeMedida { get; set; } = null!;

        [DataType(DataType.Date)]
        [Display(Name = "Data de Validade")]
        public DateTime? DataValidade { get; set; }

        [Required(ErrorMessage = "O fornecedor é obrigatório.")]
        [StringLength(100, ErrorMessage = "Fornecedor não pode exceder 100 caracteres.")]
        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; } = null!;

        [Display(Name = "Transação Desejada")]
        public int? TransacaoDesejada { get; set; }

        // Propriedades de navegação (One-to-One com chave compartilhada)
        public Vacina? Vacina { get; set; }
        public Procedimento? Procedimento { get; set; }
        public Serviços? Servico { get; set; }
    }
}