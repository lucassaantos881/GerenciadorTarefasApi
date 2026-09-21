using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface IProjetoService
    {

        Task<Projeto> AdicionarProjetoAsync(ProjetoDto projetoDto);
        Task<Projeto> AtualizarProjetoAsync(int id, ProjetoDto projetoDto);
        Task<Projeto> DeletarProjetoAsync(int id);
        Task<Projeto> ObterProjetoPorIdAsync(int id);
        Task<IEnumerable<Projeto>> ObterTodosProjetosAsync();
    }
}
