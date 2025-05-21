using PetMed_Digital.Models; // Adicionado para BaseModel
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Procedimento")]
    public class Procedimento : BaseModel
    {
        [Key]
        public int IdProcedimento { get; set; }
        public string NomeProcedimento { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int IdProduto { get; set; } // Chave estrangeira para ItemEstoque.IdProduto

        // Propriedades de navega��o
        public ItemEstoque ItemEstoque { get; set; }
    }
}