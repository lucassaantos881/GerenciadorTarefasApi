using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace GerenciadorTarefasCore.Models
{
    public class Tarefa
    {
        public Tarefa()
        {

        }

        public Tarefa(string titulo, string descricao, StatusTarefa status, DateTime dataPrazo, int usuarioId, int projetoId)
        {
            if(string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentNullException(nameof(titulo), "O título da tarefa é obrigatório.");
            }

            if(string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentNullException(nameof(descricao), "A descrição da tarefa é obrigatória.");
            }

            if(usuarioId < 0)
            {
                throw new ArgumentNullException(nameof(usuarioId), "O ID do usuário é obrigatório.");
            }

            if (projetoId < 0)
            {
                throw new ArgumentNullException(nameof(projetoId), "O ID do projeto é obrigatório.");
            }

            Titulo = titulo;
            Descricao = descricao;
            Status = status;
            DataPrazo = dataPrazo;
            UsuarioId = usuarioId;
            ProjetoId = projetoId;
        }

        public int TarefaId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Titulo { get; set; }

        [Required]
        [StringLength(150)]
        public string? Descricao { get; set; }
        public StatusTarefa Status { get; set; }
        public DateTime? DataPrazo { get; set; }
        public int ProjetoId { get; set; }
        public int UsuarioId { get; set; }

        [JsonIgnore]
        public Projeto? Projeto { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        public void FinalizarTarefa(string confirmar)
        {
            if (confirmar == "SIM" && Status == StatusTarefa.Em_Andamento)
            {
                Status = StatusTarefa.Concluida;
            }
            else
            {
                throw new InvalidOperationException("Não foi possível finalizar tarefa, pois a mesma ainda não foi iniciada!!");
            }
  

        }



    }
}
