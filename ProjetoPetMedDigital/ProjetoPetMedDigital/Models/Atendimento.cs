using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Atendimento")]
    public class Atendimento : BaseModel
    {
        public Atendimento()
        {
            CreatedAt = DateTime.Now;
            StatusAtendimento = "Pendente";
        }

        public string DoutorEscalado { get; set; }
        public string StatusAtendimento { get; set; }
    }
}
