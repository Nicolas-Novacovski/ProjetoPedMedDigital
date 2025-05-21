using PetMed_Digital.Models; // Adicionado para BaseModel
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoPetMedDigital.Models
{
    [Table("CadastroColaborador")]
    public class CadastroColaborador : BaseModel
    {
        [Key]
        public int IdColaborador { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime Dtnascimento { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int Cargo { get; set; } // Alterado de long para int
        public int TipoUsuario { get; set; } // Alterado de long para int
        public string Login { get; set; } // Chave estrangeira para Usuario.Login (PK de Usuario)

        // Propriedades de navegação
        public Usuario Usuario { get; set; }
        public Veterinario Veterinario { get; set; } // Se um colaborador PODE ser um veterinário (relação 1-0 ou 1)
    }
}