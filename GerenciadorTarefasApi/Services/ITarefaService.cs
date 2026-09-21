using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface ITarefaService
    {
        Task<Tarefa> AdicionarTarefaAsync(Tarefa tarefaDto);
        Task<Tarefa> AtualizarTarefaAsync(int id, Tarefa tarefaDto);
        Task<Tarefa> DeletarTarefaAsync(int id);
        Task<Tarefa> FinalizarTarefaAsync(int id, string confirmacao);
        Task<Tarefa> ObterTarefaPorIdAsync(int id);
        Task<IEnumerable<Tarefa>> ObterTodasTarefasAsync();
        Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync();
    }
}
