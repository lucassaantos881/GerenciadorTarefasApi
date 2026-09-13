using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface ITarefaService
    {
        Task AdicionarTarefaAsync(Tarefa tarefa);
        Task AtualizarTarefaAsync(int id, Tarefa tarefa);
        Task DeletarTarefaAsync(int id);
        Task FinalizarTarefaAsync(int id, string confirmacao);
        Task<Tarefa> ObterTarefaPorIdAsync(int id);
        Task<IEnumerable<Tarefa>> ObterTodasTarefasAsync();
        Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync();
    }
}
