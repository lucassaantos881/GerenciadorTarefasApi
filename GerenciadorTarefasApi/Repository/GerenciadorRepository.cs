using GerenciadorTarefasApi.Context;
using GerenciadorTarefasApi.Pagination;
using GerenciadorTarefasCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace GerenciadorTarefasApi.Repository
{
    //Criação de uma classe genérica onde o where T : class indica que o tipo T deve ser uma classe.
    public class GerenciadorRepository<T> : IGerenciadorRepository<T> where T : class
    {

        private readonly GerenciadorContext _context;
        
        public GerenciadorRepository(GerenciadorContext context)
        {
            _context = context;
        }

        public async Task<T> AdicionarAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> AtualizarAsync(int id,T entity)
        {

            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException("O ID deve ser um valor positivo.",nameof(id));
            }

            if(entity == null)
            {
                throw new ArgumentNullException("A entidade não pode ser nula.",nameof(entity));
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeletarAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException("O ID deve ser um valor positivo.",nameof(id));
            }

            var entity = await _context.FindAsync<T>(id);
            if(entity == null)
            {
                throw new KeyNotFoundException("Entidade não encontrada.");
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> ObterPorIdAsync(int id)
        {
            if(id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            return await _context.FindAsync<T>(id);
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> ObterPaginadoAsync(GerenciadorParameter parameter, Func<IQueryable<T>, IOrderedQueryable<T>> ordenacao)
        {

            // Realiza a consulta ao banco de dados e armazena um resultado sem ordenação em uma variável
            var consulta = ordenacao(_context.Set<T>());

            // Aplica a paginação à consulta usando Skip e Take, retornando apenas os registros correspondentes à página solicitada
            return await consulta
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToListAsync();
        }


    }
}
