using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Agendamento")]
    public class Agendamento : BaseModel
    {
        [Key]
        public int IdAgendamento { get; set; }
        public int IdPaciente { get; set; }
        public int IdVeterinario { get; set; }
        public int IdServico { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime HoraAgendamento { get; set; }
        public string Observacoes { get; set; }
        public int IdValor { get; set; }
    }
}