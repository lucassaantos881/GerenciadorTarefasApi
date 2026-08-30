using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public interface IUsuarioService
    {

        void AdicionarUsuario(Usuario usuario);
        void AtualizarUsuario(int id, Usuario usuario);
        void DeletarUsuario(int id);
        IEnumerable<Usuario> ObterTodosUsuarios();
        Usuario ObterUsuarioPorId(int id);
    }
}
