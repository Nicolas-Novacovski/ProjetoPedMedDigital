using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Paciente")]
    public class Paciente : BaseModel
    {
        [Key]
        public int IdPaciente { get; set; }
        public int IdCliente { get; set; }
        public string NomeCachorro { get; set; }
        public long Estado { get; set; }
        public string Problema { get; set; }
        public long TipoAtendimento { get; set; }
        public float Peso { get; set; }
        public string SinaisVitais { get; set; }
        public string Recomendacoes { get; set; }
    }
}