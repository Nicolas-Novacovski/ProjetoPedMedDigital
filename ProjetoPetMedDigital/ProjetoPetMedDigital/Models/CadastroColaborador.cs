using PetMed_Digital.Models;
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
        public string Nome { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string RG { get; set; } = null!;
        public DateTime Dtnascimento { get; set; }
        public string CEP { get; set; } = null!;
        public string Endereco { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public int Cargo { get; set; }
        public int TipoUsuario { get; set; }
        public int UsuarioId { get; set; } // Renomeado para evitar conflito com Login, e como FK
                                           // Mantenho a FK aqui, mas o relacionamento é com Usuario.Login
                                           // Você precisa decidir se Usuario.Login é a PK ou se Usuario.Id é a PK.
                                           // No seu PetMedContext, Usuario.Login é a PK.
                                           // Então, a FK aqui deveria ser do tipo string e se chamar Login.
                                           // VOU AJUSTAR PARA REFLETIR O PetMedContext:
        public string Login { get; set; } = null!; // Chave estrangeira para Usuario.Login

        // Propriedades de navegação
        public Usuario Usuario { get; set; } = null!;
        public Veterinario? Veterinario { get; set; } // Um colaborador PODE ser um veterinário
    }
}