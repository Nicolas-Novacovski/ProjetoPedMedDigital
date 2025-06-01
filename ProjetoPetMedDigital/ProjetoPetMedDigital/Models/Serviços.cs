using ProjetoPetMedDigital.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Servico")] 
    public class Serviços : BaseModel 
    {
        [Key]
        public int IdServico { get; set; }

        [Required(ErrorMessage = "O tipo de venda é obrigatório.")]
        [StringLength(50, ErrorMessage = "Tipo de venda não pode exceder 50 caracteres.")]
        [Display(Name = "Tipo de Venda")]
        public string TipoVenda { get; set; } = null!;

        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        [StringLength(150, ErrorMessage = "Nome do serviço não pode exceder 150 caracteres.")]
        [Display(Name = "Nome do Serviço")]
        public string NomeServico { get; set; } = null!;

        [Required(ErrorMessage = "O veterinário é obrigatório.")]
        [Display(Name = "Veterinário")]
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Serviço")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "A hora é obrigatória.")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora do Serviço")]
        public DateTime Hora { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um status válido.")]
        [Display(Name = "Status")]
        public int Status { get; set; }

        [Required(ErrorMessage = "O preço de venda é obrigatório.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Preço de venda deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço de Venda")]
        public decimal PrecoVenda { get; set; }

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O agendamento é obrigatório.")]
        [Display(Name = "Agendamento")]
        public int IdAgendamento { get; set; }

        [Required(ErrorMessage = "O produto/item de estoque é obrigatório.")]
        [Display(Name = "Produto/Item de Estoque")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O valor associado é obrigatório.")]
        [Display(Name = "Valor Associado")]
        public int IdValor { get; set; }

        // Propriedades de navegação
        public Agendamento Agendamento { get; set; } = null!;
        public Veterinario Veterinario { get; set; } = null!;
        public ItemEstoque ItemEstoque { get; set; } = null!;
        public Valor Valor { get; set; } = null!;
    }
}