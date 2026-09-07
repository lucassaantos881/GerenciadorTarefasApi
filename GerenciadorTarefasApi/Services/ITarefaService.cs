using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface ITarefaService
    {

        Task AdicionarTarefaAsync(Tarefa tarefa);
        Task AtualizarTarefaAsync(int id, Tarefa tarefa);
        Task FinalizarTarefaAsync(int id, string confirmacao);
        Task DeletarTarefaAsync(int id);
        Task<IEnumerable<Tarefa>> ObterTodasTarefasAsync();
        Task<Tarefa> ObterTarefaPorIdAsync(int id);
        Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync();







    }
}
