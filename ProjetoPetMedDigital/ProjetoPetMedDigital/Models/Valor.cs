using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Valor")]
    public class Valor : BaseModel
    {
        [Key]
        public int IdValor { get; set; }
        public string ValorProcedimento { get; set; }
        public string TipoPagamento { get; set; }
        public string ValorReceita { get; set; }
        public string ValorSaida { get; set; }
        public string Salario { get; set; }
        public string CompraProdutos { get; set; }
        public int IdCliente { get; set; }
    }
}