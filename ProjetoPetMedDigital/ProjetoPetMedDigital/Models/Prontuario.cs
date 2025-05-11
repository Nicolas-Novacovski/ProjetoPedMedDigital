using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Prontuario")]
    public class Prontuario : BaseModel
    {
        [Key]
        public int IdProntuario { get; set; }
        public int IdAgendamento { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime DataConsulta { get; set; }
        public string MotivoConsulta { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamento { get; set; }
        public float Peso { get; set; }
        public int Temperatura { get; set; }
        public int FrequenciaCardiaca { get; set; }
        public int FrequenciaRespiatoria { get; set; }
        public string Observacoes { get; set; }
        public int IdPaciente { get; set; }
    }
}