
using GerenciadorTarefasApi.Repository;
using GerenciadorTarefasCore.Models;
using GerenciadorTarefasCore.DTO_s;

namespace GerenciadorTarefasApi.Services
{
    public class TarefaService : ITarefaService
    {

        private readonly IGerenciadorRepository<Tarefa> _tarefaRepository;
        private readonly ITarefaRepository _tarefaRepositoryEspecifico;

        private readonly IProjetoService _projetoServiceValidacao;

        public TarefaService(IGerenciadorRepository<Tarefa> tarefaRepository, ITarefaRepository tarefaRepositoryEspecifico, IProjetoService projetoServiceValidacao)
        {
            _tarefaRepository = tarefaRepository;
            _tarefaRepositoryEspecifico = tarefaRepositoryEspecifico;
            _projetoServiceValidacao = projetoServiceValidacao;
        }

        public async Task<Tarefa> AdicionarTarefaAsync(TarefaDto tarefaDto)
        {
            
                if (tarefaDto == null)
                {
                    throw new ArgumentNullException("A tarefa não pode ser nula.");
                }
                
                var projetoExistente = await _projetoServiceValidacao.ObterProjetoPorIdAsync(tarefaDto.ProjetoId);

                if(tarefaDto.DataPrazo < projetoExistente.DataCriacao)
                {
                    throw new ArgumentException("A data do prazo da tarefa não pode ser anterior à data de criação do projeto.");
                }

                var tarefa = new Tarefa(tarefaDto.Titulo, tarefaDto.Descricao, tarefaDto.DataPrazo, tarefaDto.UsuarioId, tarefaDto.ProjetoId);

                await _tarefaRepository.AdicionarAsync(tarefa);
                return tarefa;

            
        }

        public async Task<Tarefa> AtualizarTarefaAsync(int id, TarefaDto tarefaDto)
        {
                if (id <= 0)
                {
                    throw new ArgumentOutOfRangeException("O ID da tarefa deve ser um valor positivo.");
                }

                var tarefaExistente = await _tarefaRepository.ObterPorIdAsync(id);

                if(tarefaExistente == null)
                {
                    throw new KeyNotFoundException("Tarefa não encontrada.");
                }

                tarefaExistente.Titulo = tarefaDto.Titulo;
                tarefaExistente.Descricao = tarefaDto.Descricao;   
                tarefaExistente.UsuarioId = tarefaDto.UsuarioId;
                tarefaExistente.ProjetoId = tarefaDto.ProjetoId;

                await _tarefaRepository.AtualizarAsync(id, tarefaExistente);
                return tarefaExistente;

        }

        public async Task<Tarefa> DeletarTarefaAsync(int id)
        {
                if (id <= 0)
                {
                    throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
                }

                var tarefaDeletada = await _tarefaRepository.DeletarAsync(id);
                return tarefaDeletada;
        }

        public async Task<Tarefa> FinalizarTarefaAsync(int id, string confirmacao)
        {

               
                if (id < 0)
                {
                    throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
                }

                if (string.IsNullOrWhiteSpace(confirmacao))
                {
                    throw new ArgumentNullException("Valor inválido, possível valor nulo!!!");

                }else if(confirmacao.ToLower() == "sim")
                {
                    confirmacao = confirmacao.ToUpper();
                   
                }
                var tarefaDeletada = await _tarefaRepositoryEspecifico.FinalizarTarefaAsync(id, confirmacao);
                return tarefaDeletada;
        }

        public async Task<IEnumerable<Tarefa>> ObterTodasTarefasAsync()
        {
            
            return await _tarefaRepository.ObterTodosAsync();

        }

        public async Task<Tarefa> ObterTarefaPorIdAsync(int id)
        {
            if (id < 0)
            {
              throw new ArgumentOutOfRangeException("Id precisa ser um valor positivo");
            }

           return await _tarefaRepository.ObterPorIdAsync(id);

        }

        public async Task<IEnumerable<Tarefa>> ObterTarefaPorStatusPendenteAsync()
        {
          
            return await _tarefaRepositoryEspecifico.ObterTarefaPorStatusPendenteAsync();

        }

    }
}
