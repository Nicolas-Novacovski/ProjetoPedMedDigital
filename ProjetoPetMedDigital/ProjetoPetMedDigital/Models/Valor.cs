using PetMed_Digital.Models; // Adicionado para BaseModel
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Valor")]
    public class Valor : BaseModel
    {
        [Key]
        public int IdValor { get; set; }
        public decimal ValorProcedimento { get; set; } // Alterado para decimal
        public string TipoPagamento { get; set; }
        public decimal ValorReceita { get; set; } // Alterado para decimal
        public decimal ValorSaida { get; set; } // Alterado para decimal
        public decimal Salario { get; set; } // Alterado para decimal
        public decimal CompraProdutos { get; set; } // Alterado para decimal
        public int IdCliente { get; set; } // FK para Cliente. Se um valor est� associado a um cliente.

        // Propriedades de navega��o
        public Servico Servico { get; set; } // Se este Valor est� ligado a um Servico (rela��o 1-1, FK em Servico)
        public Cliente Cliente { get; set; }
    }
}