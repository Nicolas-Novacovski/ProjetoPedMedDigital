using PetMed_Digital.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("Veterinarios")]
    public class Veterinario : BaseModel
    {
        [Key]
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "O nome do veterinário é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome do veterinário não pode exceder 100 caracteres.")]
        [Display(Name = "Nome do Veterinário")]
        public string NomeVeterinario { get; set; } = null!;

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "Especialidade não pode exceder 100 caracteres.")]
        [Display(Name = "Especialidade")]
        public string Especialidade { get; set; } = null!;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O ID do colaborador é obrigatório.")]
        [Display(Name = "ID Colaborador")]
        public int IdColaborador { get; set; }

        // Propriedades de navegação
        public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
        public List<Prontuario> Prontuarios { get; set; } = new List<Prontuario>();
        public List<AgendaVeterinario> AgendaVeterinarios { get; set; } = new List<AgendaVeterinario>();
        public CadastroColaborador CadastroColaborador { get; set; } = null!;
   
        public List<Serviços> Servicos { get; set; } = new List<Serviços>();
    }
}