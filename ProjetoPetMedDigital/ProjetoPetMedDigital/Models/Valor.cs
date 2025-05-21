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
        public decimal ValorProcedimento { get; set; }
        public string TipoPagamento { get; set; } = null!;
        public decimal ValorReceita { get; set; }
        public decimal ValorSaida { get; set; }
        public decimal Salario { get; set; }
        public decimal CompraProdutos { get; set; }
        public int IdCliente { get; set; }

        // Propriedades de navegação
        public Servico Servico { get; set; } = null!; // Assumindo que a relação com Servico é obrigatória de um lado
        public Cliente Cliente { get; set; } = null!;
    }
}