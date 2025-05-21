using System;
using System.ComponentModel.DataAnnotations;

namespace PetMed_Digital.Models // Namespace original mantido
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; } // Chave primária base
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Valor padrão
    }
}