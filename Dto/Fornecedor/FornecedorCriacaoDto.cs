using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.Fornecedor
{
    public class FornecedorCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'nome' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo 'cnpj' é obrigatório.")]
        [StringLength(14, ErrorMessage = "O cnpj não pode ter mais de 14 caracteres.")]
        public string cnpj { get; set; }

        [Required(ErrorMessage = "O campo 'telefone' é obrigatório.")]
        [StringLength(11, ErrorMessage = "O telefone não pode ter mais de 11 caracteres.")]
        public string telefone { get; set; }

        [Required(ErrorMessage = "O campo 'endereço' é obrigatório.")]
        [StringLength(250, ErrorMessage = "O endereço não pode ter mais de 250 caracteres.")]
        public string endereco { get; set; }
    }
}
