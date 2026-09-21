using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Repository
{
    public interface IGerenciadorRepository<T>
    {

        Task<T> AdicionarAsync(T entity);
        Task<T> AtualizarAsync(int id, T entity);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task<T> DeletarAsync(int id);

    }
}
