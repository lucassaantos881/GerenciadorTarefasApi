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

        public async Task AdicionarUsuarioAsync(Usuario usuario)
        {
                if (usuario == null)
                {
                    throw new ArgumentNullException("O usuário não pode ser nulo.");
                }

                await _usuarioRepository.AdicionarAsync(usuario);

        }

        public async Task AtualizarUsuarioAsync(int id, Usuario usuario)
        {

                if(id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
                }

                var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(id);
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Email = usuario.Email;
        
                await _usuarioRepository.AtualizarAsync(id, usuarioExistente);

        }

        public async Task DeletarUsuarioAsync(int id)
        {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
                }

                await _usuarioRepository.DeletarAsync(id);

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
