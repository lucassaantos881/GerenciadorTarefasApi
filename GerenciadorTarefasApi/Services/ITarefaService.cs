using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.Models;
using GerenciadorTarefasApi.Pagination;

namespace GerenciadorTarefasApi.Services
{
    public interface ITarefaService
    {
        Task<Tarefa> AdicionarTarefaAsync(TarefaDto tarefaDto);
        Task<Tarefa> AtualizarTarefaAsync(int id, TarefaDto tarefaDto);
        Task<Tarefa> DeletarTarefaAsync(int id);
        Task<Tarefa> IniciarTarefaAsync(int id, int usuarioId);
        Task<Tarefa> CancelarTarefaAsync(int id, string confirmacao);
        Task<Tarefa> FinalizarTarefaAsync(int id, string confirmacao);
        Task<Tarefa> ObterTarefaPorIdAsync(int id);
        Task<IEnumerable<Tarefa>> ObterTodasTarefasAsync();
        Task<IEnumerable<Tarefa>> ObterTarefasPaginadoAsync(GerenciadorParameter parameter);
        Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync();
    }
}
