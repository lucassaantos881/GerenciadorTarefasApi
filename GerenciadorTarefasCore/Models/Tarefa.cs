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
        public int TarefaId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Titulo { get; set; }

        [Required]
        [StringLength(150)]
        public string? Descricao { get; set; }
        public StatusTarefa Status { get; set; }
        public DateTime DataPrazo { get; set; }
        public int ProjetoId { get; set; }
        public int UsuarioId { get; set; }

        [JsonIgnore]
        public Projeto? Projeto { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        public void FinalizarTarefa(string confirmar)
        {
            bool ConfirmarTarefa = false;
           

            if(confirmar == "SIM")
            {
                ConfirmarTarefa = true;
                Status = StatusTarefa.Concluida;
            }

            Status = StatusTarefa.Pendente;

        }
       

    }
}
