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
        public int Idveterinario { get; set; } // Use o nome correto da propriedade da chave prim�ria
        public string NomeVeterinario { get; set; }
        public string Especialidade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        // As FKs para Agendamento, Prontuario e Agenda ser�o tratadas pelas propriedades de navega��o

        public int IdColaborador { get; set; } // Chave estrangeira para CadastroColaborador

        // Propriedades de navega��o
        public List<Agendamento> Agendamentos { get; set; }
        public List<Prontuario> Prontuarios { get; set; }
        public List<AgendaVeterinario> AgendaVeterinarios { get; set; }
        public CadastroColaborador CadastroColaborador { get; set; }
    }
}