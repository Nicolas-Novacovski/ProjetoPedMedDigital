using PetMed_Digital.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Cliente")]
    public class Cliente : BaseModel
    {
        [Key]
        public int IdCliente { get; set; }
        public string NomeResponsavel { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string RG { get; set; } = null!;
        public DateTime DtNascimento { get; set; }
        public string CEP { get; set; } = null!;
        public string Endereco { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Cidade { get; set; } = null!;

        // Propriedades de navegação
        public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
        public List<Valor> Valores { get; set; } = new List<Valor>();
    }
}