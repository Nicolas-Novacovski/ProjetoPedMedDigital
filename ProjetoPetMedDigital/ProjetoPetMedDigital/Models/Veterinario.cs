using PetMed_Digital.Models; // Adicionado para BaseModel
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Veterinarios")] // Mantido, mas confirme o nome da tabela no DB
    public class Veterinario : BaseModel
    {
        [Key]
        public int IdVeterinario { get; set; } // Corrigido: Idveterinario -> IdVeterinario
        public string NomeVeterinario { get; set; }
        public string Especialidade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int IdColaborador { get; set; } // Chave estrangeira para CadastroColaborador.IdColaborador

        // Propriedades de navegação
        public List<Agendamento> Agendamentos { get; set; }
        public List<Prontuario> Prontuarios { get; set; }
        public List<AgendaVeterinario> AgendaVeterinarios { get; set; }
        public CadastroColaborador CadastroColaborador { get; set; }
        public List<Servico> Servicos { get; set; } // Adicionado: Um veterinário pode realizar múltiplos serviços
    }
}