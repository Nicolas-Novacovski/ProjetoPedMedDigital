using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PetMed_Digital.Models
{
    [Table("Animais")]
    public class CadastroAnimal : BaseModel
    {
        [Key]
        public int IdCachorro { get; set; }
        public string NomeAnimal { get; set; }
        public int IdadeAnimal { get; set; }
        public string TipoAnimal { get; set; }
        public string RacaAnimal{ get; set; }
        public float PesoAnimal { get; set; }
        public bool Agressivo { get; set; }
        public bool Vacinado { get; set; }
        public string? Observacoes { get; set; }
    }
}
