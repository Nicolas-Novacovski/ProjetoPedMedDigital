using PetMed_Digital.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Veterinarios")] // Nome da tabela está no plural, mantenha a consistência.
    public class Veterinario : BaseModel
    {
        [Key]
        public int IdVeterinario { get; set; }
        public string NomeVeterinario { get; set; } = null!;
        public string Especialidade { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int IdColaborador { get; set; } // FK para CadastroColaborador.IdColaborador

        // Propriedades de navegação
        public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
        public List<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();
        public List<AgendaVeterinario> AgendaVeterinarios { get; set; } = new List<AgendaVeterinario>();
        public CadastroColaborador CadastroColaborador { get; set; } = null!;
        public List<Serviços> Servicos { get; set; } = new List<Serviços>();
    }
}