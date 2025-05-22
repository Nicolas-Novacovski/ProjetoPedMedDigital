using System;
// Removido o [Key] aqui, pois cada classe filha terá sua própria chave primária
// usando a propriedade específica (ex: IdAgendamento, IdCliente).
// O 'Id' da BaseModel passa a ser um campo comum, sem ser PK.

namespace PetMed_Digital.Models
{
    public class BaseModel
    {
        public int Id { get; set; } // Agora é um campo comum, não a chave primária
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}