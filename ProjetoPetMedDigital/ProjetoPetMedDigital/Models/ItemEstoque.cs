using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("ItemEstoque")]
    public class ItemEstoque : BaseModel
    {
        [Key]
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public string UnidadeMedida { get; set; }
        public DateTime DataValidade { get; set; }
        public string Fornecedor { get; set; }
        public long TransacaoDesejada { get; set; }
        public int IdVacina { get; set; }
    }
}