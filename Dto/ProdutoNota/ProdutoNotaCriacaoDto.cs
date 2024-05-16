using PharmaStock___API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.ProdutoNota
{
    public class ProdutoNotaCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'idNota' é obrigatório.")]
        public int idNota { get; set; }

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
