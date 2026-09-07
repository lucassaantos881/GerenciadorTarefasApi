using GerenciadorTarefasApi.Context;
using GerenciadorTarefasCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefasApi.Services
{
    public class TarefaService : ITarefaService
    {

        private readonly GerenciadorContext _context;

        public TarefaService(GerenciadorContext context)
        {
            _context = context;
        }

        public async Task AdicionarTarefaAsync(Tarefa tarefa)
        {
            
                if (tarefa == null)
                {
                    throw new ArgumentNullException("A tarefa não pode ser nula.");
                }

                //Realiza a consulta para verificar se a data de prazo da tarefa é anterior à data de criação do projeto
                var projeto = _context.Projetos?.FirstOrDefault(p => p.ProjetoId == tarefa.ProjetoId);

                if(tarefa.DataPrazo < projeto?.DataCriacao)
                {
                    throw new ArgumentException("A data de prazo não pode ser anterior à data de início do projeto.");
                }

                _context.Add(tarefa);
                await _context.SaveChangesAsync();

                
            
        }

        public async Task AtualizarTarefaAsync(int id, Tarefa tarefa)
        {

            

                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID da tarefa deve ser um valor positivo.");
                }

                var tarefaExistente = _context.Tarefas?.FirstOrDefault(t => t.TarefaId == id);

                if (tarefaExistente == null)
                {
                    throw new KeyNotFoundException($"Tarefa com ID {id} não encontrada para atualizar.");
                }
                else
                {
                    tarefaExistente.Titulo = tarefa.Titulo;
                    tarefaExistente.Descricao = tarefa.Descricao;
                    tarefaExistente.UsuarioId = tarefa.UsuarioId;
                    tarefaExistente.ProjetoId = tarefa.ProjetoId;
                }

                _context.Update(tarefaExistente);
                await _context.SaveChangesAsync();
  

        }

        public async Task DeletarTarefaAsync(int id)
        {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
                }

                var excluirTarefa = _context.Tarefas?.FirstOrDefault(t => t.TarefaId == id);

                if(excluirTarefa == null)
                {
                    throw new KeyNotFoundException($"Tarefa com ID {id} não encontrada para deletar.");
                }

                _context.Remove(excluirTarefa);
                await _context.SaveChangesAsync();
             
        }

        public async Task FinalizarTarefaAsync(int id, string confirmacao)
        {
           
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
                }

                var finalizarTarefa = _context.Tarefas?.FirstOrDefault(t => t.TarefaId == id);

                if (finalizarTarefa == null)
                {
                    throw new KeyNotFoundException($"Tarefa com ID {id} não encontrada para finalizar!.");  
                }

                if (confirmacao.ToUpper() == "SIM")
                {
                    finalizarTarefa.FinalizarTarefa(confirmacao);


                    _context.Update(confirmacao);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Não foi possível finalizar tarefa!");
                }
         
        }

        public async Task<IEnumerable<Tarefa>> ObterTodasTarefasAsync()
        {
            
             var tarefas = _context.Tarefas?.AsNoTracking().Take(10).ToList();

             return tarefas;
       
        }

        public async Task<Tarefa> ObterTarefaPorIdAsync(int id)
        {
            if (id < 0)
            {
              throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
            }

            var tarefaExistente = await _context.Tarefas.FirstOrDefaultAsync(t => t.TarefaId == id);

            if(tarefaExistente != null)
            {
                return tarefaExistente;
            }
            else
            {
                throw new KeyNotFoundException($"Tarefa com ID {id} não encontrada.");
            }

            

         
                
        }

        public async Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync()
        {
          
              var statusTarefas = await _context.Tarefas.Where(t => t.Status == StatusTarefa.Pendente).AsNoTracking().Take(3).ToListAsync(); 

              return statusTarefas;
           
        }

    }
}
