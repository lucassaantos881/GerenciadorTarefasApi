using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Repository
{
    public interface ITarefaRepository
    {
        //Repositorio específico para a entidade Tarefa.
        Task FinalizarTarefaAsync(int id, string confirmacao);
        Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync();
    }
}
