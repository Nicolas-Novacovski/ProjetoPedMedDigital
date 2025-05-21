using PetMed_Digital.Models; // Adicionado para BaseModel
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
        public string NomeCachorro { get; set; }
        public int Estado { get; set; } // Alterado de long para int
        public string Problema { get; set; }
        public int TipoAtendimento { get; set; } // Alterado de long para int
        public float Peso { get; set; }
        public string SinaisVitais { get; set; }
        public string Recomendacoes { get; set; }

        // Propriedades de navegação
        public Cliente Cliente { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
        public List<Prontuario> Prontuarios { get; set; }
        public List<Vacina> Vacinas { get; set; }
    }
}