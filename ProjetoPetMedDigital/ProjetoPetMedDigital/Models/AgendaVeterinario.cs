using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("AgendaVeterinario")]
    public class AgendaVeterinario : BaseModel
    {
        [Key]
        public int IdAgenda { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int IdPaciente { get; set; }
    }
}