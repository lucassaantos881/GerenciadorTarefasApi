using GerenciadorTarefasCore.Models;
using GerenciadorTarefasApi.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefasApi.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly GerenciadorContext _context;

        public UsuarioService(GerenciadorContext context)
        {
            _context = context;
        }

        public async Task AdicionarUsuarioAsync(Usuario usuario)
        {
                if (usuario == null)
                {
                    throw new ArgumentNullException("O usuário não pode ser nulo.");
                }

                _context.Add(usuario);
                await _context.SaveChangesAsync();
       
        }

        public async Task AtualizarUsuarioAsync(int id, Usuario usuario)
        {

                if(id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
                }

                var usuarioExistente = _context.Usuarios?.FirstOrDefault(u => u.UsuarioId == id);

                if(usuarioExistente == null)
                {
                   throw new KeyNotFoundException($"Usuário com ID {id} não encontrado para atualizar.");
                }
                else
                {
                    usuarioExistente.Nome = usuario.Nome;
                    usuarioExistente.Email = usuario.Email;

                    _context.Update(usuarioExistente);
                    await _context.SaveChangesAsync();
                }

            
           
        }

        public async Task DeletarUsuarioAsync(int id)
        {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
                }

                var usuarioExistente = _context.Usuarios?.FirstOrDefault(u => u.UsuarioId == id);

                if (usuarioExistente == null)
                {
                    throw new KeyNotFoundException($"Usuário com ID {id} não encontrado para deletar.");
                }
                else
                {
                    _context.Remove(usuarioExistente);
                    await _context.SaveChangesAsync();
                }
        }

        public async Task<IEnumerable<Usuario>> ObterTodosUsuariosAsync()
        {
           
           //AsNoTracking() é usado para melhorar o desempenho em consultas de leitura, pois não rastreia as alterações nos objetos retornados.
           //Take(5) é usado para limitar o número de registros retornados para 5.

           var usuarios = await _context.Usuarios.AsNoTracking().Take(5).ToListAsync(); 

           return usuarios;

        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
            }

            var usuarioLocalizado = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.UsuarioId == id);

            if (usuarioLocalizado != null)
            {
                return usuarioLocalizado;
            }
            else
            {
                throw new KeyNotFoundException($"Usuário com ID {id} não encontrado.");
            }
            
        }
    }
}
