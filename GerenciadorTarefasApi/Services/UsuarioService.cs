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

        public void AdicionarUsuario(Usuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    throw new ArgumentNullException("O usuário não pode ser nulo.");
                }

                _context.Add(usuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }


        }

        public void AtualizarUsuario(int id, Usuario usuario)
        {
            try
            {

                if(id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
                }

                var usuarioExistente = _context.Usuarios?.FirstOrDefault(u => u.UsuarioId == id);

                if(usuarioExistente == null)
                {
                    throw new KeyNotFoundException("O usuário não foi encontrado.");
                }
                else
                {
                    usuarioExistente.Nome = usuario.Nome;
                    usuarioExistente.Email = usuario.Email;

                    _context.Update(usuarioExistente);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void DeletarUsuario(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
                }

                var usuarioExistente = _context.Usuarios?.FirstOrDefault(u => u.UsuarioId == id);

                if (usuarioExistente == null)
                {
                    throw new KeyNotFoundException("O usuário não foi encontrado.");
                }
                else
                {
                    _context.Remove(usuarioExistente);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
           
           //AsNoTracking() é usado para melhorar o desempenho em consultas de leitura, pois não rastreia as alterações nos objetos retornados.
           //Take(5) é usado para limitar o número de registros retornados para 5.

           var usuarios = _context.Usuarios?.AsNoTracking().Take(5).ToList(); 

           return usuarios;

        }

        public Usuario ObterUsuarioPorId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("O ID do usuário deve ser um valor positivo.");
            }

            var usuarioLocalizado = _context.Usuarios?.AsNoTracking().FirstOrDefault(u => u.UsuarioId == id);

            return usuarioLocalizado;
        }
    }
}
