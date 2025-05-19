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
        public string NomeCachorro { get; set; }
        public long Estado { get; set; }
        public string Problema { get; set; }
        public long TipoAtendimento { get; set; }
        public float Peso { get; set; }
        public string SinaisVitais { get; set; }
        public string Recomendacoes { get; set; }
        // As FKs para Agendamento, Prontuario e Vacina serão tratadas pelas propriedades de navegação

        // Propriedades de navegação
        public Cliente Cliente { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
        public List<Prontuario> Prontuarios { get; set; }
        public List<Vacina> Vacinas { get; set; }
    }
}