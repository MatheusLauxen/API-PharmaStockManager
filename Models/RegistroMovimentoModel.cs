using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaStock___API.Models
{
    [Table("RegistroMovimento")]
    public class RegistroMovimentoModel
    {
        [Key]
        public int id { get; set; }

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

        [ForeignKey("idUsuario")]
        public UsuarioModel Usuario { get; set; }

        [ForeignKey("idMedico")]
        public MedicoModel Medico { get; set; }

        [ForeignKey("idProduto")]
        public ProdutoModel Produto { get; set; }

        [ForeignKey("idPaciente")]
        public PacienteModel Paciente { get; set; }
    }
}
