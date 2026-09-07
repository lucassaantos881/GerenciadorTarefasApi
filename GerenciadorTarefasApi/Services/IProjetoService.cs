using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface IProjetoService
    {

        Task AdicionarProjetoAsync(Projeto projeto);
        Task AtualizarProjetoAsync(int id, Projeto projeto);
        Task<IEnumerable<Projeto>> ObterTodosProjetosAsync();
        Task<Projeto> ObterProjetoPorIdAsync(int id);
        Task DeletarProjetoAsync(int id);
        
       


    }
}
