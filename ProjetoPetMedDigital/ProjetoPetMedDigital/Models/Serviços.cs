using ProjetoPetMedDigital.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Servico")] 
    public class Servi�os : BaseModel 
    {
        [Key]
        public int IdServico { get; set; }

        [Required(ErrorMessage = "O tipo de venda � obrigat�rio.")]
        [StringLength(50, ErrorMessage = "Tipo de venda n�o pode exceder 50 caracteres.")]
        [Display(Name = "Tipo de Venda")]
        public string TipoVenda { get; set; } = null!;

        [Required(ErrorMessage = "O nome do servi�o � obrigat�rio.")]
        [StringLength(150, ErrorMessage = "Nome do servi�o n�o pode exceder 150 caracteres.")]
        [Display(Name = "Nome do Servi�o")]
        public string NomeServico { get; set; } = null!;

        [Required(ErrorMessage = "O veterin�rio � obrigat�rio.")]
        [Display(Name = "Veterin�rio")]
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "A data � obrigat�ria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Servi�o")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "A hora � obrigat�ria.")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora do Servi�o")]
        public DateTime Hora { get; set; }

        [Required(ErrorMessage = "O status � obrigat�rio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um status v�lido.")]
        [Display(Name = "Status")]
        public int Status { get; set; }

        [Required(ErrorMessage = "O pre�o de venda � obrigat�rio.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Pre�o de venda deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Pre�o de Venda")]
        public decimal PrecoVenda { get; set; }

        [StringLength(500, ErrorMessage = "A descri��o n�o pode exceder 500 caracteres.")]
        [Display(Name = "Descri��o")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O agendamento � obrigat�rio.")]
        [Display(Name = "Agendamento")]
        public int IdAgendamento { get; set; }

        [Required(ErrorMessage = "O produto/item de estoque � obrigat�rio.")]
        [Display(Name = "Produto/Item de Estoque")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O valor associado � obrigat�rio.")]
        [Display(Name = "Valor Associado")]
        public int IdValor { get; set; }

        // Propriedades de navega��o
        public Agendamento Agendamento { get; set; } = null!;
        public Veterinario Veterinario { get; set; } = null!;
        public ItemEstoque ItemEstoque { get; set; } = null!;
        public Valor Valor { get; set; } = null!;
    }
}