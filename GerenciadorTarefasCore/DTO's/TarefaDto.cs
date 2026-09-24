using GerenciadorTarefasCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorTarefasCore.DTO_s
{
    public class TarefaDto
    {

        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(150)]
        public string Descricao { get; set; }
        public DateTime DataPrazo { get; set; }
        public int ProjetoId { get; set; }
        public int UsuarioId { get; set; }

        public TarefaDto(string titulo, string descricao, DateTime dataPrazo, int projetoId, int usuarioId)
        {
            Titulo = titulo;
            Descricao = descricao;
            DataPrazo = dataPrazo;
            ProjetoId = projetoId;
            UsuarioId = usuarioId;
        }
    }
}
