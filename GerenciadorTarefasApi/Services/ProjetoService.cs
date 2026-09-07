using GerenciadorTarefasApi.Context;
using GerenciadorTarefasCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefasApi.Services
{
    public class ProjetoService : IProjetoService
    {

        private readonly GerenciadorContext _context;

        public ProjetoService(GerenciadorContext context)
        {
            _context = context;
        }

        public async Task AdicionarProjetoAsync(Projeto projeto)
        {
   
                if (projeto == null)
                {
                    throw new ArgumentNullException("O projeto não pode ser nulo.");
                }

                _context.Add(projeto);
                await _context.SaveChangesAsync();

        }

        public async Task AtualizarProjetoAsync(int id, Projeto projeto)
        {

                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do projeto deve ser um valor positivo.");
                }

                var projetoExistente = _context.Projetos?.FirstOrDefault(p => p.ProjetoId == id);

                if (projetoExistente == null)
                {
                    throw new KeyNotFoundException($"Projeto com ID {id} não encontrado para atualizar.");
                }
                else
                {
                    projetoExistente.Nome = projeto.Nome;
                    projetoExistente.Descricao = projeto.Descricao;
                    projetoExistente.DataConclusao = projeto.DataConclusao;
                }

                _context.Update(projetoExistente);
                await _context.SaveChangesAsync();

        }

        public async Task DeletarProjetoAsync(int id)
        {
         
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id tem que ser um número positivo");
                }

                var deletarProjeto = _context.Projetos?.FirstOrDefault(p => p.ProjetoId == id);

                if(deletarProjeto == null)
                {
                    throw new KeyNotFoundException($"Projeto com ID {id} não encontrado para deletar.");
                }

                _context.Remove(deletarProjeto);
                await _context.SaveChangesAsync();

               
           
        }


        public async Task<Projeto> ObterProjetoPorIdAsync(int id) {


            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("Id tem que ser um número positivo");
            }

            var projeto = await _context.Projetos.FirstOrDefaultAsync(p => p.ProjetoId == id);

            if (projeto != null)
            {
                return projeto;
            }
            else
            {
                throw new KeyNotFoundException($"Projeto com ID {id} não encontrado.");
            }

        }

        public async Task<IEnumerable<Projeto>> ObterTodosProjetosAsync()
        {

           var projetoLocalizado = await _context.Projetos.AsNoTracking().Take(5).ToListAsync();

           return projetoLocalizado;
 
        }

       

        
    }
}
