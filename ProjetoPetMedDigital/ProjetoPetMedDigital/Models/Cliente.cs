using PetMed_Digital.Models; // Adicionado para BaseModel
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
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime DtNascimento { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }

        // Propriedades de navegação
        public List<Paciente> Pacientes { get; set; }
        public List<Valor> Valores { get; set; } // Adicionado se Cliente pode ter múltiplos Valores
    }
}