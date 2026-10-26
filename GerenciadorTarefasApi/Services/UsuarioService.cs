using GerenciadorTarefasApi.Repository;
using GerenciadorTarefasCore.Models;
using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.DTO_s.Mappings;
using GerenciadorTarefasApi.Pagination;

namespace GerenciadorTarefasApi.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IGerenciadorRepository<Usuario> _usuarioRepository;

        public UsuarioService(IGerenciadorRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> AdicionarUsuarioAsync(UsuarioDto usuarioDto)
        {
                if (usuarioDto == null)
                {
                    throw new ArgumentNullException("O usuário não pode ser nulo.");
                }

                var usuario = usuarioDto.ToUsuario();

                await _usuarioRepository.AdicionarAsync(usuario);
                return usuario;

        }

        public async Task<Usuario> AtualizarUsuarioAsync(int id, UsuarioDto usuarioDto)
        {

                if(id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
                }

                var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(id);
                
                usuarioExistente.AtualizarUsuario(usuarioDto);
        
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

        public async Task<IEnumerable<Usuario>> ObterUsuariosPaginadoAsync(GerenciadorParameter parameter)
        {          
            return await _usuarioRepository.ObterPaginadoAsync(parameter, 
                consulta => consulta.OrderBy(u => u.UsuarioId));
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
