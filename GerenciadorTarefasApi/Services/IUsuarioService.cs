using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> AdicionarUsuarioAsync(UsuarioDto usuario);
        Task<Usuario> AtualizarUsuarioAsync(int id, UsuarioDto usuario);
        Task<Usuario> DeletarUsuarioAsync(int id);
        Task<Usuario> ObterUsuarioPorIdAsync(int id);
        Task<IEnumerable<Usuario>> ObterTodosUsuariosAsync();
    }
}
