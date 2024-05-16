using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.Paciente
{
    public class PacienteCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'idPessoa' é obrigatório.")]
        public int idPessoa { get; set; }

        [StringLength(20, ErrorMessage = "O número do cartão não pode ter mais de 20 caracteres.")]
        public string? cartaoSus { get; set; }

        [StringLength(100, ErrorMessage = "O número do cartão não pode ter mais de 100 caracteres.")]
        public string? planoSaude { get; set; }
    }
}
