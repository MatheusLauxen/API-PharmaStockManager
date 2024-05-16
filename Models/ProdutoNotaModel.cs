using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaStock___API.Models
{
    [Table("ProdutoNota")]
    public class ProdutoNotaModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "O campo 'idNota' é obrigatório.")]
        public int idNota{ get; set; }

        [Required(ErrorMessage = "O campo 'idProduto' é obrigatório.")]
        public int idProduto { get; set; }

        [Required(ErrorMessage = "O campo 'quantidade' é obrigatório.")]
        public int quantidade { get; set; }

        [Required(ErrorMessage = "O campo 'lote' é obrigatório.")]
        public int lote { get; set; }

        [ForeignKey("idNota")]
        public NotaFiscalModel NotaFiscal { get; set; }

        [ForeignKey("idProduto")]
        public ProdutoModel Produto { get; set; }
    }
}
