using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PharmaStock___API.Models
{
    [Table("Medico")]
    public class MedicoModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "O campo 'idPessoa' é obrigatório.")]
        public int idPessoa { get; set; }

        [Required(ErrorMessage = "O campo 'crm' é obrigatório.")]
        [StringLength(6, ErrorMessage = "O crm não pode ter mais de 6 caracteres.")]
        public string crm { get; set; }

        [ForeignKey("idPessoa")]
        public PessoaModel Pessoa { get; set; }

        [JsonIgnore]
        public virtual ICollection<RegistroMovimentoModel> RegistroMovimento { get; set; }
    }
}
