using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface IProjetoService
    {

        Task AdicionarProjetoAsync(Projeto projeto);
        Task AtualizarProjetoAsync(int id, Projeto projeto);
        Task DeletarProjetoAsync(int id);
        Task<Projeto> ObterProjetoPorIdAsync(int id);
        Task<IEnumerable<Projeto>> ObterTodosProjetosAsync();
    }
}
