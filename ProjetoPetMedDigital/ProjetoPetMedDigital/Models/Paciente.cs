using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Paciente")]
    public class Paciente : BaseModel
    {
        [Key]
        public int IdPaciente { get; set; }

        // Chave estrangeira para o Cliente
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; } // Navegação para a tabela Cliente

        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Raca { get; set; }
        public string Cor { get; set; }
        public string Sexo { get; set; }

        public ICollection<Prontuario> Prontuarios { get; set; }
        public ICollection<Agendamento> Agendamentos { get; set; }
        public ICollection<Vacina> Vacinas { get; set; }
        public ICollection<AgendaVeterinario> Agendas { get; set; }
    }
}
