using GerenciadorTarefasCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorTarefasCore.DTO_s.Mappings
{
    public static class TarefaDTOMappingExtensions
    {
        public static TarefaDto ToTarefaDto(this Tarefa tarefa)
        {
            if (tarefa == null) return null;

            return new TarefaDto
            {
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataPrazo = tarefa.DataPrazo,
                ProjetoId = tarefa.ProjetoId,
                UsuarioId = tarefa.UsuarioId
            };
        }

        public static Tarefa ToTarefa(this TarefaDto tarefaDto)
        {
            if (tarefaDto == null) return null;

            return new Tarefa
            {
                Titulo = tarefaDto.Titulo,
                Descricao = tarefaDto.Descricao,
                DataPrazo = tarefaDto.DataPrazo,
                ProjetoId = tarefaDto.ProjetoId,
                UsuarioId = tarefaDto.UsuarioId
            };
        }

        public static void AtualizarTarefa(this Tarefa tarefa, TarefaDto tarefaDto)
        {
            if (tarefa == null || tarefaDto == null) return;

            tarefa.Titulo = tarefaDto.Titulo;
            tarefa.Descricao = tarefaDto.Descricao;
            tarefa.DataPrazo = tarefaDto.DataPrazo;
        }
    }
}