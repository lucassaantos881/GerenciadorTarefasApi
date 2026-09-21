using GerenciadorTarefasCore.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;


namespace GerenciadorTarefasCore.DTO_s
{
    public class ProjetoDto
    {

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(150)]
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }

        public ProjetoDto(string nome, string descricao, DateTime dataCriacao)
        {
            Nome = nome;
            Descricao = descricao;
            DataCriacao = dataCriacao;
        }
    }
}
