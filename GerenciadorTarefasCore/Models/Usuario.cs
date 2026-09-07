using GerenciadorTarefasCore.Validations;
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

        public Usuario(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException(nameof(nome), "O nome do usuário é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email), "O email do usuário é obrigatório.");
            }

            Nome = nome;
            Tarefas = new Collection<Tarefa>();

            var primeiraLetra = email?.ToString()[0].ToString();

            if (primeiraLetra == primeiraLetra?.ToUpper())
            {
                Email = email?.ToLower();
            }

            
        }

        public int UsuarioId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(100)]
        [ContemArroba]
        public string? Email { get; set; }

        [JsonIgnore]
        public ICollection<Tarefa>? Tarefas { get; set; }


    }
}
