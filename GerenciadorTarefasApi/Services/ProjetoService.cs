using GerenciadorTarefasApi.Repository;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public class ProjetoService : IProjetoService
    {

        private readonly IGerenciadorRepository<Projeto> _projetoRepository;

        public ProjetoService(IGerenciadorRepository<Projeto> projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task AdicionarProjetoAsync(Projeto projeto)
        {
   
                if (projeto == null)
                {
                    throw new ArgumentNullException("O projeto não pode ser nulo.");
                }

                await _projetoRepository.AdicionarAsync(projeto);

        }

        public async Task AtualizarProjetoAsync(int id, Projeto projeto)
        {

                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("O ID do projeto deve ser um valor positivo.");
                }

                var projetoExistente = await _projetoRepository.ObterPorIdAsync(id);

            
                if (projetoExistente == null)
                {
                        throw new KeyNotFoundException("Projeto não encontrado.");
                }

                projetoExistente.Nome = projeto.Nome;
                projetoExistente.Descricao = projeto.Descricao;
                projetoExistente.DataCriacao = projeto.DataCriacao;
                projetoExistente.DataConclusao = projeto.DataConclusao;
            
                await _projetoRepository.AtualizarAsync(id, projetoExistente);
                

        }

        public async Task DeletarProjetoAsync(int id)
        {
         
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id tem que ser um número positivo");
                }

                await _projetoRepository.DeletarAsync(id);


        }


        public async Task<Projeto> ObterProjetoPorIdAsync(int id) {


            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("Id tem que ser um número positivo");
            }

            return await _projetoRepository.ObterPorIdAsync(id);

        }

        public async Task<IEnumerable<Projeto>> ObterTodosProjetosAsync()
        {

           var projetoLocalizado = await _projetoRepository.ObterTodosAsync();

           return projetoLocalizado;
 
        }

       

        
    }
}
