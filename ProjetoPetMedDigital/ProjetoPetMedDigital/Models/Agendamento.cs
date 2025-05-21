using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetMed_Digital.Models; // Adicionado para BaseModel
using System.Collections.Generic; // Adicionado para List

namespace ProjetoPetMedDigital.Models
{
    [Table("Agendamento")]
    public class Agendamento : BaseModel
    {
        [Key]
        public int IdAgendamento { get; set; }
        public int IdPaciente { get; set; }
        public int IdVeterinario { get; set; }
        // IdServico removido, pois um agendamento pode ter múltiplos serviços.
        // A relação será gerenciada por Servico.IdAgendamento
        public DateTime DataAgendamento { get; set; }
        public DateTime HoraAgendamento { get; set; } // Considerar usar TimeSpan ou apenas DateTime para DataAgendamento completo
        public string Observacoes { get; set; }
        // IdProntuario removido, Prontuario terá IdAgendamento para a relação 1-1

        // Propriedades de navegação
        public Paciente Paciente { get; set; }
        public Veterinario Veterinario { get; set; }
        public List<Servico> Servicos { get; set; } // Alterado para List<Servico>
        public Prontuario Prontuario { get; set; }
    }
}