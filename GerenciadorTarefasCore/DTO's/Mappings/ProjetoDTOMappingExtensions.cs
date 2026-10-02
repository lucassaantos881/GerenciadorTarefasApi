using System;
using System.Collections.Generic;
using System.Text;
using GerenciadorTarefasCore.Models;
using GerenciadorTarefasCore.DTO_s;

namespace GerenciadorTarefasCore.DTO_s.Mappings
{
    public static class ProjetoDTOMappingExtensions
    {
        public static ProjetoDto? ToProjetoDto(this Projeto projeto)
        {
            if (projeto is null)
            {
                return null;
            }

            return new ProjetoDto
            {
                Nome = projeto.Nome,
                Descricao = projeto.Descricao,
                DataCriacao = projeto.DataCriacao
            };
        }

        public static Projeto? ToProjeto(this ProjetoDto projetoDto)
        {
            if (projetoDto is null)
            {
                return null;
            }
            return new Projeto
            {   
                Nome = projetoDto.Nome,
                Descricao = projetoDto.Descricao,
                DataCriacao = projetoDto.DataCriacao
            };
        }
        public static void AtualizarProjeto(this Projeto projeto, ProjetoDto projetoDto)
        {
            if (projeto is null || projetoDto is null)
            {
                return;
            }

            projeto.Nome = projetoDto.Nome;
            projeto.Descricao = projetoDto.Descricao;
            projeto.DataCriacao = projetoDto.DataCriacao;

        }
    } 
}
