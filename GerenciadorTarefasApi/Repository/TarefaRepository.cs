using GerenciadorTarefasApi.Context;
using GerenciadorTarefasCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefasApi.Repository
{

    public class TarefaRepository : GerenciadorRepository<Tarefa>, ITarefaRepository
    {

        private readonly GerenciadorContext _context;

        public TarefaRepository(GerenciadorContext context) : base(context)
        {
            _context = context;
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

       
        public async Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync()
        {
            return await _context.Tarefas.Where(t => t.Status == StatusTarefa.Pendente).ToListAsync();
        }
    }
}
