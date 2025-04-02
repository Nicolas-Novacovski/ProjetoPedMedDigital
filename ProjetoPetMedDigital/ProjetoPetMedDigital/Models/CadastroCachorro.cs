using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PetMed_Digital.Models
{
    [Table("CadastroCachorro")]
    public class CadastroCachorro : BaseModel
    {
        public string NomeCachorro { get; set; }
        public int IdadeCachorro { get; set; }
        public string RacaCachorro{ get; set; }
        public float PesoCachorro { get; set; }
        public string Urgencia { get; set; }
        public string Perigo { get; set; }
        public string Observacoes { get; set; }
        public bool Vacinado { get; set; }
    }
}
