using GerenciadorTarefasApi.Repository;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IGerenciadorRepository<Usuario> _usuarioRepository;

        public UsuarioService(IGerenciadorRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> AdicionarUsuarioAsync(Usuario usuario)
        {
                if (usuario == null)
                {
                    throw new ArgumentNullException("O usuário não pode ser nulo.");
                }

                await _usuarioRepository.AdicionarAsync(usuario);
                return usuario;

        }

        public async Task<Usuario> AtualizarUsuarioAsync(int id, Usuario usuario)
        {

                if(id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
                }

                var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(id);
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Email = usuario.Email;
        
                await _usuarioRepository.AtualizarAsync(id, usuarioExistente);
                return usuarioExistente;

        }

        public async Task<Usuario> DeletarUsuarioAsync(int id)
        {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
                }

                var usuarioDeletado = await _usuarioRepository.DeletarAsync(id);
                return usuarioDeletado;

        }

        public async Task<IEnumerable<Usuario>> ObterTodosUsuariosAsync()
        {
           
           return await _usuarioRepository.ObterTodosAsync();

        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
            }

            return await _usuarioRepository.ObterPorIdAsync(id);

        }
    }
}
