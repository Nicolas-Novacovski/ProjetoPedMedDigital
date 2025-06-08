// Removida: using ProjetoPetMedDigital.Models; - pois BaseModel estar� em ProjetoPetMedDigital.Models
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models // NAMESPACE PADRONIZADO
{
    [Table("Veterinarios")]
    public class Veterinario : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "O nome do veterin�rio � obrigat�rio.")]
        [StringLength(100, ErrorMessage = "Nome do veterin�rio n�o pode exceder 100 caracteres.")]
        [Display(Name = "Nome do Veterin�rio")]
        public string NomeVeterinario { get; set; } = null!;

        [Required(ErrorMessage = "A especialidade � obrigat�ria.")]
        [StringLength(100, ErrorMessage = "Especialidade n�o pode exceder 100 caracteres.")]
        [Display(Name = "Especialidade")]
        public string Especialidade { get; set; } = null!;

        [Required(ErrorMessage = "O telefone � obrigat�rio.")]
        [Phone(ErrorMessage = "Formato de telefone inv�lido.")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inv�lido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O ID do colaborador � obrigat�rio.")]
        [Display(Name = "ID Colaborador")]
        public int IdColaborador { get; set; }

        // Propriedades de navega��o
        public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
        public List<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();
        public List<AgendaVeterinario> AgendaVeterinarios { get; set; } = new List<AgendaVeterinario>();
        public CadastroColaborador? CadastroColaborador { get; set; }
        public List<Servico> Servico { get; set; } = new List<Servico>(); // Refer�ncia a Servico (singular)
    }
}