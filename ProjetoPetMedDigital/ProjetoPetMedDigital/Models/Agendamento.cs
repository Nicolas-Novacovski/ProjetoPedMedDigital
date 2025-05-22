using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetMed_Digital.Models; // Certifique-se de que este using est� correto
using System.Collections.Generic;

namespace ProjetoPetMedDigital.Models
{
    [Table("Agendament")]
    public class Agendamento : BaseModel
    {
        [Key]
        public int IdAgendamento { get; set; }
        public int IdPaciente { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime HoraAgendamento { get; set; }
        public string Observacoes { get; set; } = string.Empty; // Inicializado

        // Propriedades de navega��o
        public Paciente Paciente { get; set; } = null!; // EF Core preencher�
        public Veterinario Veterinario { get; set; } = null!; // EF Core preencher�
        public List<Servico> Servicos { get; set; } = new List<Servico>(); // Inicializado
        public Prontuario? Prontuario { get; set; } // Um agendamento pode n�o ter um prontu�rio imediatamente
    }
}
```