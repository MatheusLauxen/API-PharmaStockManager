using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PharmaStock___API.Models
{
    [Table("PermissaoUsuario")]
    public class PermissaoUsuarioModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "O campo 'descrição' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O descrição não pode ter mais de 50 caracteres.")]
        public string descricao { get; set; }

        [JsonIgnore]
        public virtual ICollection<UsuarioModel> Usuario { get; set; }
    }
}
