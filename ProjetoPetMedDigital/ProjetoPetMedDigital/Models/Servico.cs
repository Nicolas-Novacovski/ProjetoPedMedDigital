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
        public string TipoVenda { get; set; }
        public string NomeServico { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public long Status { get; set; }
        public decimal PrecoVenda { get; set; }
        public string Descricao { get; set; }
        public int IdAgendamento { get; set; } // Chave estrangeira para Agendamento
        public int IdProduto { get; set; } // Chave estrangeira para ItemEstoque (não estava no SQL, adicionei baseado no Procedimento e Vacina)
        public int IdValor { get; set; } // Chave estrangeira para Valor

        // Propriedades de navegação
        public Agendamento Agendamento { get; set; }
        public Veterinario Veterinario { get; set; }
        public ItemEstoque ItemEstoque { get; set; }
        public Valor Valor { get; set; }
    }
}