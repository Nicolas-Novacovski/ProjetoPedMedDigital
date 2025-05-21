using PetMed_Digital.Models;
using System.Collections.Generic;
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
        public string NomeCachorro { get; set; } = null!;
        public int Estado { get; set; }
        public string Problema { get; set; } = string.Empty;
        public int TipoAtendimento { get; set; }
        public float Peso { get; set; }
        public string SinaisVitais { get; set; } = string.Empty;
        public string Recomendacoes { get; set; } = string.Empty;

        // Propriedades de navegação
        public Cliente Cliente { get; set; } = null!;
        public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
        public List<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();
        public List<Vacina> Vacinas { get; set; } = new List<Vacina>();
    }
}