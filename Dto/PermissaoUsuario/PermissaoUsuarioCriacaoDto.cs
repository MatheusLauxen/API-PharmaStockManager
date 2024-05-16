using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.PermissaoUsuario
{
    public class PermissaoUsuarioCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'descrição' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O descrição não pode ter mais de 50 caracteres.")]
        public string descricao { get; set; }
    }
}
