using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Repository
{
    public interface IGerenciadorRepository<T>
    {

        Task AdicionarAsync(T entity);
        Task AtualizarAsync(int id, T entity);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task DeletarAsync(int id);

    }
}
