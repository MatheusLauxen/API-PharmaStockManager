using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PharmaStock___API.Models
{
    [Table("NotaFiscal")]
    public class NotaFiscalModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "O campo 'data entrada' é obrigatório.")]
        public DateTime dtaEntrada { get; set; }

        [Required(ErrorMessage = "O campo 'número' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O número não pode ter mais de 100 caracteres.")]
        public string numero { get; set; }

        [Required(ErrorMessage = "O campo 'idFornecedor' é obrigatório.")]
        public int idFornecedor{ get; set; }

        [Required(ErrorMessage = "O campo 'valor' é obrigatório.")]
        public decimal valor{ get; set; }

        [Required(ErrorMessage = "O campo 'data cadastro nota' é obrigatório.")]
        public DateTime dtaCadastroNota { get; set; }

        [ForeignKey("idFornecedor")]
        public FornecedorModel Fornecedor{ get; set; }

        [JsonIgnore]
        public virtual ICollection<ProdutoNotaModel> ProdutoNota { get; set; }
    }
}
