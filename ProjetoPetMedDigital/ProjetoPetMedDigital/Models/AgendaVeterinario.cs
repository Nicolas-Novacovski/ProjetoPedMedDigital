using System;
using System.ComponentModel.DataAnnotations; // Adicionado para [Key]
using System.ComponentModel.DataAnnotations.Schema;
using ProjetoPetMedDigital.Models;
using PetMed_Digital.Models; // Adicionado para BaseModel

namespace ProjetoPetMedDigital.Models
{
    [Table("AgendaVeterinario")]
    public class AgendaVeterinario : BaseModel
    {
        // Como BaseModel.Id não é mais a PK global, você precisa definir uma PK aqui.
        // Vou assumir que o 'Id' da BaseModel será o Id da AgendaVeterinario.
        // OU você pode adicionar uma nova propriedade como 'IdAgendaVeterinario' com [Key].
        // Para simplicidade, vamos usar o Id da BaseModel como PK para esta tabela,
        // ou seja, o EF Core vai inferir 'Id' como PK se não houver outra.
        // Se você quiser uma PK específica, adicione: [Key] public int IdAgendaVeterinario { get; set; }
        // e remova a herança de BaseModel, ou torne a BaseModel.Id como a PK.
        // Para ser consistente com outros modelos, vamos adicionar uma PK específica:
        [Key]
        public int IdAgendaVeterinario { get; set; } // Nova chave primária para esta tabela

        public int IdVeterinario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int IdPaciente { get; set; } // Esta FK parece estranha aqui, agendas de vet para pacientes?
                                            // Se for agendamento de vet disponível, IdPaciente não faria sentido.
                                            // Mantenho como está no seu código, mas sugiro revisar a lógica.

        // Propriedades de navegação
        public Veterinario Veterinario { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!; // Se a FK IdPaciente for removida, remova esta linha.
    }
}