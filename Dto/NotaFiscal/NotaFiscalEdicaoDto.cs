using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.NotaFiscal
{
    public class NotaFiscalEdicaoDto
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "O campo 'data entrada' é obrigatório.")]
        public DateTime dtaEntrada { get; set; }

        [Required(ErrorMessage = "O campo 'número' é obrigatório.")]
        [StringLength(100, ErrorMessage = "O número não pode ter mais de 100 caracteres.")]
        public string numero { get; set; }

        [Required(ErrorMessage = "O campo 'idFornecedor' é obrigatório.")]
        public int idFornecedor { get; set; }

        [Required(ErrorMessage = "O campo 'valor' é obrigatório.")]
        public decimal valor { get; set; }
    }
}
