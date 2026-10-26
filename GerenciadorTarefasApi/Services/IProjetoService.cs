using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.Models;
using GerenciadorTarefasApi.Pagination;

namespace GerenciadorTarefasApi.Services
{
    public interface IProjetoService
    {

        Task<Projeto> AdicionarProjetoAsync(ProjetoDto projetoDto);
        Task<Projeto> AtualizarProjetoAsync(int id, ProjetoDto projetoDto);
        Task<Projeto> DeletarProjetoAsync(int id);
        Task<Projeto> FinalizarProjeto(int id, string confirmacao, DateTime dataConclusao);
        Task<Projeto> ObterProjetoPorIdAsync(int id);
        Task<IEnumerable<Projeto>> ObterTodosProjetosAsync();
        Task<IEnumerable<Projeto>> ObterProjetosPaginadoAsync(GerenciadorParameter parameter);
    }
}
