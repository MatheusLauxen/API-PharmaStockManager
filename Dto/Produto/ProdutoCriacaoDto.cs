using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.Produto
{
    public class ProdutoCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'nome' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo 'data de válidade' é obrigatório.")]
        public DateTime dtaValidade { get; set; }

        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string? nomeTipo { get; set; }

        [Required(ErrorMessage = "O campo 'descrição' é obrigatório.")]
        [StringLength(200, ErrorMessage = "A descrição não pode ter mais de 200 caracteres.")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "O campo 'fabricante' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O fabricante não pode ter mais de 50 caracteres.")]
        public string fabricante { get; set; }
    }
}
