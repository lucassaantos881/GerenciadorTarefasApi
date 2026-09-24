using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace GerenciadorTarefasCore.Models
{
    public class Projeto : IValidatableObject
    {

        
        public Projeto(string nome, string descricao, DateTime dataCriacao)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException(nameof(nome), "O nome do projeto é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentNullException(nameof(descricao), "A descrição do projeto é obrigatória.");
            }

            Nome = nome;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            Tarefas = new Collection<Tarefa>();
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

        public bool Finalizado { get; set; } = false;

        public ICollection<Tarefa>? Tarefas { get; set; }

        public void FinalizarProjeto(string confirmacao, DateTime dataConclusao)
        {
            if (confirmacao.ToUpper() == "SIM")
            {
                Finalizado = true;
                DataConclusao = dataConclusao;
            }
            else
            {
                throw new InvalidOperationException("Para finalizar o projeto, confirme com 'SIM'.");
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataConclusao < DataCriacao)
            {
                yield return new ValidationResult("A data de conclusão não pode ser anterior à data de criação do projeto.", new[] { nameof(DataConclusao)});
            }
        }

    }
}
