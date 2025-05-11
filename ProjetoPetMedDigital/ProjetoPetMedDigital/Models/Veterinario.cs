using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
	[Table("Veterinario")]
	public class Veterinario : BaseModel
	{
		[Key]
		public int IdVeterinario { get; set; }
		public string NomeVeterinario { get; set; }
		public string Especialidade { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }
		public int IdVacina { get; set; }
	}
}