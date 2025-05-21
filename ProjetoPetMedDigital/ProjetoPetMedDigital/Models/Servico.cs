using PetMed_Digital.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Servico")]
    public class Servico : BaseModel
    {
        [Key]
        public int IdServico { get; set; }
        public string TipoVenda { get; set; } = null!;
        public string NomeServico { get; set; } = null!;
        public int IdVeterinario { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public int Status { get; set; }
        public decimal PrecoVenda { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IdAgendamento { get; set; }
        public int IdProduto { get; set; }
        public int IdValor { get; set; }

        // Propriedades de navegação
        public Agendamento Agendamento { get; set; } = null!;
        public Veterinario Veterinario { get; set; } = null!;
        public ItemEstoque ItemEstoque { get; set; } = null!;
        public Valor Valor { get; set; } = null!;
    }
}