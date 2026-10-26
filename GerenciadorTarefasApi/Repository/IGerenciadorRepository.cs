using GerenciadorTarefasCore.Models;
using GerenciadorTarefasApi.Pagination;

namespace GerenciadorTarefasApi.Repository
{
    public interface IGerenciadorRepository<T>
    {

        Task<T> AdicionarAsync(T entity);
        Task<T> AtualizarAsync(int id, T entity);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<IEnumerable<T>> ObterPaginadoAsync(GerenciadorParameter parameter, Func<IQueryable<T>, IOrderedQueryable<T>> ordenacao);
        Task<T> ObterPorIdAsync(int id);
        Task<T> DeletarAsync(int id);

    }
}
