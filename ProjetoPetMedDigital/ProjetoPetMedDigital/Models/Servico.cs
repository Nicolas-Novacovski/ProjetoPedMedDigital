using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Servico")]
    public class Servico : BaseModel
    {
        [Key]
        public int IdServico { get; set; }
        public string TipoVenda { get; set; }
        public string NomeServico { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public long Status { get; set; }
        public decimal PrecoVenda { get; set; }
        public string Descricao { get; set; }
        public int IdProduto { get; set; }
    }
}