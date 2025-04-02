using PetMed_Digital.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Donos")]
    public class CadastroDonoDoAnimal : CadastroAnimal
    {
        public CadastroDonoDoAnimal()
        {
            CreatedAt = DateTime.Now;
        }

        [Key]
        public string CPF { get; set; }
        public string NomeDonoDoAnimal { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

    }
}
