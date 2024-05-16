using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.Estoque
{
    public class EstoqueCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'idProduto' é obrigatório.")]
        public int idProduto { get; set; }

        [Required(ErrorMessage = "O campo 'quantidade' é obrigatório.")]
        public int quantidade { get; set; }
    }
}
