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
        public int IdProduto { get; set; } // Esta será a PK para Vacina, Procedimento, Servico
        public string NomeProduto { get; set; } = null!;
        public string Descricao { get; set; } = string.Empty;
        public int? Quantidade { get; set; }
        public decimal? PrecoCusto { get; set; }
        public decimal? PrecoVenda { get; set; }
        public string UnidadeMedida { get; set; } = null!;
        public DateTime? DataValidade { get; set; }
        public string Fornecedor { get; set; } = null!;
        public int? TransacaoDesejada { get; set; }

        // Propriedades de navegação (One-to-One com chave compartilhada)
        public Vacina? Vacina { get; set; }
        public Procedimento? Procedimento { get; set; }
        public Servico? Servico { get; set; }
    }
}