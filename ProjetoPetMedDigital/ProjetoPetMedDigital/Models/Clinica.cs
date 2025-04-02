using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMed_Digital.Models
{
    [Table("Clinica")]
    public class Clinica : BaseModel
    {
        public Clinica()
        {
            CreatedAt = DateTime.Now;
        }
        public int QntdAtendimentos { get; set; }
        public float ValorLiquido { get; set; }
        public float ValorBruto { get; set; }
        public float DespesasClinica { get; set; }
        public int DespesasFuncionarios { get; set; }
        public int CasosGraves { get; set; }
        public int CasosMedios { get; set; }
        public int CasosLeves { get; set; }
        public int VacinasAplicadas { get; set; }
        public int EstoqueVacina { get; set; }
        public int EstoqueMedicamento { get; set; }
        public int? ListaMateriais { get; set; }
    }
}
