using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace GerenciadorTarefasCore.Models
{
    public class Projeto
    {

        public Projeto()
        {
            Tarefas = new Collection<Tarefa>();
        }

        public Projeto(string nome, string descricao, DateTime dataPrazo, Tarefa tarefa)
        {
            Nome = nome;
            Descricao = descricao;
            DataConclusao = dataPrazo;

        }

        public int ProjetoId { get; set; }

        //Required é um atributo que obriga a tal propriedade a ser preenchida com um valor.
        [Required] 
        [StringLength(100)] //Define o tamanho máximo de caracteres que a propriedade pode ter.
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }

        [JsonIgnore]
        public ICollection<Tarefa>? Tarefas { get; set; }
        
    }
}
