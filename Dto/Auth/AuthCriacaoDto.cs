﻿using System.ComponentModel.DataAnnotations;

namespace PharmaStock___API.Dto.Auth
{
    public class AuthCriacaoDto
    {
        [Required(ErrorMessage = "O campo 'idPessoa' é obrigatório.")]
        public int idPessoa { get; set; }

        [Required(ErrorMessage = "O campo 'login' é obrigatório.")]
        [StringLength(50, ErrorMessage = "O login não pode ter mais de 50 caracteres.")]
        public string login { get; set; }

        [Required(ErrorMessage = "O campo 'senha' é obrigatório.")]
        [StringLength(250, ErrorMessage = "A senha não pode ter mais de 250 caracteres.")]
        public string senha { get; set; }

        [Required(ErrorMessage = "O campo 'idPermissao' é obrigatório.")]
        public int idPermissao { get; set; }
    }
}
