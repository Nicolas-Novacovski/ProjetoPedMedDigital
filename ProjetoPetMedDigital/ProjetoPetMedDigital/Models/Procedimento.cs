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

        // Chave estrangeira para o Prontuario
        [ForeignKey("Prontuario")]
        public int IdProntuario { get; set; }
        public Prontuario Prontuario { get; set; } // Navegação para a tabela Prontuario
    }
}
