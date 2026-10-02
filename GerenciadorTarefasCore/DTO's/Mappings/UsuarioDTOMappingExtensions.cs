using System;
using System.Collections.Generic;
using System.Text;
using GerenciadorTarefasCore.Models;
using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.DTO_s.Mappings;

namespace GerenciadorTarefasCore.DTO_s.Mappings
{
    public static class UsuarioDTOMappingExtensions
    {
        public static UsuarioDto ToUsuarioDto(this Usuario usuario)
        {
            if (usuario == null) return null;
            return new UsuarioDto
            {
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public static Usuario ToUsuario(this UsuarioDto usuarioDto)
        {
            if (usuarioDto == null) return null;
            return new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email
            };
        }

        public static void AtualizarUsuario(this Usuario usuario, UsuarioDto usuarioDto)
        {
            if (usuario == null || usuarioDto == null) return;
            usuario.Nome = usuarioDto.Nome;
            usuario.Email = usuarioDto.Email;
        }
    }
}
