using PetMed_Digital.Models; // Adicionado para BaseModel
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
        public string NomeProduto { get; set; }
        public string Descricao { get; set; }
        public int? Quantidade { get; set; }
        public decimal? PrecoCusto { get; set; }
        public decimal? PrecoVenda { get; set; }
        public string UnidadeMedida { get; set; }
        public DateTime? DataValidade { get; set; }
        public string Fornecedor { get; set; }
        public int? TransacaoDesejada { get; set; } // Alterado de long? para int?

        // Propriedades de navegação (para relações 1-para-1 onde ItemEstoque é o principal)
        public Vacina Vacina { get; set; }
        public Procedimento Procedimento { get; set; }
        public Servico Servico { get; set; } // Se um ItemEstoque está diretamente ligado a um único tipo de serviço
    }
}