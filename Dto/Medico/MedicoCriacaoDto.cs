using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.Medico
{
    public class MedicoCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'idPessoa' é obrigatório.")]
        public int idPessoa { get; set; }

        [Required(ErrorMessage = "O campo 'crm' é obrigatório.")]
        [StringLength(6, ErrorMessage = "O crm não pode ter mais de 6 caracteres.")]
        public string crm { get; set; }
    }
}
