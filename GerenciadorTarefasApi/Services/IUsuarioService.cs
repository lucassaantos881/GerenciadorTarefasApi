using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface IUsuarioService
    {

        Task AdicionarUsuarioAsync(Usuario usuario);
        Task AtualizarUsuarioAsync(int id, Usuario usuario);
        Task DeletarUsuarioAsync(int id);
        Task<IEnumerable<Usuario>> ObterTodosUsuariosAsync();
        Task<Usuario> ObterUsuarioPorIdAsync(int id);
    }
}
