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

        public void AdicionarProjeto(Projeto projeto)
        {
            try{
                if (projeto == null)
                {
                    throw new ArgumentNullException("O projeto não pode ser nulo.");
                }

                _context.Add(projeto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }

        }

        public void AtualizarProjeto(int id, Projeto projeto)
        {

            try
            {

                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do projeto deve ser um valor positivo.");
                }

                var projetoExistente = _context.Projetos?.FirstOrDefault(p => p.ProjetoId == id);

                if (projetoExistente == null)
                {
                    return;

                }
                else
                {
                    projetoExistente.Nome = projeto.Nome;
                    projetoExistente.Descricao = projeto.Descricao;
                    projetoExistente.DataConclusao = projeto.DataConclusao;
                }

                _context.Update(projetoExistente);
                _context.SaveChanges();

            }catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }




        }

        public void DeletarProjeto(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id tem que ser um número positivo");
                }

                var deletarProjeto = _context.Projetos?.FirstOrDefault(p => p.ProjetoId == id);

                _context.Remove(deletarProjeto);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }
        }


        public Projeto ObterProjetoPorId(int id){

            
          if (id < 0)
          {
             throw new ArgumentOutOfRangeException("Id tem que ser um número positivo");
          }

          var projeto = _context.Projetos?.FirstOrDefault(p => p.ProjetoId == id);

          return projeto;
            
            
        }

        public IEnumerable<Projeto> ObterTodosProjetos()
        {

           var projetoLocalizado = _context.Projetos?.AsNoTracking().Take(5).ToList();

           return projetoLocalizado;
 
        }

       

        
    }
}
