using GerenciadorTarefasApi.Repository;
using GerenciadorTarefasCore.DTO_s;
using GerenciadorTarefasCore.Models;

namespace GerenciadorTarefasApi.Services
{
    public class ProjetoService : IProjetoService
    {

        private readonly IGerenciadorRepository<Projeto> _projetoRepository;
        private readonly IProjetoRepository _projetoRepositoryEspecifico;

        public ProjetoService(IGerenciadorRepository<Projeto> projetoRepository, IProjetoRepository projetoRepositoryEspecifico)
        {
            _projetoRepository = projetoRepository;
            _projetoRepositoryEspecifico = projetoRepositoryEspecifico;
        }

        public async Task<Projeto> AdicionarProjetoAsync(ProjetoDto projetoDto)
        {
   
                if (projetoDto == null)
                {
                    throw new ArgumentNullException("O projeto não pode ser nulo.");
                }

                var projeto = new Projeto(projetoDto.Nome, projetoDto.Descricao, projetoDto.DataCriacao);

                await _projetoRepository.AdicionarAsync(projeto);
                return projeto;

        }

        public async Task<Projeto> AtualizarProjetoAsync(int id, ProjetoDto projetoDto)
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

                projetoExistente.Nome = projetoDto.Nome;
                projetoExistente.Descricao = projetoDto.Descricao;
                
                await _projetoRepository.AtualizarAsync(id, projetoExistente);
                return projetoExistente;
                

        }

        public async Task<Projeto> DeletarProjetoAsync(int id)
        {
         
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id tem que ser um número positivo");
                }

                var projetoDeletado = await _projetoRepository.DeletarAsync(id);
                return projetoDeletado;


        }

        public async Task<Projeto> FinalizarProjeto(int id, string confirmacao, DateTime dataConclusao)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("ID do projeto inválido.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(confirmacao))
            {
                throw new ArgumentNullException("Confirmação não pode ser nula!!");
            }

            return await _projetoRepositoryEspecifico.ConcluirProjetoAsync(id, confirmacao, dataConclusao);

        }

        public async Task<Projeto> ObterProjetoPorIdAsync(int id) {


            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("Id tem que ser um número positivo");
            }

            var projetoLocalizado = await _projetoRepository.ObterPorIdAsync(id);
            return projetoLocalizado;

        }

        public async Task<IEnumerable<Projeto>> ObterTodosProjetosAsync()
        {

           var projetoLocalizado = await _projetoRepository.ObterTodosAsync();

           return projetoLocalizado;
 
        }

       

        
    }
}
