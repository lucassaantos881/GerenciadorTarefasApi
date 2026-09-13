using GerenciadorTarefasApi.Context;
using GerenciadorTarefasCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefasApi.Repository
{

    public class TarefaRepository : IGerenciadorRepository<Tarefa>, ITarefaRepository
    {

        private readonly GerenciadorContext _context;

        public TarefaRepository(GerenciadorContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Tarefa tarefa)
        {

            if (tarefa == null)
            {
                throw new ArgumentException("A tarefa não pode ser nula", nameof(tarefa));
            }

            var projeto = _context.Projetos?.FirstOrDefault(p => p.ProjetoId == tarefa.ProjetoId);

            if (tarefa.DataPrazo < projeto?.DataCriacao)
            {
                throw new ArgumentException("A data de prazo não pode ser anterior à data de início do projeto.");
            }

            _context.Add(tarefa);
            await _context.SaveChangesAsync();
        }

 

        public async Task AtualizarAsync(int id, Tarefa tarefa)
        {
            if(id < 0)
            {
                throw new ArgumentException("O ID da tarefa deve ser um valor positivo", nameof(id));
            }

            var tarefaExistente = await _context.Tarefas.FindAsync(id);

            if (tarefaExistente == null)
            {
                throw new ArgumentException("Tarefa não encontrada", nameof(tarefa));
            }

            _context.Entry(tarefaExistente).CurrentValues.SetValues(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
           if(id <= 0)
           {
               throw new ArgumentException("O ID da tarefa deve ser um valor positivo", nameof(id));
           }

           var tarefaExistente = await _context.Tarefas.FindAsync(id);

           if (tarefaExistente == null)
           {
               throw new ArgumentException("Tarefa não encontrada", nameof(tarefaExistente));
           }

           _context.Tarefas.Remove(tarefaExistente);
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
                throw new KeyNotFoundException($"Tarefa com ID: {id} não encontrada para finalizar!.");
            }

            if (confirmacao.ToUpper() == "SIM")
            {
                finalizarTarefa.FinalizarTarefa(confirmacao);

                _context.Update(finalizarTarefa);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Não foi possível finalizar tarefa!");
            }
        }

        public async Task<Tarefa> ObterPorIdAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("O ID da tarefa deve ser um valor positivo", nameof(id));
            }

            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<IEnumerable<Tarefa>> ObterTodosAsync()
        {
            return await _context.Tarefas.AsTracking().Take(5).ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync()
        {
            return await _context.Tarefas.Where(t => t.Status == StatusTarefa.Pendente).ToListAsync();
        }
    }
}
