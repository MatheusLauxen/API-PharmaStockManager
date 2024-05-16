using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.RegistroMovimento
{
    public class RegistroMovimentoCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'idUsuario' é obrigatório.")]
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "O campo 'idMedico' é obrigatório.")]
        public int idMedico { get; set; }

        [Required(ErrorMessage = "O campo 'data e hora' é obrigatório.")]
        public DateTime dtaHora { get; set; }

        [Required(ErrorMessage = "O campo 'transação' é obrigatório.")]
        [StringLength(1, ErrorMessage = "A transação não pode ter mais de 1 caracteres.")]
        public string tipoTransacao { get; set; }

        [Required(ErrorMessage = "O campo 'idProduto' é obrigatório.")]
        public int idProduto { get; set; }

        [Required(ErrorMessage = "O campo 'quantidade' é obrigatório.")]
        public int quantidade { get; set; }

        [Required(ErrorMessage = "O campo 'idPaciente' é obrigatório.")]
        public int idPaciente { get; set; }
    }
}
