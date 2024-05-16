using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PharmaStock___API.Models
{
    [Table("Paciente")]
    public class PacienteModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "O campo 'idPessoa' é obrigatório.")]
        public int idPessoa { get; set; }

        [StringLength(20, ErrorMessage = "O número do cartão não pode ter mais de 20 caracteres.")]
        public string? cartaoSus { get; set; }

        [StringLength(100, ErrorMessage = "O número do cartão não pode ter mais de 100 caracteres.")]
        public string? planoSaude { get; set; }

        [ForeignKey("idPessoa")]
        public PessoaModel Pessoa { get; set; }

        [JsonIgnore]
        public virtual ICollection<RegistroMovimentoModel> RegistroMovimento { get; set; }
    }
}
