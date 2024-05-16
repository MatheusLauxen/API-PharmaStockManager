using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Models
{
    [Table("Estoque")]
    public class EstoqueModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "O campo 'idProduto' é obrigatório.")]
        public int idProduto { get; set; }

        [Required(ErrorMessage = "O campo 'quantidade' é obrigatório.")]
        public int quantidade { get; set; }

        [ForeignKey("idProduto")]
        public ProdutoModel Produto { get; set; }
    }
}
