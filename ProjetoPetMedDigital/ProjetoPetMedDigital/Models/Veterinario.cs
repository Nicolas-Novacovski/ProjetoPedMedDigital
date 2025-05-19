using PetMed_Digital.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Veterinarios")] // Use o nome correto da tabela no banco de dados
    public class Veterinario : BaseModel
    {
        [Key]
        public int Idveterinario { get; set; } // Use o nome correto da propriedade da chave primária
        public string NomeVeterinario { get; set; }
        public string Especialidade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        // As FKs para Agendamento, Prontuario e Agenda serão tratadas pelas propriedades de navegação

        public int IdColaborador { get; set; } // Chave estrangeira para CadastroColaborador

        // Propriedades de navegação
        public List<Agendamento> Agendamentos { get; set; }
        public List<Prontuario> Prontuarios { get; set; }
        public List<AgendaVeterinario> AgendaVeterinarios { get; set; }
        public CadastroColaborador CadastroColaborador { get; set; }
    }
}