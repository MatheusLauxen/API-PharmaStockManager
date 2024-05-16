using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.Pessoa
{
    public class PessoaCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'nome' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string nome { get; set; }

        [Required(ErrorMessage = "O campo 'data de nascimento' é obrigatório.")]
        public DateTime dtaNascimento { get; set; }

        [Required(ErrorMessage = "O campo 'cpf' é obrigatório.")]
        [StringLength(11, ErrorMessage = "O cpf não pode ter mais de 11 caracteres.")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "O campo 'telefone' é obrigatório.")]
        [StringLength(11, ErrorMessage = "O telefone não pode ter mais de 11 caracteres.")]
        public string telefone { get; set; }

        [Required(ErrorMessage = "O campo 'sexo' é obrigatório.")]
        [StringLength(1, ErrorMessage = "O sexo não pode ter mais de 1 caracteres.")]
        public string sexo { get; set; }
    }
}
