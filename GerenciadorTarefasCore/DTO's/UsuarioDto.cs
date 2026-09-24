using GerenciadorTarefasCore.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorTarefasCore.DTO_s
{
    public class UsuarioDto
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [ContemArroba]
        public string Email { get; set; }

        public UsuarioDto(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
    }
}
