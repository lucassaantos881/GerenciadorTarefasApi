using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace GerenciadorTarefasCore.Models
{
    public class Usuario
    {

        public Usuario()
        {
            Tarefas = new Collection<Tarefa>();
        }

        public int UsuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string? Email { get; set; }

        [JsonIgnore]
        public ICollection<Tarefa>? Tarefas { get; set; }


    }
}
