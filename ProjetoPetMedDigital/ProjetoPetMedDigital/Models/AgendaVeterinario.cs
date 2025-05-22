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
        // Como BaseModel.Id n�o � mais a PK global, voc� precisa definir uma PK aqui.
        // Vou assumir que o 'Id' da BaseModel ser� o Id da AgendaVeterinario.
        // OU voc� pode adicionar uma nova propriedade como 'IdAgendaVeterinario' com [Key].
        // Para simplicidade, vamos usar o Id da BaseModel como PK para esta tabela,
        // ou seja, o EF Core vai inferir 'Id' como PK se n�o houver outra.
        // Se voc� quiser uma PK espec�fica, adicione: [Key] public int IdAgendaVeterinario { get; set; }
        // e remova a heran�a de BaseModel, ou torne a BaseModel.Id como a PK.
        // Para ser consistente com outros modelos, vamos adicionar uma PK espec�fica:
        [Key]
        public int IdAgendaVeterinario { get; set; } // Nova chave prim�ria para esta tabela

        public int IdVeterinario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int IdPaciente { get; set; } // Esta FK parece estranha aqui, agendas de vet para pacientes?
                                            // Se for agendamento de vet dispon�vel, IdPaciente n�o faria sentido.
                                            // Mantenho como est� no seu c�digo, mas sugiro revisar a l�gica.

        // Propriedades de navega��o
        public Veterinario Veterinario { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!; // Se a FK IdPaciente for removida, remova esta linha.
    }
}