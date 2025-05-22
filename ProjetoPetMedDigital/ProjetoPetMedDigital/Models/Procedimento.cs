using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Procedimento")]
    public class Procedimento : BaseModel
    {
        [Key]
        public int IdProcedimento { get; set; } // PK da tabela Procedimento
        public string NomeProcedimento { get; set; } = null!;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int IdProduto { get; set; } // FK para ItemEstoque.IdProduto

        // Propriedades de navegação
        public ItemEstoque ItemEstoque { get; set; } = null!;
    }
}